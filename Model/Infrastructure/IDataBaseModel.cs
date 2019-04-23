using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Collections;

namespace DomainModel.Infrastructure
{
    public interface IDataBaseModel
    {
        bool[] DoesUserExist(string login, string password);
        int GetUserIdViaLogin(string login);
        string GetUserLoginPassViaId(int id);
        string GetUserNameViaId(int id);
        string[] GetAllMaterials();
        string[] GetAllTables();
        int GetMaterialIdViaName(string name);
        double[] FetchAllProperties(string materialName);
        double[] FetchAllCoefficients(string materialName);
        double[] FetchLimitsMin(int idMaterial);
        double[] FetchLimitsMax(int idMaterial);
        void InsertRow(string table, ArrayList values);
        void UpdateRow(string table, ArrayList values,string[] columnNames);
        void DeleteRow(string table, int id);
        void DeleteRow(string table, int id1, int id2, string column1, string column2);
        Dictionary<string, int> IdAndRelevantNames(string tableName, string[] columnNames);
        //Dictionary<int, string> IdAndRelevantNames(string tableName, string[] columnNames);
        DataTable GetTableData(string tableName);
    }
}
