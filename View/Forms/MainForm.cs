using System;
using System.Data;
using System.Windows.Forms;
using View.ViewInterfaces;

namespace View.Forms
{
    public partial class MainForm : Form, IMainView
    {
        public event EventHandler Add;
        public event EventHandler Change;
        public event EventHandler Delete;
        public event EventHandler Open;
        public event EventHandler New;
        public event EventHandler Help;

        public void ShowAccounts(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                BankGrid.Rows.Add(table.Rows[i].ItemArray);
            }
        }

        public MainForm()
        {
            InitializeComponent();
            Open?.Invoke(this, null);
        }

        #region для запуска приложения через презентер
        public void Start()
        {
            this.Show();
        }

        public new void Show()
        {
            Application.Run(this);
        }
        #endregion

        #region Бинд событий на нажатие кнопок
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Delete?.Invoke(this, null);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Add?.Invoke(this, null);
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            Change?.Invoke(this, null);
        }

        private void NewFileButton_Click(object sender, EventArgs e)
        {
            New?.Invoke(this, null);
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            Open?.Invoke(this, null);
        }

        private void HelpMenuButton_Click(object sender, EventArgs e)
        {
            Help?.Invoke(this, null);
        }
        #endregion


    }
}
