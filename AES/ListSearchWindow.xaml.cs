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
        /// <summary>
        /// Instance variables, an object of the casemanager
        /// and a public propertie that will hold information about
        /// the search from the searchwindow
        /// </summary>
        private CaseManager cm = new CaseManager();

        public string SearchCriteria { get; set; }

        public ListSearchWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Methods below will be called from the searchwindow depending on the searchalternative choosen
        /// The metod will update the Datagrid with cases depending on the call of method in the CaseManager Class
        /// </summary>
        public void ListByStatus()
        {
            try
            {
                this.dgCases.ItemsSource = cm.SearchResultStatus(SearchCriteria);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occure while searching by Status: \n" + ex.Message);
            }

        }//End listByStatus

        public void ListByCategory()
        {
            try
            {
                this.dgCases.ItemsSource = cm.SearchResultCategory(SearchCriteria);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while searching by category: \n" + ex.Message);
            }

        }//End ListByCategory

        public void ListByCaseID()
        {
            try
            {
                int id = int.Parse(SearchCriteria);
                this.dgCases.ItemsSource = cm.SearchResultCaseID(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while searching by caseid\nAre you sure there is a caseid with id: " + SearchCriteria + "\n" + ex.Message);
            }
        }//End listByCaseID

        public void ListByAssigne()
        {
            try
            {
                this.dgCases.ItemsSource = cm.SearchResultAssigne(SearchCriteria);
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occured while searching by assigne \n" + ex.Message);
            }

        }

        /// <summary>
        /// Method that that will respond when the user hits the open case button.
        /// The method will initalize an object of the Case class och  CaseWindow class
        /// to get case information and fíll the Casewindow with information beforere showing it
        /// tot the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event method that will respond when the user hits the new search button.
        /// Initialize and display an object of the searchwindow and closing the resultsearch window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchWindow sw = new SearchWindow();
                sw.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while trying to open a new search: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Event method that will respond and close the resultsearch window when 
        /// the user hits the cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//ned ListByAssigne
    }
}
