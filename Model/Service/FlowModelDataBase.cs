using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;
using System.Data.SQLite;
using System.Data;
using System.Collections;

namespace DomainModel.Service
{
    public class FlowModelDataBase : IDataBaseModel
    {
        private readonly SQLiteConnection connection;
        private SQLiteCommand command = new SQLiteCommand();
        private string dbName = "Data Source=C:\\Users\\prais\\AVPIoO.db";

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
            throw new NotImplementedException();
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

        public Dictionary<string, int> IdAndRelevantNames(string tableName)
        {
            return null;
        }

        public void InsertRow(string table, ArrayList values)
        {
            string query = String.Format("insert into {0} values (", table);
            for(int i = 0; i < values.Count; i++)
            {
                if (i != values.Count - 1)
                {
                    query += values[i].ToString() + ",";
                }
                else
                {
                    query += values[i].ToString() + ")";
                }
            }
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        public void UpdateRow(string table, ArrayList values, string[] columnNames)
        {
            string query = String.Format("Update {0} set", table);

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
    }
}
