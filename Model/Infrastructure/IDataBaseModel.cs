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
        bool[] doesUserExist(string login, string password);
        int getUserIdViaLogin(string login);
        string getUserLoginPassViaId(int id);
        string getUserNameViaId(int id);
        string[] getAllMaterials();
        double[] fetchAllProperties(string materialName);
        double[] fetchAllCoefficients(string materialName);
    }
}
