/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

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
    /// Interaction logic for ListSearchWindow.xaml
    /// </summary>
    public partial class ListSearchWindow : Window
    {
        CaseManager cm = new CaseManager();

        public string SearchCriteria { get; set; }

        public ListSearchWindow()
        {
            InitializeComponent();
        }

        public void ListByStatus()
        {

            this.dgCases.ItemsSource = cm.SearchResultStatus(SearchCriteria);

        }//End listByStatus

        public void ListByCategory()
        {
            this.dgCases.ItemsSource = cm.SearchResultCategory(SearchCriteria);

        }//End ListByCategory

        public void ListByCaseID()
        {
            int id = int.Parse(SearchCriteria);
            this.dgCases.ItemsSource = cm.SearchResultCaseID(id);
        }//End listByCaseID

        public void ListByAssigne()
        {
            this.dgCases.ItemsSource = cm.SearchResultAssigne(SearchCriteria);

        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selected = dgCases.SelectedIndex;
                if (selected < 0)
                {
                    MessageBox.Show("You must select a case in the list to open");
                }
                else
                {
                    Case c = new Case();
                    CaseWindow cw = new CaseWindow();
                    cw.openCase(((Case)dgCases.SelectedItem).CaseID);
                    cw.comboBoxCreateBy.IsReadOnly = true;

                    cw.ShowDialog();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured \n" + ex.Message);
            }
        }

        private void btnNewSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow();
            sw.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//ned ListByAssigne
    }
}
