using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View.ViewInterfaces;

namespace View.Forms
{
    public partial class AdminForm : Form, IAdminForm
    {
        public event EventHandler materialChanged;

        public string currentMaterial
        {
            get
            {
                return this.tablesList.Text;
            }
        }

        public AdminForm()
        {
            InitializeComponent();
        }

        public void Start(string[] names)
        {
            this.tablesList.Items.AddRange(names);
            Application.Run(this);
        }

        public void GenerateInputFields(string[] columnNames, string[] columnTypes, Dictionary<string, Dictionary<string, Dictionary<string, int>>> columnReferencesTable)
        {
            this.DBRow.Controls.Clear();
            int pad = 9;
            int forNames = 0;
            for(int i = 0; i < columnNames.Length; i++) 
            {
                Label ColumnName = new Label();
                Label ColumnType = new Label();
                Control variableColumn;
                /*
                switch (columnTypes[i])
                {
                    case "INTEGER":
                        ColumnType.Text = columnTypes[i];
                        break;
                    case "REAL":
                        ColumnType.Text = columnTypes[i];
                        break;
                    case "TEXT":
                        ColumnType.Text = columnTypes[i];
                        break;
                }*/

                if (!columnNames[i].StartsWith("id"))
                {
                    variableColumn = new TextBox();
                } else if(columnNames[i].Length < 3)
                {
                    variableColumn = new TextBox();
                }
                else
                {
                    variableColumn = new ComboBox();
                }

                ColumnName.Location = new System.Drawing.Point(pad, 20);
                ColumnName.Size = new System.Drawing.Size(100, 20);
                ColumnName.Text = columnNames[i];
                variableColumn.Location = new System.Drawing.Point(pad, 40);
                variableColumn.Size = new System.Drawing.Size(100, 20);
                ColumnName.Name = "Label" + forNames;
                forNames++;
                variableColumn.Name = "Box" + forNames;
                forNames++;

                pad += 100;
                this.DBRow.Controls.Add(ColumnName);
                this.DBRow.Controls.Add(variableColumn);
            }
            Button Submit = new Button();
            Submit.Location = new System.Drawing.Point(694, 31);
            Submit.Name = "Submit";
            Submit.Size = new System.Drawing.Size(75, 23);
            Submit.TabIndex = 0;
            Submit.Text = "Ок";
            Submit.UseVisualStyleBackColor = true;
            this.DBRow.Controls.Add(Submit);
            //Сюда вставляем обработчик события по кнопке
        }

        private void TableChanged(object sender, EventArgs e)
        {
            materialChanged?.Invoke(this, null);
        }
    }
}
