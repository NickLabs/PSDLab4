using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IAdminForm
    {
        string currentMaterial { get; }
        event EventHandler materialChanged;
        void Start(string[] tableNames);
        void GenerateInputFields(string[] columnNames, string[] columnTypes, Dictionary<string, Dictionary<string, Dictionary<string, int>>> columnReferencesTable);
    }
}
