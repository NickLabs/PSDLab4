using System;
using System.Data;
using System.Windows.Forms;
using View.ViewInterfaces;

namespace View.Forms
{
    public partial class MainForm : Form, IMainView
    {
        #region Получение информации из ряда
        public string IdValue { get {
                if (BankGrid.SelectedRows[0].Cells["id"].Value != null)
                {
                    return BankGrid.SelectedRows[0].Cells["id"].Value.ToString();
                }
                else
                {
                    RowSelectionErrorMessage();
                    return "";
                }
            }
        }
        public string NameValue { get { return BankGrid.SelectedRows[0].Cells["NS"].Value.ToString(); } }
        public string BalanceValue { get { return BankGrid.SelectedRows[0].Cells["Account_money"].Value.ToString(); } }
        public string StatusValue { get { return BankGrid.SelectedRows[0].Cells["Status"].Value.ToString(); } }
        public string RegistrationDateValue { get { return BankGrid.SelectedRows[0].Cells["Creation_Date"].Value.ToString(); } }

        #endregion

        public event EventHandler Add;
        public event EventHandler Change;
        public event EventHandler Delete;
        public event EventHandler Open;
        public event EventHandler New;
        public event EventHandler Help;

        public void ShowAccounts(DataTable table)
        {
            BankGrid.Rows.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                BankGrid.Rows.Add(table.Rows[i].ItemArray);
            }
        }

        #region работа с откртыием и созданием баз
        public string OpenFileDialogForCreatingDB()
        {
            FileDialog fd = new SaveFileDialog();
            fd.Filter = "Database Files| *.db";
            fd.AddExtension = false;

            string finalPath = "";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                finalPath = fd.FileName;
            }
            return finalPath;
        }

        public string OpenFileDialogForChangingDB()
        {
            FileDialog fd = new OpenFileDialog();
            fd.Filter = "Database Files| *.db";
            fd.AddExtension = false;
            
            string finalPath = "";
            if(fd.ShowDialog() == DialogResult.OK)
            {
                finalPath = fd.FileName;
            }
            return finalPath;
        }
        #endregion

        #region вывод ошибок и приветствие
        public void RowSelectionErrorMessage()
        {
            string caption = "Ошибка при попытке изменить счёт";
            string message = "Для изменения параметров счёта необходимо выбрать только один аккаунт.\nНи больше, ни меньше!";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }

        public void WrongFileSelection()
        {
            string caption = "Ошибка при открытии файла";
            string message = "Нужно указать путь к валидному файлу базы данных";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }

        public void Greetings()
        {
            string caption = "Здравствуй";
            string message = "Данная программа представляет собой симуляцию хранения банковских счетов\n"+
                "Можно создавать, изменять и удалять информацию об аккаунтах, а также создавать и открывать новые экземпляры"+
                "Программа была написана студентов 465 группы Винокуровым Никитой Александровичем";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }
        #endregion

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
            if (BankGrid.SelectedRows.Count == 1)
            {
                Change?.Invoke(this, null);
            }
            else
            {
                RowSelectionErrorMessage();
            }
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
