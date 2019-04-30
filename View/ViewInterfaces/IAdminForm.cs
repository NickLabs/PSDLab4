using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IAdminForm
    {
        string CurrentTable { get; }
        ArrayList ValuesToSubmit { get; set; }
        ArrayList ValuesToInsert { get; }
        int NumberOfSelectedRows { get; }
        int[] SelectedItemIndex { get; }
        event EventHandler materialChanged;
        event EventHandler delete;
        event EventHandler changeAdd;
        event EventHandler submit;
        void SetChangeDeleteRules(bool isAllowed);
        void UpdateTable();
        void ShowChangeOrDeleteError();
        void ShowSQLInjectionError();
        void SetColumnNames(string[] columns);
        void SetData(DataTable data, Dictionary<string, Dictionary<int, string>> columnReferencesTableKeyNames);
        void SetData(DataTable data, Dictionary<string, Dictionary<string, int>> columnReferencesTable);
        void ChangeAddCirculation(string status, ArrayList values);
        void Start(string[] tableNames);
        void GenerateInputFields(string[] columnNames, string[] columnTypes, Dictionary<string, Dictionary<string, int>> columnReferencesTable);
    }
}
