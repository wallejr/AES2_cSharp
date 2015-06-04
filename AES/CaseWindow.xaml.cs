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
    /// Interaction logic for CaseWindow.xaml
    /// </summary>
    public partial class CaseWindow : Window
    {
        CaseManager cm = new CaseManager();
        CategoryManager catm = new CategoryManager();

        public CaseWindow()
        {
            InitializeComponent();
            initGUI();
        }
      

        private void initGUI()
        {
            try
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

                foreach (Status state in EnumToList<Status>())
                    comboBoxState.Items.Add(GetEnumDescription(state));

                comboBoxDepartment.ItemsSource = Enum.GetValues(typeof(Department));

                comboBoxCategory.ItemsSource = catm.ToStringArray();
                comboBoxCategory.SelectedItem = "Network";
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error opening sql data \n" + ex.Message);
            }

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
