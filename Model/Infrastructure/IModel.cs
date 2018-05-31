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
    public interface IModel
    {
        void CreateDb(string filepath);
        void OpenDb(string filepath);
        DataTable GetAccounts();
        void AddAccount(string name, string balance, string status, string registration_date);
    }
}
