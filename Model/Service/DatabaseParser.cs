using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace DomainModel.Service
{
    public class DatabaseParser
    {
        private Dictionary<string, Dictionary<string, string>> dataBaseRules;
        private Dictionary<string, Dictionary<string, string>> references;
        private string connectionToDb = @"Data Source = C:\Users\prais\Desktop\Programming\Projects\C#\c#MVPLab4\PSDLab4\PSDLab4\resources\4.db;foreign keys=true;";
        
        public DatabaseParser() { }

        public Dictionary<string, Dictionary<string, string>> GetDataBaseRules()
        {
            return dataBaseRules;
        }

        public Dictionary<string, Dictionary<string, string>> GetReferences()
        {
            return references;
        }

        public string[] GetTablesColumnNames(string tableName)
        {
            return dataBaseRules[tableName].Keys.ToArray();
        }

        public void Parse()
        {
            SQLiteConnection c = new SQLiteConnection(connectionToDb);
            c.Open();
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = c;
            DataTable dt = c.GetSchema("Tables");
            this.dataBaseRules = new Dictionary<string, Dictionary<string, string>>();
            this.references = new Dictionary<string, Dictionary<string, string>>();
            foreach (DataRow d in dt.Rows)
            {

                try
                {
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    int ruleStart = (d[6] as string).IndexOf('(');
                    string rules = "";
                    for (int i = ruleStart; i < (d[6] as string).Length; i++)
                    {
                        rules += (d[6] as string)[i];
                    }
                    rules = rules.Replace("(", string.Empty).Replace(")", string.Empty).Replace("\t", " ").Trim();
                    string[] atrRules1 = rules.Split('\n');
                    atrRules1 = atrRules1.Select(x => x.Trim()).ToArray();
                    Dictionary<string, string> keyTable = new Dictionary<string, string>();
                    foreach (string rule in atrRules1)
                    {
                        string columnName = "";
                        string columnType = "";

                        int rulesIndex = 1;
                        if (rule.StartsWith("\""))
                        {

                            for (int i = 1; rule[i] != '"'; i++)
                            {
                                columnName += rule[i];
                                rulesIndex++;
                            }
                            rulesIndex += 2;
                            for (int i = rulesIndex; i < rule.Length; i++)
                            {
                                

                                if (rule[i] != ' ')
                                {
                                    columnType += rule[i];
                                }
                                else
                                {
                                    break;
                                }
                            }
                            
                            string columnTypeForDataTable = "";
                            switch (columnType)
                            {
                                case "INTEGER":
                                    columnTypeForDataTable = "INTEGER";
                                    break;
                                case "TEXT":
                                    columnTypeForDataTable = "TEXT";
                                    break;
                                case "REAL":
                                    columnTypeForDataTable = "REAL";
                                    break;
                            }
                            columns.Add(columnName, columnTypeForDataTable);

                        }
                        else if (rule.StartsWith("FOREIGN KEY"))
                        {
                            string tmp = "";
                            int keysIndex = 12;
                            for (int i = 12; rule[i] != '"'; i++)
                            {
                                tmp += rule[i];
                                keysIndex++;
                            }
                            string tmpTable = "";
                            keysIndex += 14;
                            for (int i = keysIndex; rule[i] != '"'; i++)
                            {
                                tmpTable += rule[i];
                            }
                            keyTable.Add(tmp, tmpTable);
                        }
                    }

                    dataBaseRules.Add((d[2] as string), columns);
                    references.Add((d[2] as string), keyTable);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
