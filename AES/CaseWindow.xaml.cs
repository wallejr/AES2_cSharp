using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        PersonalManager pm = new PersonalManager();
        CommentManager ComMan = new CommentManager();

        public CaseWindow()
        {
            InitializeComponent();
            initGUI();

            
        }
      

        private void initGUI()
        {
            try
            {
                txtBoxCaseID.Text = string.Empty;
                txtBoxCreated.Text = string.Empty;
                txtBoxChanged.Text = string.Empty;
                txtBoxUserName.Text = string.Empty;
                txtBoxFullName.Text = string.Empty;
                txtBoxPhone.Text = string.Empty;
                txtBoxCity.Text = string.Empty;
                txtBoxCompName.Text = string.Empty;
                txtBoxTitel.Text = string.Empty;
                txtBoxCaseDesc.Text = string.Empty;
                txtBoxComments.Text = string.Empty;
                txtBoxSolution.Text = string.Empty;


                foreach (Status state in EnumToList<Status>())
                {
                    comboBoxState.Items.Add(GetEnumDescription(state));
                }
                comboBoxState.SelectedIndex = 0;

                comboBoxDepartment.ItemsSource = Enum.GetValues(typeof(Department));
                comboBoxDepartment.SelectedIndex = 0;

                comboBoxCategory.ItemsSource = catm.ToStringArray();
                comboBoxCategory.SelectedIndex = 0;

                populatePersonalLists();


            }//End Try
            catch (Exception ex)
            {
                MessageBox.Show("There was an error opening sql data \n" + ex.Message);
            }//End catch

        } //End method initGUI

        private void populatePersonalLists()
        {
            pm.getItSupporters(comboBoxCategory.SelectedItem.ToString());
            comboBoxAssigne.ItemsSource = pm.ToStringArray();
            
            comboBoxAssigne.SelectedIndex = 0;

            pm.getProcessLeader(comboBoxCategory.SelectedItem.ToString());
            comboBoxCreateBy.ItemsSource = pm.ToStringArray();
            comboBoxCreateBy.SelectedIndex = 0;

            

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxCaseID.Text))
            {
                saveNewCase();
            }
            else
            {
                UpdateCase();
            }
        }

        private void saveNewCase()
        {
            try
            {
                if (validateName())
                {
                    if (validatePhone())
                    {
                        if (validateCasInfo())
                        {
                            if (validateSelections())
                            {
                                Case c = new Case();
                                
                                c.RequesterName = txtBoxFullName.Text;
                                c.ReqUserName = txtBoxUserName.Text;
                                c.CaseDesc = txtBoxCaseDesc.Text;
                                c.Cat = comboBoxCategory.SelectedItem.ToString();
                                c.Created = DateTime.Now;
                                c.Changed = DateTime.Now;
                                c.CaseTitle = txtBoxTitel.Text;
                                c.Assigne = comboBoxAssigne.SelectedItem.ToString();
                                c.CreatedBy = comboBoxCreateBy.SelectedItem.ToString();
                                c.PhoneNr = txtBoxPhone.Text;
                                c.ComputerName = txtBoxCompName.Text;
                                c.State = comboBoxState.SelectedItem.ToString();
                                c.Dep = comboBoxDepartment.SelectedItem.ToString();
                                c.City = txtBoxCity.Text;

                                if (!string.IsNullOrEmpty(txtBoxSolution.Text))
                                {
                                    c.Solution = txtBoxSolution.Text;
                                }

                                if (cm.addCase(c))
                                {


                                    if (!string.IsNullOrEmpty(txtBoxComments.Text))
                                    {
                                        Comment com = new Comment();
                                        com.Comments = txtBoxComments.Text;
                                        com.CaseIDFK = c.CaseID;
                                        com.WrittenTime = DateTime.Now;

                                        if (comboBoxAssigne.Equals("Not Assigned"))
                                        {
                                            com.WrittenBy = comboBoxCreateBy.SelectedItem.ToString();
                                        }
                                        else
                                        {
                                            com.WrittenBy = comboBoxAssigne.SelectedItem.ToString();
                                        }

                                        if (!ComMan.AddComment(com, cm.GetPKID))
                                        {
                                            throw new Exception();
                                        }
                                    }//End if comments is not null
                                    this.Close();
                                }
                                else
                                {
                                    throw new Exception();
                                } //End if..else Solution is not empty

                            }//End if validateSelection
                        }//End if validateCaseinfo

                    }//End if validatephone

                    
                }//End if validatephone
            }//End try
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the case \n" + ex.Message);
            }
        }// End method saveNewCase

        private void UpdateCase()
        {
            try
            {
                if (validateName())
                {
                    if (validatePhone())
                    {
                        if (validateCasInfo())
                        {
                            if (validateSelections())
                            {
                                Case c = new Case();
                                
                                c.CaseID = Convert.ToInt32(txtBoxCaseID.Text);
                                c.RequesterName = txtBoxFullName.Text;
                                c.ReqUserName = txtBoxUserName.Text;
                                c.CaseDesc = txtBoxCaseDesc.Text;
                                c.Cat = comboBoxCategory.SelectedItem.ToString();
                                c.Changed = DateTime.Now;
                                c.CaseTitle = txtBoxTitel.Text;
                                c.Assigne = comboBoxAssigne.SelectedItem.ToString();
                                c.PhoneNr = txtBoxPhone.Text;
                                c.ComputerName = txtBoxCompName.Text;
                                c.State = comboBoxState.SelectedItem.ToString();
                                c.Dep = comboBoxDepartment.SelectedItem.ToString();
                                c.City = txtBoxCity.Text;

                                if (!string.IsNullOrEmpty(txtBoxSolution.Text))
                                {
                                    c.Solution = txtBoxSolution.Text;
                                }

                                if (cm.updateCase(c))
                                {
                                    if(!string.IsNullOrEmpty(txtBoxComments.Text))
                                    {
                                        Comment com = new Comment();
                                        com.Comments = txtBoxComments.Text;
                                        com.CaseIDFK = c.CaseID;
                                        com.WrittenTime = DateTime.Now;

                                        if (comboBoxAssigne.SelectedItem.Equals("Not Assigned"))
                                        {
                                            com.WrittenBy = comboBoxCreateBy.SelectedItem.ToString();
                                        }
                                        else
                                        {
                                            com.WrittenBy = comboBoxAssigne.SelectedItem.ToString();
                                        }

                                        if(!ComMan.UpdateCaseComment(com))
                                        {
                                            throw new Exception();
                                        }

                                    } //End if comments is not empty
                                    MessageBox.Show("Case Updated");
                                    this.Close();
                                }
                                else
                                {
                                    throw new Exception();
                                }

                            }//End if ValidateSelection
                        }//End if ValidateCaseInfo

                    }//Enn validatePhone

                    
                }//End validateNAme
            }//End try
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the case \n" + ex.Message);
            }
        }// End method

        public void openCase(int id)
        {
            Case c = new Case();
            c = cm.LoadCase(id);

            txtBoxCaseID.Text = c.CaseID.ToString();
            txtBoxCreated.Text = c.Created.ToString();
            txtBoxChanged.Text = c.Changed.ToString();
            comboBoxState.SelectedItem = c.State;
            comboBoxCreateBy.SelectedItem = c.CreatedBy;
            comboBoxAssigne.SelectedItem = c.Assigne;
            comboBoxCategory.SelectedItem = c.Cat;
            txtBoxFullName.Text = c.RequesterName;
            txtBoxUserName.Text = c.ReqUserName;
            txtBoxPhone.Text = c.PhoneNr;
            comboBoxDepartment.SelectedItem = c.Dep;
            txtBoxCity.Text = c.City;
            txtBoxCompName.Text = c.ComputerName;
            txtBoxTitel.Text = c.CaseTitle;
            txtBoxCaseDesc.Text = c.CaseDesc;
            
            if (!string.IsNullOrEmpty(txtBoxComments.Text) || !string.IsNullOrEmpty(txtBoxSolution.Text))
            {
                txtBoxComments.Text = c.Comments;

                if (!string.IsNullOrEmpty(txtBoxSolution.Text))
                {
                    txtBoxSolution.Text = c.Solution;
                }
            }
            

        }

        private bool validateSelections()
        {
            string selectedAssigne = comboBoxAssigne.ToString();
            

            if (comboBoxState.Equals(Status.Assigned) && selectedAssigne.Equals("Not Assigned"))
            {
                MessageBox.Show("An assigne must be selected when status Assigned is selected");
                return false;
            }
            else if (comboBoxState.Equals(Status.Open) && selectedAssigne.Equals("Not Assigned"))
            {
                MessageBox.Show("You can't selected an assigne when selected status i opened");
                return false;
            }
            else if (comboBoxState.Equals(Status.Closed) && string.IsNullOrEmpty(txtBoxSolution.Text))
            {
                MessageBox.Show("You must enter a solution when closing the case");
                return false;
            }
            else
                return true;
            

        }

        private bool validateName()
        {
            
            string tempFullName = txtBoxFullName.Text;
            string tempUserName = txtBoxUserName.Text;
            string tempPhone = txtBoxPhone.Text;
            int checkInt;
            if (int.TryParse(txtBoxFullName.Text, out checkInt))
            {
                MessageBox.Show("Please enter a correct name", "Inputer Error");
                 return false;
                
            }
            else if ( int.TryParse(txtBoxUserName.Text, out checkInt))
            {
                MessageBox.Show("Please enter a correct username", "Inputer Error");
                return false;
            }
            
            else if (string.IsNullOrEmpty(tempFullName))
            {
                MessageBox.Show("Please enter a name", "Inputer Error");
                return false;
            }
            else if (string.IsNullOrEmpty(tempUserName))
            {
                MessageBox.Show("Please enter a username", "Inputer Error");
                return false;
            }
            
            else
                return true;

        }

        public bool validatePhone()
        {
            string tempPhone = txtBoxPhone.Text;
            int checkint;

            if (!int.TryParse(tempPhone, out checkint))
            {
                MessageBox.Show("Please enter a correct number\nExample 012314124", "Inputer Error");
                return false;
            }
            else
                return true;
        }

        public bool validateCasInfo()
        {
            string temptTitle = txtBoxTitel.Text;
            string tempDesc = txtBoxCaseDesc.Text;

            if(string.IsNullOrEmpty(temptTitle))
            {
                MessageBox.Show("Please enter a correct case Title", "Inputer Error");
                return false;
            }
            else if(string.IsNullOrEmpty(tempDesc))
            {
                MessageBox.Show("Please enter a correct case description", "Inputer Error");
                return false;
            }

            return true;
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

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populatePersonalLists();
        }

        private void btnReadComments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtBoxCaseID.Text);
                CommentsWindow cw = new CommentsWindow(id);
                cw.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured:\n" + ex.Message);
            }
            


        }

    }
}
