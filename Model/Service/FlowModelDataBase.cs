using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;
using System.Data.SQLite;
using System.Data;
using System.Collections;
using System.Data.Common;

namespace DomainModel.Service
{
    public class FlowModelDataBase : IDataBaseModel
    {
        private readonly SQLiteConnection connection;
        private SQLiteCommand command = new SQLiteCommand();
        private string dbName = @"Data Source=C:\Users\prais\4.db;foreign keys=true;";
        private Dictionary<string, bool> DatabaseRules = new Dictionary<string, bool>();
        private string[] dropVariaties = { "drop ", "droP ", "drOp ", "drOP ", "dRop ", "dRoP ", "dROp ", "dROP ", "Drop ", "DroP ", "DrOp ", "DrOP ", "DRop ", "DRoP ", "DROp ", "DROP " };

        public bool GetRule(string tableName)
        {
            return DatabaseRules[tableName];
        }

        public FlowModelDataBase()
        {
            this.connection = new SQLiteConnection(dbName);
            this.connection.Open();
            command.Connection = connection;
        }

        public void DeleteRow(string table, int id)
        {
            string query = String.Format("DELETE FROM {0} WHERE id={1}", table, id);
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        public void DeleteRow(string table, int id1, int id2, string column1, string column2)
        {
            string query = String.Format("DELETE FROM {0} WHERE {1}={2} AND {3}={4}", table, id1, column1, id2, column2);
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        public bool[] DoesUserExist(string login, string password)
        {
            throw new NotImplementedException();
        }

        public double[] FetchAllCoefficients(string materialName)
        {
            throw new NotImplementedException();
        }

        public double[] FetchAllProperties(string materialName)
        {
            throw new NotImplementedException();
        }

        public double[] FetchLimitsMax(int idMaterial)
        {
            throw new NotImplementedException();
        }

        public double[] FetchLimitsMin(int idMaterial)
        {
            throw new NotImplementedException();
        }

        public string[] GetAllMaterials()
        {
            throw new NotImplementedException();
        }

        public string[] GetAllTables()
        {
            List<string> tables= new List<string>();
            DataTable dt = this.connection.GetSchema("Tables");
            
            foreach(DataRow row in dt.Rows)
            {
                tables.Add(row[2].ToString());
            }

            tables.Remove("sqlite_sequence");
            DatabaseRules = tables.ToDictionary(x => x, x => true);
            DatabaseRules["experiment"] = false;
            DatabaseRules["canal"] = false;
            DatabaseRules["expers_variables"] = false;
            return tables.ToArray();
        }

        public int GetMaterialIdViaName(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable GetTableData(string tableName)
        {
            DataTable dt = new DataTable();
            string query = String.Format("SELECT * FROM {0}", tableName);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, this.connection);
            adapter.Fill(dt);
            return dt;
        }

        public int GetUserIdViaLogin(string login)
        {
            throw new NotImplementedException();
        }

        public string GetUserLoginPassViaId(int id)
        {
            throw new NotImplementedException();
        }

        public string GetUserNameViaId(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> IdAndRelevantNames(string tableName, string[] columnNames)
        {
            string namePresenter = "";
            string query = "";
            Dictionary<string, int> nameId = new Dictionary<string, int>();

            foreach (string n in columnNames)
            {
                if (n.Contains("name") || n.Contains("Name"))
                {
                    namePresenter = n;
                    break;
                }
            }

            if (namePresenter.Equals(""))
            {
                bool ableToMakeName = true;
                foreach (string n in columnNames)
                {
                    if (n.Length > 3 && n.StartsWith("id"))
                    {
                        ableToMakeName = false;
                        break;
                    }
                }
                if (!ableToMakeName)
                {
                    query = String.Format("SELECT id, id from {0}", tableName);

                    nameId = new Dictionary<string, int>();
                    command.CommandText = query;
                    SQLiteDataReader reader = command.ExecuteReader();
                    foreach (DbDataRecord record in reader)
                    {
                        int id = Convert.ToInt32(record[0].ToString());
                        string name = record[1].ToString();
                        nameId[name] = id;
                    }
                    reader.Close();
                }
                else
                {
                    query = String.Format("SELECT * from {0}", tableName);

                    nameId = new Dictionary<string, int>();
                    command.CommandText = query;
                    SQLiteDataReader reader = command.ExecuteReader();
                    foreach (DbDataRecord record in reader)
                    {
                        int id = Convert.ToInt32(record[0].ToString());
                        string name = "";
                        for (int i = 1; i < record.FieldCount; i++)
                        {
                            name += record[i];
                            if (i + 1 != record.FieldCount)
                            {
                                name += "*";
                            }
                        }
                        nameId[name] = id;
                    }
                    reader.Close();
                }
            }
            else
            {
                query = String.Format("SELECT id, {0} from {1}", namePresenter, tableName);

                nameId = new Dictionary<string, int>();  
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    int id = Convert.ToInt32(record[0].ToString());
                    string name = record[1].ToString();
                    nameId[name] = id;
                }
                reader.Close();
            }
            return nameId;
        }


        public Dictionary<int, string> IdAndRelevantNameReverse(string tableName, string[] columnNames)
        {
            string namePresenter = "";
            string query = "";
            Dictionary<int, string> idName = new Dictionary<int, string>();

            foreach (string n in columnNames)
            {
                if (n.Contains("name") || n.Contains("Name"))
                {
                    namePresenter = n;
                    break;
                }
            }

            if (namePresenter.Equals(""))
            {
                bool ableToMakeName = true;
                foreach (string n in columnNames)
                {
                    if (n.Length < 3 && n.StartsWith("id"))
                    {
                        ableToMakeName = false;
                        break;
                    }
                }
                if (!ableToMakeName)
                {
                    query = String.Format("SELECT id, id from {0}", tableName);

                    idName = new Dictionary<int, string>();
                    command.CommandText = query;
                    SQLiteDataReader reader = command.ExecuteReader();
                    foreach (DbDataRecord record in reader)
                    {
                        int id = Convert.ToInt32(record[0].ToString());
                        string name = record[1].ToString();
                        idName[id] = name;
                    }
                    reader.Close();
                }
                else
                {
                    query = String.Format("SELECT * from {0}", tableName);

                    idName = new Dictionary<int, string>();
                    command.CommandText = query;
                    SQLiteDataReader reader = command.ExecuteReader();
                    foreach (DbDataRecord record in reader)
                    {
                        int id = Convert.ToInt32(record[0].ToString());
                        string name = "";
                        for(int i = 1; i < record.FieldCount; i++)
                        {
                            name += record[i];
                            if(i+1 != record.FieldCount)
                            {
                                name += "*";
                            }
                        }
                        idName[id] = name;
                    }
                    reader.Close();
                }
            }
            else
            {
                query = String.Format("SELECT id, {0} from {1}", namePresenter, tableName);

                idName = new Dictionary<int, string>();
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    int id = Convert.ToInt32(record[0].ToString());
                    string name = record[1].ToString();
                    idName[id] = name;
                }
                reader.Close();
            }
            return idName;
        }

        public void InsertRow(string table, ArrayList values)
        {
            CheckInjection(values);

            string query = String.Format("insert into {0} values (", table);
            for(int i = 0; i < values.Count; i++)
            {
                if (i != values.Count - 1)
                {
                    query += "'" + values[i].ToString() + "',";
                }
                else
                {
                    query += "'" + values[i].ToString() + "')";
                }
            }
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        public void UpdateRow(string table, ArrayList values, string[] columnNames)
        {
            CheckInjection(values);

            string query = String.Format("Update {0} set ", table);

            for (int i = 0; i < values.Count; i++)
            {
                if (i != values.Count - 1)
                {
                    query += columnNames[i] + "=" +values[i].ToString() + ",";
                }
                else
                {
                    query += columnNames[i] + "=" + values[i].ToString();
                }
            }
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private void CheckInjection(ArrayList values)
        {
            foreach (object val in values)
            {
                foreach (string s in dropVariaties)
                {
                    if (val.ToString().Contains(s))
                    {
                        throw new Exception();
                    }
                }
            }
        }
    }
}
