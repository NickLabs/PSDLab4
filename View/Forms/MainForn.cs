using System;
using System.Windows.Forms;
using View.ViewInterfaces;

namespace View.Forms
{
    public partial class MainForn : Form, IMainView
    {
        public MainForn()
        {
            InitializeComponent();
        }
    }
}
