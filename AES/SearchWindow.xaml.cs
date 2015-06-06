/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        ListSearchWindow lw = new ListSearchWindow();
        CategoryManager catm = new CategoryManager();
        PersonalManager pm = new PersonalManager();
        public SearchWindow()
        {
            InitializeComponent();
            InitGUI();
        }

        /// <summary>
        /// Method to initalize the custom GUI.
        /// Status combobox recieves information from the Enum while combox for Category and Assignes
        /// is calling respective manager that will load information from a database into a list.
        /// </summary>
        private void InitGUI()
        {
            foreach (Status state in EnumToList<Status>())
            {
                comboBoxStatus.Items.Add(GetEnumDescription(state));
            }
            comboBoxStatus.SelectedIndex = 0;

            comboBoxCategory.ItemsSource = catm.ToStringArray();
            comboBoxCategory.SelectedIndex = 0;


            comboBoxAssigne.ItemsSource = pm.GetAllAssignes();
            comboBoxAssigne.SelectedIndex = 0;
        }

        /// <summary>
        /// Event method that will respond on corresponding search alternatives.
        /// Depending on the choice, one of those below will be called and call
        /// to corresponding public method in the ListSeachWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchStatus_Click(object sender, RoutedEventArgs e)
        {
            lw.SearchCriteria = comboBoxStatus.SelectedItem.ToString();
            lw.ListByStatus();
            lw.Show();
            this.Close();

        }

        private void btnSearchCategory_Click(object sender, RoutedEventArgs e)
        {
            lw.SearchCriteria = comboBoxCategory.SelectedItem.ToString();
            lw.ListByStatus();
            lw.Show();
            this.Close();
        }

        private void btnSearchCaseID_Click(object sender, RoutedEventArgs e)
        {
            lw.SearchCriteria = txtBoxID.Text;
            lw.ListByStatus();
            lw.Show();
            this.Close();
        }

        private void btnSearchAssigne_Click(object sender, RoutedEventArgs e)
        {
            lw.SearchCriteria = comboBoxAssigne.SelectedItem.ToString();
            lw.ListByStatus();
            lw.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method that will get the enumdescription from the enum ChangeRoute
        /// to display in the combobox
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetEnumDescription(Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Method that will store the enum in a list, it's only purpose is to store the
        /// enums in the list that make it possible to iterate through in the Fillcombox method
        /// and display user friendly information since enums cant handle space character otherwise
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }
    }
}
