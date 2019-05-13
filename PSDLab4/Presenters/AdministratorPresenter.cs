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
        private Dictionary<string, Dictionary<string, int>> columnReferencesTableNameKeys = new Dictionary<string, Dictionary<string, int>>();
        private Dictionary<string, Dictionary<int, string>> columnReferencesTableKeyNames = new Dictionary<string, Dictionary<int, string>>();

        private rowStatus status = rowStatus.CHANGE;

        public event EventHandler changeUser;

        public void Start()
        {
            string[] names = dataBase.GetAllTables();
            this.parser.Parse();
            this.form.Start(names);
        }

        public void Close()
        {
            this.form.Stop();
        }

        private void MaterialChanged(object sender, EventArgs e)
        {
            //Из парсера взять правила по типам и зависимости для конкретного материала
            //отправить эти правила в форму
            columnReferencesTableNameKeys = new Dictionary<string, Dictionary<string, int>>();
            columnReferencesTableKeyNames = new Dictionary<string, Dictionary<int, string>>();

            this.status = rowStatus.ADD;
            string table = this.form.CurrentTable;
            this.columnNames = this.parser.GetDataBaseRules()[dataBase.GetTranslationToEng(table)].Select(x => dataBase.GetTranslationToRus(x.Key)).ToArray();
            string[] columnTypes = this.parser.GetDataBaseRules()[dataBase.GetTranslationToEng(table)].Select(x => x.Value).ToArray();
            numberOfColumns = columnNames.Length;

            var ss = this.parser.GetReferences()[dataBase.GetTranslationToEng(table)];

            //Получаем названия внешних таблиц
            string[] refColumns = this.parser.GetReferences()[dataBase.GetTranslationToEng(table)].Select(x => this.dataBase.GetTranslationToRus(x.Key)).ToArray();
            string[] outerTables = this.parser.GetReferences()[dataBase.GetTranslationToEng(table)].Select(x => this.dataBase.GetTranslationToRus(x.Value)).ToArray();
            if (outerTables.Length > 0)
            {
                //Для каждой связной таблицы получаем айдишники и их текстовые представления для наглядности 
                for (int i = 0; i < refColumns.Length; i++)
                {
                    Dictionary<string, int> nameKeys = this.dataBase.IdAndRelevantNames(outerTables[i], this.parser.GetTablesColumnNames(this.dataBase.GetTranslationToEng(outerTables[i])).Select(x=>this.dataBase.GetTranslationToRus(x)).ToArray());
                    Dictionary<int, string> keyNames = this.dataBase.IdAndRelevantNameReverse(outerTables[i], this.parser.GetTablesColumnNames(this.dataBase.GetTranslationToEng(outerTables[i])).Select(x => this.dataBase.GetTranslationToRus(x)).ToArray());
                    this.columnReferencesTableNameKeys.Add(refColumns[i], nameKeys);
                    this.columnReferencesTableKeyNames.Add(refColumns[i], keyNames);
                }
            }

            this.form.GenerateInputFields(columnNames, columnTypes, this.columnReferencesTableNameKeys);
            DataTable s = this.dataBase.GetTableData(table);
            this.form.SetColumnNames(columnNames);
            this.form.SetData(s, columnReferencesTableKeyNames);

            var ar = new ArrayList();
            for (int i = 0; i < this.numberOfColumns; i++)
            {
                ar.Add("");
            }
            this.form.SetChangeDeleteRules(this.dataBase.GetRule(table));
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
            this.form.changeUser += ChangeUser;
        }

        private void ChangeUser(object sender, EventArgs e)
        {
            this.form.Stop();
            changeUser?.Invoke(this, null);
        }

        private void DataSubmitted(object sender, EventArgs e)
        {
            if (status == rowStatus.ADD)
            {
                ArrayList tmp = new ArrayList();
                tmp = this.form.ValuesToSubmit;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (columnNames[i].StartsWith("Ид ") && columnNames[i].Length > 2)
                    {
                        tmp[i] = columnReferencesTableNameKeys[columnNames[i]][(tmp[i] as string)];
                    }
                }
                try
                {
                    this.dataBase.InsertRow(this.form.CurrentTable, tmp);
                    this.form.UpdateTable();
                    this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable), columnReferencesTableKeyNames);

                    var ar = new ArrayList();
                    for (int i = 0; i < this.numberOfColumns; i++)
                    {
                        ar.Add("");
                    }
                    this.form.ValuesToSubmit = ar;
                }
                catch (Exception)
                {
                    this.form.ShowSQLInjectionError();
                }
            }
            else
            {
                ArrayList tmp = new ArrayList();
                tmp = this.form.ValuesToSubmit;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (columnNames[i].StartsWith("Ид ") && columnNames[i].Length > 2)
                    {
                        tmp[i] = columnReferencesTableNameKeys[columnNames[i]][(tmp[i] as string)];
                    }
                }
                try
                {
                    this.dataBase.UpdateRow(this.form.CurrentTable, tmp, columnNames);
                    this.form.UpdateTable();
                    this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable), columnReferencesTableKeyNames);

                    var ar = new ArrayList();
                    for (int i = 0; i < this.numberOfColumns; i++)
                    {
                        ar.Add("");
                    }
                    this.form.ValuesToSubmit = ar;
                }
                catch (Exception)
                {
                    this.form.ShowSQLInjectionError();
                }
            }
        }

        private void DataDeleted(object sender, EventArgs e)
        {
            if (this.columnNames[0].StartsWith("Ид "))
            {
                this.dataBase.DeleteRow(this.form.CurrentTable, this.form.SelectedItemIndex[0], this.form.SelectedItemIndex[1], this.columnNames[0], this.columnNames[1]);
                this.form.UpdateTable();
                this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable), columnReferencesTableKeyNames);
                var ar = new ArrayList();
                for (int i = 0; i < this.numberOfColumns; i++)
                {
                    ar.Add("");
                }
                this.form.ValuesToSubmit = ar;
            }
            else
            {
                this.dataBase.DeleteRow(this.form.CurrentTable, this.form.SelectedItemIndex[0]);
                this.form.UpdateTable();
                this.form.SetData(this.dataBase.GetTableData(this.form.CurrentTable), columnReferencesTableKeyNames);
                var ar = new ArrayList();
                for (int i = 0; i < this.numberOfColumns; i++)
                {
                    ar.Add("");
                }
                this.form.ValuesToSubmit = ar;
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
