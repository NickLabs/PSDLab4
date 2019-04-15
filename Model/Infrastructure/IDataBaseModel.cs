using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

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
        Dictionary<string, int> IdAndRelevantNames(string tableName);
    }
}
