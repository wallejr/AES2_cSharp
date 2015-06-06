/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

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
        /// <summary>
        /// Private instance variable declaring a referance to CaseManager object
        /// </summary>
        private CaseManager cm = new CaseManager();
        

        public MainWindow()
        {
            InitializeComponent();
            LoadCases();

            CultureInfo.CreateSpecificCulture("en-US");
        }

        /// <summary>
        /// Method that will load all cases with status Open in the list.
        /// Calling method loadFromDatabase in the casemanager class
        /// </summary>
        private void LoadCases()
        {

            this.DgCases.ItemsSource = cm.loadFromDatabase();
        }

        /// <summary>
        /// Method that will respond when the user click the new case button.
        /// The method will declare an object of the window class CaseWindow
        /// Open it as a dialog and when the dialog is closed it will reload the list of open cases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            CaseWindow cw = new CaseWindow();
            cw.ShowDialog();
            LoadCases();
        }

        /// <summary>
        /// Method that will open a cases based on user choice in the list.
        /// Then it will create an object of class Case, sending the selected case caseid
        /// and display CaseWindow as a dialog. When the dialog is closed the list of opened cases will refreshed
        /// with the method LoadCases as above
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    LoadCases();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occured \n" + ex.Message);
            }


            
        }

        /// <summary>
        /// Method that will respond when the user hits the Search cases button.
        /// The method will
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow();
            sw.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }//ENd method
    }
}
