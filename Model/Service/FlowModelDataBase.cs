using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;
using System.Data.SQLite;
using System.Data;

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
    }
}
