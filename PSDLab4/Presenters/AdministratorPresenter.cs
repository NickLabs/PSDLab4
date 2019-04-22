using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.ViewInterfaces;
using DomainModel.Infrastructure;
using DomainModel.Service;
using System.Collections;
using System.Data;

namespace PSDLab4.Presenters
{
    public class AdministratorPresenter
    {
        private readonly IAdminForm form;
        private readonly IDataBaseModel dataBase;
        private readonly DatabaseParser parser;
        private enum rowStatus { ADD, CHANGE };
        private int numberOfColumns = -1;
        private string[] columnNames;


        private rowStatus status = rowStatus.CHANGE;

        public void Start()
        {
            string[] names = dataBase.GetAllTables();
            this.parser.Parse();
            this.form.Start(names);
        }

        private void MaterialChanged(object sender, EventArgs e)
        {
            //Dictionary<string, Dictionary<string, object>> dataBaseRules;
            //private Dictionary<string, Dictionary<string, string>> references;
            //Из парсера взять правила по типам и зависимости для конкретного материала
            //отправить эти правила в форму
            this.status = rowStatus.ADD;
            string table = this.form.CurrentTable;
            this.columnNames = this.parser.GetDataBaseRules()[table].Select(x => x.Key).ToArray();
            string[] columnTypes = this.parser.GetDataBaseRules()[table].Select(x => x.Value).ToArray();
            numberOfColumns = columnNames.Length;
            Dictionary<string, Dictionary<string, Dictionary<string, int>>> columnReferencesTable = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

            //Получаем названия внешних таблиц
            string[] refColumns = this.parser.GetReferences()[table].Select(x => x.Key).ToArray();
            string[] outerTables = this.parser.GetReferences()[table].Select(x => x.Value).ToArray();
            if (outerTables.Length > 0)
            {
                //Для каждой связной таблицы получаем айдишники и их текстовые представления для наглядности
                Dictionary<string, Dictionary<string, int>> tableNameKey = new Dictionary<string, Dictionary<string, int>>();
                for (int i = 0; i < refColumns.Length; i++)
                {
                    Dictionary<string, int> nameKeys = this.dataBase.IdAndRelevantNames(outerTables[i]);
                    tableNameKey.Add(outerTables[i], nameKeys);
                    columnReferencesTable.Add(refColumns[i], tableNameKey);
                }
            }
            this.form.GenerateInputFields(columnNames, columnTypes, columnReferencesTable);
            DataTable s = this.dataBase.GetTableData(table);
            this.form.SetColumnNames(columnNames);
            this.form.SetData(s);

            var ar = new ArrayList();
            for (int i = 0; i < this.numberOfColumns; i++)
            {
                ar.Add("");
            }

            this.form.ChangeAddCirculation("Добавление", ar);
        }

        public AdministratorPresenter(IAdminForm form, IDataBaseModel dataBase, DatabaseParser parser)
        {
            this.form = form;
            this.dataBase = dataBase;
            this.parser = parser;
            this.form.materialChanged += MaterialChanged;
            this.form.changeAdd += ChangeAddCycle;
            this.form.delete += DataDeleted;
            this.form.submit += DataSubmitted;
        }

        private void DataSubmitted(object sender, EventArgs e)
        {
            if (status == rowStatus.ADD)
            {
                this.dataBase.InsertRow(this.form.CurrentTable, this.form.ValuesToSubmit);
                this.form.UpdateTable();
                this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable));
            }
            else
            {
                this.dataBase.UpdateRow(this.form.CurrentTable, this.form.ValuesToSubmit, columnNames);
                this.form.UpdateTable();
                this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable));
            }
        }

        private void DataDeleted(object sender, EventArgs e)
        {
            if (this.columnNames[0].Length > 2)
            {
                this.dataBase.DeleteRow(this.form.CurrentTable, this.form.SelectedItemIndex[0], this.form.SelectedItemIndex[1], this.columnNames[0], this.columnNames[1]);
                this.form.UpdateTable();
                this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable));
            }
            else
            {
                this.dataBase.DeleteRow(this.form.CurrentTable, this.form.SelectedItemIndex[0]);
                this.form.UpdateTable();
                this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable));
            }
        }

        private void ChangeAddCycle(object sender, EventArgs e)
        {

            if (this.status == rowStatus.ADD && this.form.NumberOfSelectedRows == 1)
            {
                this.status = rowStatus.CHANGE;
                this.form.ChangeAddCirculation("Изменение", this.form.ValuesToInsert);
            }
            else if (this.status == rowStatus.CHANGE && this.form.NumberOfSelectedRows == 1)
            {
                this.status = rowStatus.CHANGE;
                this.form.ChangeAddCirculation("Изменение", this.form.ValuesToInsert);
            }
            else if (this.status == rowStatus.CHANGE && this.form.NumberOfSelectedRows < 1)
            {
                this.status = rowStatus.ADD;
                var ar = new ArrayList();
                for (int i = 0; i < this.numberOfColumns; i++)
                {
                    ar.Add("");
                }
                this.form.ChangeAddCirculation("Добавление", ar);
            }

            //Ошибочные случаи
            else if (this.status == rowStatus.ADD && this.form.NumberOfSelectedRows != 1)
            {
                this.form.ShowChangeOrDeleteError();
            }
            else if (this.status == rowStatus.CHANGE && this.form.NumberOfSelectedRows > 1)
            {
                this.form.ShowChangeOrDeleteError();
            }

        }
    }
}
