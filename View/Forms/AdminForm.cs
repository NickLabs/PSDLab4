using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using View.ViewInterfaces;

namespace View.Forms
{
    public partial class AdminForm : Form, IAdminForm
    {
        public event EventHandler materialChanged;
        public event EventHandler delete;
        public event EventHandler changeAdd;
        public event EventHandler changeUser;
        public event EventHandler submit;

        public string[] SelectedItemIndex
        {
            get
            {
                string[] mas = new string[2];
                mas[0] =TableView.SelectedRows[0].Cells[0].Value.ToString();
                try
                {
                    mas[1] = TableView.SelectedRows[0].Cells[1].Value.ToString();
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
                return TableView.SelectedRows.Count;
            }
        }
        private int numberOfParams;
        public string CurrentTable
        {
            get
            {
                return tablesList.Text;
            }
        }

        public ArrayList ValuesToSubmit
        {
            get
            {
                ArrayList tmp = new ArrayList();
                for (int i = 0; i < (DBRow.Controls.Count - 1) / 2; i++)
                {
                    tmp.Add(DBRow.Controls[i * 2 + 1].Text);
                }
                return tmp;
            }
            set
            {
                //У нас каждый второй элемент в DBRow - нужный контрол
                for (int i = 0; i < (DBRow.Controls.Count - 1) / 2; i++)
                {
                    DBRow.Controls[i * 2 + 1].Text = value[i].ToString();
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
            tablesList.Items.Clear();
            tablesList.Items.AddRange(names);
            Visible = true;
        }

        public void Stop()
        {
            Visible = false;
        }

        public void GenerateInputFields(string[] columnNames, string[] columnTypes, Dictionary<string, Dictionary<string, int>> columnReferencesTable)
        {
            numberOfParams = columnNames.Length;
            DBRow.Controls.Clear();
            int pad = 9;
            int forNames = 0;

            for (int i = 0; i < columnNames.Length; i++)
            {
                Label ColumnName = new Label();
                Control variableColumn;

                if (!columnNames[i].StartsWith("Ид"))
                {
                    variableColumn = new TextBox();

                }
                else if (columnNames[i].StartsWith("Иде"))
                {
                    variableColumn = new TextBox();
                    variableColumn.Leave += new EventHandler(UniqueID);
                }
                else
                {
                    variableColumn = new ComboBox();

                    //this.tablesList.Items.Clear();

                    (variableColumn as ComboBox).DropDownStyle = ComboBoxStyle.DropDownList;
                    (variableColumn as ComboBox).Items.AddRange(columnReferencesTable[columnNames[i]].Keys.ToArray());
                    if (columnNames.Contains("Ид "))
                    {
                        variableColumn.Leave += new EventHandler(UniqueCompositeKey);
                    }
                }

                int paddingSize = 100;
                if (columnNames.Length > 6)
                {
                    paddingSize = 80;
                }
                ColumnName.Location = new Point(pad, 20);
                ColumnName.Size = new Size(paddingSize, 40);
                ColumnName.Text = columnNames[i];
                variableColumn.Location = new Point(pad, 60);
                variableColumn.Size = new Size(paddingSize, 20);

                ColumnName.Name = "Label" + forNames;
                variableColumn.Name = columnNames[i];
                forNames++;

                if (columnTypes[i].Equals("INTEGER") && variableColumn.GetType().Equals(typeof(TextBox)))
                {
                    variableColumn.Leave += new EventHandler(ValidateInt);
                }
                else if (columnTypes[i].Equals("REAL") && variableColumn.GetType().Equals(typeof(TextBox)))
                {
                    variableColumn.Leave += new EventHandler(ValidateDouble);
                }

                pad += paddingSize;
                DBRow.Controls.Add(ColumnName);
                DBRow.Controls.Add(variableColumn);
            }
            Button Submit = new Button();
            Submit.Location = new Point(694, 37);
            Submit.Name = "Submit";
            Submit.Size = new Size(75, 23);
            Submit.TabIndex = 0;
            Submit.Text = "Ок";
            Submit.UseVisualStyleBackColor = true;
            Submit.Click += new EventHandler(SubmitRow);
            DBRow.Controls.Add(Submit);
        }

        public void SetChangeDeleteRules(bool isAllowed)
        {
            if (!isAllowed)
            {
                DBRow.Enabled = false;
                Modify.Enabled = false;
            }
            else
            {
                DBRow.Enabled = true;
                Modify.Enabled = true;
            }
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

                if (dt.Rows.Count < 1)
                {
                    Modify.Enabled = false;
                    Delete.Enabled = false;
                }
                else
                {
                    Modify.Enabled = true;
                    Delete.Enabled = true;
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
                    TableView.Rows.Add(list.ToArray());
                }

                if (dt.Rows.Count < 1)
                {
                    Modify.Enabled = false;
                    Delete.Enabled = false;
                }
                else
                {
                    Modify.Enabled = true;
                    Delete.Enabled = true;
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
            if (dt.Rows.Count < 1)
            {
                Modify.Enabled = false;
                Delete.Enabled = false;
            }
            else
            {
                Modify.Enabled = true;
                Delete.Enabled = true;
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
                    DBRow.Controls[DBRow.Controls.Count - 1].Enabled = true;
                }
                catch (Exception)
                {
                    string message = String.Format("В поле {0} должно быть введено целое неотрицательное число", (sender as TextBox).Name);
                    MessageBox.Show(message, "Неверный тип данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    DBRow.Controls[DBRow.Controls.Count - 1].Enabled = true;
                }
                catch (Exception)
                {
                    string message = String.Format("В поле {0} должно быть введено вещественное неотрицательное число", (sender as TextBox).Name);
                    MessageBox.Show(message, "Неверный тип данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UniqueID(object sender, EventArgs e)
        {
            var s = (sender as TextBox).Text;
            foreach (DataGridViewRow row in TableView.Rows)
            {
                if (row.Index < TableView.Rows.Count - 1)
                {
                    if (row.Cells[0].Value.ToString().Equals(s))
                    {
                        ShowNotUniqueIdError();
                        DBRow.Controls[DBRow.Controls.Count - 1].Enabled = false;
                    }
                }
            }
            DBRow.Controls[DBRow.Controls.Count - 1].Enabled = true;
        }

        private void UniqueCompositeKey(object sender, EventArgs e)
        {
            //Наверное лучше делать через презентер всё через ивент
            List<string> compositeKeyElements = new List<string>();
            if (!DBRow.Controls.ContainsKey("Ид "))
            {
                foreach (Control c in DBRow.Controls)
                {
                    if (c.Text.Contains("Ид "))
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
                    foreach (DataGridViewRow row in TableView.Rows)
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

        public void ShowSQLInjectionError()
        {
            string caption = "Предотвращзённая попытка порчи данных";
            string message = "Данная операция могла привести к потере данных, поэтому она была заблокирована системой";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            if (TableView.SelectedRows.Count == 1)
            {
                delete?.Invoke(this, null);
            }
            else
            {
                ShowChangeOrDeleteError();
            }
        }

        public void ChangeAddCirculation(string status, ArrayList values)
        {
            addChangeStatus.Text = status;

            if (status.Equals("Изменение"))
            {
                ValuesToSubmit = values;
                if (DBRow.Controls[1].Name.StartsWith("Ид "))
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
                ValuesToSubmit = values;

                DBRow.Controls[1].Enabled = true;
                DBRow.Controls[3].Enabled = true;
            }
        }

        public void UpdateTable()
        {
            TableView.Rows.Clear();
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти из данного меню?\nВсе несохранённые данные будут утрачены", "Подтвердите выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                changeUser?.Invoke(this, null);
            }
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для добавления записи в таблицу" +
                "\n\t1) выберите таблицу;" +
                "\n\t2) в поле 'выбранная строка' введите нужные данные и нажмите 'Ок'" +
                "\nДля изменения записи в таблице" +
                "\n\t1)Выберите нужную запись" +
                "\n\t2)Нажмите на кнопку 'Изменить/Добавить'" +
                "\n\t3)Измените данные и нажмите 'Ок'" +
                "\nДля удаления записи из таблицы" +
                "\n\t1)Выберите нужную запись" +
                "\n\t2)Нажмите на кнопку 'Удалить'", "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
               "Программу разработали студенты СПбГТИ(ТУ) 465 группы:\n" +
               "\tВинокуров Никита Александрович\n" +
               "\tТатаринцев Вадим Павлович\n" +
               "Под руководством:\n" +
               "\tПолосина Андрея Николаевича",
               "Справка",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }
    }
}
