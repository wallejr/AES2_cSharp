using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AES
{
    /// <summary>
    /// Interaction logic for CaseWindow.xaml
    /// </summary>
    public partial class CaseWindow : Window
    {
        CaseManager cm = new CaseManager();

        public CaseWindow()
        {
            InitializeComponent();
            initGUI();
        }
      

        private void initGUI()
        {
            txtBoxCaseID.Text = "";
            txtBoxCreated.Text = "";
            txtBoxChanged.Text = "";
            txtBoxUserName.Text = "";
            txtBoxFullName.Text = "";
            txtBoxPhone.Text = "";
            txtBoxCity.Text = "";
            txtBoxCompName.Text = "";
            txtBoxCreatedBy.Text = "";
            txtBoxTitel.Text = "";
            txtBoxCaseDesc.AppendText("");
            txtBoxComments.AppendText("");
            txtBoxSolution.AppendText("");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveNewCase();
        }

        private void saveNewCase()
        {

        }


    }
}
