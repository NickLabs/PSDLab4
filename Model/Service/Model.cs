using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
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
            string sqlQuery = "Select * FROM Accounts";

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, sqlConnection);
            adapter.Fill(table);

            return table;
        }

        public void AddAccount(string name, string balance, string status, string registration_date)
        {
            
            string sqlQuery = string.Format("INSERT INTO Accounts (fullname, balance, status, registration_date) values('{0}','{1}','{2}','{3}')",
                name, balance, status, registration_date);
            
            sqlCommand.CommandText = sqlQuery;
            sqlCommand.ExecuteNonQuery();
        }

        public void ChangeAccount(int id, string name, string balance, string status, string registration_date)
        {
            string sqlQuery = string.Format("UPDATE Accounts SET fullname = '{0}', balance='{1}',status='{2}',registration_date ='{3}' WHERE id='{4}'",
                name, balance, status, registration_date, id);

            sqlCommand.CommandText = sqlQuery;
            sqlCommand.ExecuteNonQuery();
        }

        public void DeleteAccount(int id)
        {
            string sqlQuery = string.Format("Delete FROM Accounts WHERE id='{0}'", id);
            sqlCommand.CommandText = sqlQuery;
            sqlCommand.ExecuteNonQuery();
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

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS Accounts (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "fullname TEXT, balance TEXT, status TEXT, registration_date TEXT)";
            sqlCommand.ExecuteNonQuery();
        }

        public Model()
        {
            dbName = "first.db";// ConfigurationManager.AppSettings["database"];
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
