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

        }

        public MainForm()
        {
            InitializeComponent();
        }

        public void Start()
        {
            this.Show();
        }
        
        public new void Show()
        {
            Application.Run(this);
        }
    }
}
