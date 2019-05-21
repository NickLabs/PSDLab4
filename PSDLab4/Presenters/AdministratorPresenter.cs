using DomainModel.Infrastructure;
using DomainModel.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using View.ViewInterfaces;

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
            parser.Parse();
            form.Start(names);
        }

        public void Close()
        {
            form.Stop();
        }

        private void TableChanged(object sender, EventArgs e)
        {
            //Из парсера взять правила по типам и зависимости для конкретной таблицы
            //отправить эти правила в форму
            columnReferencesTableNameKeys = new Dictionary<string, Dictionary<string, int>>();
            columnReferencesTableKeyNames = new Dictionary<string, Dictionary<int, string>>();

            status = rowStatus.ADD;
            string table = form.CurrentTable;
            columnNames = parser.GetDataBaseRules()[dataBase.GetTranslationToEng(table)].Select(x => dataBase.GetTranslationToRus(x.Key)).ToArray();
            string[] columnTypes = parser.GetDataBaseRules()[dataBase.GetTranslationToEng(table)].Select(x => x.Value).ToArray();
            numberOfColumns = columnNames.Length;

            var ss = parser.GetReferences()[dataBase.GetTranslationToEng(table)];

            //Получаем названия внешних таблиц
            string[] refColumns = parser.GetReferences()[dataBase.GetTranslationToEng(table)].Select(x => dataBase.GetTranslationToRus(x.Key)).ToArray();
            string[] outerTables = parser.GetReferences()[dataBase.GetTranslationToEng(table)].Select(x => dataBase.GetTranslationToRus(x.Value)).ToArray();
            if (outerTables.Length > 0)
            {
                //Для каждой связной таблицы получаем айдишники и их текстовые представления для наглядности 
                for (int i = 0; i < refColumns.Length; i++)
                {
                    Dictionary<string, int> nameKeys = dataBase.IdAndRelevantNames(outerTables[i], parser.GetTablesColumnNames(dataBase.GetTranslationToEng(outerTables[i])).Select(x => dataBase.GetTranslationToRus(x)).ToArray());
                    Dictionary<int, string> keyNames = dataBase.IdAndRelevantNameReverse(outerTables[i], parser.GetTablesColumnNames(dataBase.GetTranslationToEng(outerTables[i])).Select(x => dataBase.GetTranslationToRus(x)).ToArray());
                    columnReferencesTableNameKeys.Add(refColumns[i], nameKeys);
                    columnReferencesTableKeyNames.Add(refColumns[i], keyNames);
                }
            }

            form.GenerateInputFields(columnNames, columnTypes, columnReferencesTableNameKeys);
            DataTable s = dataBase.GetTableData(table);
            form.SetColumnNames(columnNames);
            form.SetData(s, columnReferencesTableKeyNames);

            var ar = new ArrayList();
            for (int i = 0; i < numberOfColumns; i++)
            {
                ar.Add("");
            }
            form.SetChangeDeleteRules(dataBase.GetRule(table));
            form.ChangeAddCirculation("Добавление", ar);
        }

        public AdministratorPresenter(IAdminForm form, IDataBaseModel dataBase, DatabaseParser parser)
        {
            this.form = form;
            this.dataBase = dataBase;
            this.parser = parser;
            this.form.materialChanged += TableChanged;
            this.form.changeAdd += ChangeAddCycle;
            this.form.delete += DataDeleted;
            this.form.submit += DataSubmitted;
            this.form.changeUser += ChangeUser;
        }

        private void ChangeUser(object sender, EventArgs e)
        {
            form.Stop();
            changeUser?.Invoke(this, null);
        }

        private void DataSubmitted(object sender, EventArgs e)
        {
            if (status == rowStatus.ADD)
            {
                ArrayList tmp = new ArrayList();
                tmp = form.ValuesToSubmit;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (columnNames[i].StartsWith("Ид ") && columnNames[i].Length > 2)
                    {
                        tmp[i] = columnReferencesTableNameKeys[columnNames[i]][(tmp[i] as string)];
                    }
                }
                try
                {
                    dataBase.InsertRow(form.CurrentTable, tmp);
                    form.UpdateTable();
                    form.SetData(dataBase.GetTableData(form.CurrentTable), columnReferencesTableKeyNames);

                    var ar = new ArrayList();
                    for (int i = 0; i < numberOfColumns; i++)
                    {
                        ar.Add("");
                    }
                    form.ValuesToSubmit = ar;
                }
                catch (Exception)
                {
                    form.ShowSQLInjectionError();
                }
            }
            else
            {
                ArrayList tmp = new ArrayList();
                tmp = form.ValuesToSubmit;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (columnNames[i].StartsWith("Ид ") && columnNames[i].Length > 2)
                    {
                        tmp[i] = columnReferencesTableNameKeys[columnNames[i]][(tmp[i] as string)];
                    }
                }
                try
                {
                    dataBase.UpdateRow(form.CurrentTable, tmp, columnNames);
                    form.UpdateTable();
                    form.SetData(dataBase.GetTableData(form.CurrentTable), columnReferencesTableKeyNames);

                    var ar = new ArrayList();
                    for (int i = 0; i < numberOfColumns; i++)
                    {
                        ar.Add("");
                    }
                    form.ValuesToSubmit = ar;
                }
                catch (Exception)
                {
                    form.ShowSQLInjectionError();
                }
            }
        }

        private void DataDeleted(object sender, EventArgs e)
        {
            if (columnNames[0].StartsWith("Ид "))
            {

                int firstIndex = columnReferencesTableNameKeys[columnNames[0]][form.SelectedItemIndex[0]];
                int secondIndex = columnReferencesTableNameKeys[columnNames[1]][form.SelectedItemIndex[1]];
                dataBase.DeleteRow(form.CurrentTable, firstIndex, secondIndex, columnNames[0], columnNames[1]);
                form.UpdateTable();
                form.SetData(dataBase.GetTableData(form.CurrentTable), columnReferencesTableKeyNames);
                var ar = new ArrayList();
                for (int i = 0; i < numberOfColumns; i++)
                {
                    ar.Add("");
                }
                form.ValuesToSubmit = ar;
            }
            else
            {
                int firstIndex = Convert.ToInt32(form.SelectedItemIndex[0]);
                dataBase.DeleteRow(form.CurrentTable, firstIndex);
                form.UpdateTable();
                form.SetData(dataBase.GetTableData(form.CurrentTable), columnReferencesTableKeyNames);
                var ar = new ArrayList();
                for (int i = 0; i < numberOfColumns; i++)
                {
                    ar.Add("");
                }
                form.ValuesToSubmit = ar;
            }
        }

        private void ChangeAddCycle(object sender, EventArgs e)
        {

            if (status == rowStatus.ADD && form.NumberOfSelectedRows == 1)
            {
                status = rowStatus.CHANGE;
                form.ChangeAddCirculation("Изменение", form.ValuesToInsert);
            }
            else if (status == rowStatus.CHANGE && form.NumberOfSelectedRows == 1)
            {
                status = rowStatus.CHANGE;
                form.ChangeAddCirculation("Изменение", form.ValuesToInsert);
            }
            else if (status == rowStatus.CHANGE && form.NumberOfSelectedRows < 1)
            {
                status = rowStatus.ADD;
                var ar = new ArrayList();
                for (int i = 0; i < numberOfColumns; i++)
                {
                    ar.Add("");
                }
                form.ChangeAddCirculation("Добавление", ar);
            }

            //Ошибочные случаи
            else if (status == rowStatus.ADD && form.NumberOfSelectedRows != 1)
            {
                form.ShowChangeOrDeleteError();
            }
            else if (status == rowStatus.CHANGE && form.NumberOfSelectedRows > 1)
            {
                form.ShowChangeOrDeleteError();
            }

        }
    }
}
