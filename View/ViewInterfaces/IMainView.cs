using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IMainView
    {
        void Start();
        void ShowAccounts(DataTable table);
        event EventHandler Add;
        event EventHandler Change;
        event EventHandler Delete;
        event EventHandler Open;
        event EventHandler New;
        event EventHandler Help;
    }
}
