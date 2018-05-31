using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;
using System.IO;

namespace DomainModel.Service
{
    public class Model : IModel
    {
        private string dbName;
        private static SQLiteConnection sqlConnection;
        private static SQLiteCommand sqlCommand;

        public DataTable GetAccounts()
        {
            DataTable table = new DataTable();
            string sqlQuery = "Select * FROM Catalog";

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, sqlConnection);
            adapter.Fill(table);

            return table;
        }

        public void AddAccount(params string[] options)
        {

        }

        public void CreateDb(string filepath)
        {
            SQLiteConnection.CreateFile(filepath);
            OpenDb(filepath);
        }

        public void OpenDb(string filepath)
        {
            dbName = filepath;
            sqlCommand = new SQLiteCommand();
            sqlConnection = new SQLiteConnection("Data Source=" + filepath);
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "author TEXT, book TEXT)";
            sqlCommand.ExecuteNonQuery();
        }

        public Model()
        {
            dbName = ConfigurationManager.AppSettings["database"];
            if (!File.Exists(dbName))
            {
                CreateDb(dbName);
            }
            else
            {
                OpenDb(dbName);
            }

        }
    }
}
