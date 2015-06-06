using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CaseManager cm = new CaseManager();
        

        public MainWindow()
        {
            InitializeComponent();
            LoadCases();

            CultureInfo.CreateSpecificCulture("en-US");
        }

        private void LoadCases()
        {
            cm.loadFromDatabase();
            this.DgCases.ItemsSource = cm.a_List;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            CaseWindow cw = new CaseWindow();
            cw.ShowDialog();
            LoadCases();
        }

        private void btnOpenCase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selected = DgCases.SelectedIndex;
                if (selected < 0)
                {
                    MessageBox.Show("You must select a case in the list to open");
                }
                else
                {
                    Case c = new Case();
                    CaseWindow cw = new CaseWindow();
                    cw.openCase(((Case)DgCases.SelectedItem).CaseID);
                    cw.comboBoxCreateBy.IsReadOnly = true;
                    
                    cw.ShowDialog();
                }
                LoadCases();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occured \n" + ex.Message);
            }


            
        }//ENd method
    }
}
