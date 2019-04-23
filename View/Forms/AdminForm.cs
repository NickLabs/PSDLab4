using System;
using System.Collections;
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
        public event EventHandler delete;
        public event EventHandler changeAdd;
        public event EventHandler submit;

        public int[] SelectedItemIndex
        {
            get
            {
                int[] mas = new int[2];
                mas[0] = Convert.ToInt32(this.TableView.SelectedRows[0].Cells[0].Value);
                try
                {
                    mas[1] = Convert.ToInt32(this.TableView.SelectedRows[0].Cells[1].Value);
                }
                catch (Exception)
                {

                }
                return mas;
            }
        }

        public int NumberOfSelectedRows
        {
            get
            {
                return this.TableView.SelectedRows.Count;
            }
        }
        private int numberOfParams;
        public string CurrentTable
        {
            get
            {
                return this.tablesList.Text;
            }
        }

        public ArrayList ValuesToSubmit
        {
            get
            {
                ArrayList tmp = new ArrayList();
                for (int i = 0; i < (this.DBRow.Controls.Count - 1) / 2; i++)
                {
                    tmp.Add(this.DBRow.Controls[i * 2 + 1].Text);
                }
                return tmp;
            }
            set
            {
                //У нас каждый второй элемент в DBRow - нужный контрол
                for (int i = 0; i < (this.DBRow.Controls.Count - 1) / 2; i++)
                {
                    this.DBRow.Controls[i * 2 + 1].Text = value[i].ToString();
                }
            }
        }

        public ArrayList ValuesToInsert
        {
            get
            {
                ArrayList tmp = new ArrayList();
                DataGridViewRow s = TableView.SelectedRows[0];
                foreach (DataGridViewCell c in s.Cells)
                {
                    tmp.Add(c.Value);
                }
                return tmp;
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

        public void GenerateInputFields(string[] columnNames, string[] columnTypes, Dictionary<string, Dictionary<string, int>> columnReferencesTable)
        {
            this.numberOfParams = columnNames.Length;
            this.DBRow.Controls.Clear();
            int pad = 9;
            int forNames = 0;

            for (int i = 0; i < columnNames.Length; i++)
            {
                Label ColumnName = new Label();
                Control variableColumn;

                if (!columnNames[i].StartsWith("id"))
                {
                    variableColumn = new TextBox();

                }
                else if (columnNames[i].Length < 3)
                {
                    variableColumn = new TextBox();
                    variableColumn.Leave += new System.EventHandler(UniqueID);
                }
                else
                {
                    variableColumn = new ComboBox();
                    (variableColumn as ComboBox).DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    (variableColumn as ComboBox).Items.AddRange(columnReferencesTable[columnNames[i]].Keys.ToArray());
                    if (!columnNames.Contains("id"))
                    {
                        variableColumn.Leave += new System.EventHandler(UniqueCompositeKey);
                    }
                }

                int paddingSize = 100;
                if (columnNames.Length > 6)
                {
                    paddingSize = 80;
                }
                ColumnName.Location = new System.Drawing.Point(pad, 20);
                ColumnName.Size = new System.Drawing.Size(paddingSize, 20);
                ColumnName.Text = columnNames[i];
                variableColumn.Location = new System.Drawing.Point(pad, 40);
                variableColumn.Size = new System.Drawing.Size(paddingSize, 20);

                ColumnName.Name = "Label" + forNames;
                variableColumn.Name = columnNames[i];
                forNames++;

                if (columnTypes[i].Equals("INTEGER") && variableColumn.GetType().Equals(typeof(TextBox)))
                {
                    variableColumn.Leave += new System.EventHandler(ValidateInt);
                }
                else if (columnTypes[i].Equals("REAL") && variableColumn.GetType().Equals(typeof(TextBox)))
                {
                    variableColumn.Leave += new System.EventHandler(ValidateDouble);
                }

                pad += paddingSize;
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
            Submit.Click += new System.EventHandler(SubmitRow);
            this.DBRow.Controls.Add(Submit);
        }

        public void SetColumnNames(string[] columnNames)
        {
            TableView.Columns.Clear();
            foreach (string name in columnNames)
            {
                TableView.Columns.Add(name, name);
            }
        }

        public void SetData(DataTable dt, Dictionary<string, Dictionary<int, string>> columnReferencesTableKeyNames)
        {
            TableView.Rows.Clear();
            if (columnReferencesTableKeyNames.Count < 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TableView.Rows.Add(dr.ItemArray);
                }
            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    List<object> list = new List<object>();
                    List<string> columns = new List<string>();
                    List<string> refernceColumns = new List<string>();

                    foreach(var keyName in columnReferencesTableKeyNames)
                    {
                        refernceColumns.Add(keyName.Key);
                    }
                    for (int i = 0; i < TableView.Columns.Count; i++)
                    {
                        columns.Add(TableView.Columns[i].Name);
                    }

                    for(int i = 0; i < columns.Count; i++)
                    {
                        if (refernceColumns.Contains(columns[i]))
                        {
                            list.Add(columnReferencesTableKeyNames[columns[i]][Convert.ToInt32(dr.ItemArray[i])]);
                        }
                        else
                        {
                            list.Add(dr.ItemArray[i]);
                        }
                    }

                    

                    for(int i = 0; i < dr.ItemArray.Length; i++)
                    {
                        dr.ItemArray.SetValue(list[i], i);
                    }

                    dr.ItemArray = list.ToArray();

                    foreach(object o in list)
                    {
                        MessageBox.Show(o.ToString());
                    }
                    

                    TableView.Rows.Add(dr.ItemArray);
                }
            }
        }

        public void SetData(DataTable dt, Dictionary<string, Dictionary<string, int>> columnReferencesTable)
        {
            //columnReferencesTable[columnNames[i]].Keys.ToArray()
            TableView.Rows.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                TableView.Rows.Add(dr.ItemArray);
            }
        }

        private void TableChanged(object sender, EventArgs e)
        {
            materialChanged?.Invoke(this, null);
        }

        private void SubmitRow(object sender, EventArgs e)
        {
            ArrayList tmp = new ArrayList();
            foreach (Control c in DBRow.Controls)
            {
                if (c.Text.Equals(""))
                {
                    MessageBox.Show("Должны быть заполнены все поля", "Пустые поля", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (c.GetType().Equals(typeof(ComboBox)) || c.GetType().Equals(typeof(TextBox)))
                {
                    tmp.Add(c.Text);
                }
            }

            ValuesToSubmit = tmp;
            submit?.Invoke(this, null);
        }

        private void ModifyClick(object sender, EventArgs e)
        {
            changeAdd?.Invoke(this, null);
        }

        private void ValidateInt(object sender, EventArgs e)
        {
            if (!(sender as TextBox).Text.Equals(""))
            {
                try
                {
                    int s = Convert.ToInt32((sender as TextBox).Text);
                    if (s < 0) throw new Exception();
                    this.DBRow.Controls[this.DBRow.Controls.Count - 1].Enabled = true;
                }
                catch (Exception)
                {
                    string message = String.Format("В поле {0} должно быть введено целое неотрицательное число", (sender as TextBox).Name);
                    MessageBox.Show(message, "Неверный тип данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DBRow.Controls[this.DBRow.Controls.Count - 1].Enabled = false;
                }
            }
        }

        private void ValidateDouble(object sender, EventArgs e)
        {

            if (!(sender as TextBox).Text.Equals(""))
            {
                try
                {
                    double s = Convert.ToDouble((sender as TextBox).Text);
                    if (s < 0.0) throw new Exception();
                    this.DBRow.Controls[this.DBRow.Controls.Count - 1].Enabled = true;
                }
                catch (Exception)
                {
                    string message = String.Format("В поле {0} должно быть введено вещественное неотрицательное число", (sender as TextBox).Name);
                    MessageBox.Show(message, "Неверный тип данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DBRow.Controls[this.DBRow.Controls.Count - 1].Enabled = false;
                }
            }
        }

        private void UniqueID(object sender, EventArgs e)
        {
            var s = (sender as TextBox).Text;
            foreach (DataGridViewRow row in this.TableView.Rows)
            {
                if (row.Index < TableView.Rows.Count - 1)
                {
                    if (row.Cells[0].Value.ToString().Equals(s))
                    {
                        ShowNotUniqueIdError();
                        this.DBRow.Controls[this.DBRow.Controls.Count - 1].Enabled = false;
                    }
                }
            }
            this.DBRow.Controls[this.DBRow.Controls.Count - 1].Enabled = true;
        }

        private void UniqueCompositeKey(object sender, EventArgs e)
        {
            //Наверное лучше делать через презентер всё через ивент
            List<string> compositeKeyElements = new List<string>();
            if (!this.DBRow.Controls.ContainsKey("id"))
            {
                foreach (Control c in this.DBRow.Controls)
                {
                    if (c.Text.Contains("id"))
                    {
                        compositeKeyElements.Add(c.Text);
                    }
                    else
                    {
                        break;
                    }
                }
                if (compositeKeyElements.Count > 1)
                {
                    List<List<string>> dataInTable = new List<List<string>>();
                    foreach (DataGridViewRow row in this.TableView.Rows)
                    {
                        List<string> tmp = new List<string>();
                        for (int i = 0; i < compositeKeyElements.Count; i++)
                        {
                            tmp.Add(row.Cells[i].Value.ToString());
                        }
                        dataInTable.Add(tmp);
                    }

                    foreach (List<string> s in dataInTable)
                    {
                        bool[] isUnique = { true, true, true };
                        for (int i = 0; i < s.Count; i++)
                        {
                            if (!compositeKeyElements[i].Equals(s[i]))
                            {
                                break;
                            }
                            else
                            {
                                isUnique[i] = false;
                            }
                        }
                        if (isUnique.Contains(true))
                        {
                            continue;
                        }
                        else
                        {
                            ShowNotUniqueIdError();
                            break;
                        }
                    }
                }
            }
        }

        private void ShowNotUniqueIdError()
        {
            string caption = "Не уникальный ID";
            string message = "В данной таблице уже есть поле с таким ID\nВ случае составного ID должна быть уникальная комбинация";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowChangeOrDeleteError()
        {
            string caption = "Ошибка при попытке манипуляции данными таблицы";
            string message = "Для изменения параметров таблицы или их удаления необходимо выбрать только один ряд.";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            if (this.TableView.SelectedRows.Count == 1)
            {
                this.delete?.Invoke(this, null);
            }
            else
            {
                ShowChangeOrDeleteError();
            }
        }

        public void ChangeAddCirculation(string status, ArrayList values)
        {
            this.addChangeStatus.Text = status;

            if (status.Equals("Изменение"))
            {
                this.ValuesToSubmit = values;
                if (!DBRow.Controls[1].Name.Equals("id"))
                {
                    DBRow.Controls[1].Enabled = false;
                    DBRow.Controls[3].Enabled = false;
                }
                else
                {
                    DBRow.Controls[1].Enabled = false;
                }
            }
            else
            {
                this.ValuesToSubmit = values;

                DBRow.Controls[1].Enabled = true;
                DBRow.Controls[3].Enabled = true;
                this.DBRow.Controls[this.DBRow.Controls.Count - 1].Enabled = false;
            }
        }

        public void UpdateTable()
        {
            this.TableView.Rows.Clear();
        }
    }
}
