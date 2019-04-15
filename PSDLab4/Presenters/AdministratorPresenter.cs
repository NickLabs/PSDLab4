using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.ViewInterfaces;
using DomainModel.Infrastructure;
using DomainModel.Service;

namespace PSDLab4.Presenters
{
    public class AdministratorPresenter
    {
        private readonly IAdminForm form;
        private readonly IDataBaseModel dataBase;
        private readonly DatabaseParser parser;

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
            string material = this.form.currentMaterial;
            string[] columnNames = this.parser.GetDataBaseRules()[material].Select(x => x.Key).ToArray();
            string[] columnTypes = this.parser.GetDataBaseRules()[material].Select(x => x.Value).ToArray();

            Dictionary<string, Dictionary<string, Dictionary<string, int>>> columnReferencesTable = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

            //Получаем названия внешних таблиц
            string[] refColumns = this.parser.GetReferences()[material].Select(x => x.Key).ToArray();
            string[] outerTables = this.parser.GetReferences()[material].Select(x => x.Value).ToArray();
            if (outerTables.Length > 0)
            {
                //Для каждой связной таблицы получаем айдишники и их текстовые представления для наглядности
                Dictionary<string, Dictionary<string, int>> tableNameKey = new Dictionary<string, Dictionary<string, int>>();
                for(int i = 0; i < refColumns.Length; i++)
                {
                    Dictionary<string, int> nameKeys = this.dataBase.IdAndRelevantNames(outerTables[i]);
                    tableNameKey.Add(outerTables[i], nameKeys);
                    columnReferencesTable.Add(refColumns[i], tableNameKey);
                }
            }


            this.form.GenerateInputFields(columnNames, columnTypes, columnReferencesTable);
        }

        public AdministratorPresenter(IAdminForm form, IDataBaseModel dataBase, DatabaseParser parser)
        {
            this.form = form;
            this.dataBase = dataBase;
            this.parser = parser;
            this.form.materialChanged += MaterialChanged;
        }
    }
}
