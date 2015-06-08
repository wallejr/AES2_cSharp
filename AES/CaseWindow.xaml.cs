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
        /// <summary>
        /// Initialization and declaration of thoose managerclasses needen to handle a case
        /// </summary>
        CaseManager cm = new CaseManager();
        CategoryManager catm = new CategoryManager();
        PersonalManager pm = new PersonalManager();
        CommentManager ComMan = new CommentManager();
        WorkTaskManager wtm = new WorkTaskManager();

        public CaseWindow()
        {
            InitializeComponent();
            initGUI();

            
        }
      
        /// <summary>
        /// Method that will initialize the GUI with default values
        /// Comboxes is initalized either with values from Enums or from database
        /// </summary>
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

        /// <summary>
        /// Method that will display processleader and assignes depending on which category choosen
        /// Call methods in the personalmanager with the category choosen as a string
        /// </summary>
        private void populatePersonalLists()
        {
            pm.getItSupporters(comboBoxCategory.SelectedItem.ToString());
            comboBoxAssigne.ItemsSource = pm.ToStringArray();
            
            comboBoxAssigne.SelectedIndex = 0;

            pm.getProcessLeader(comboBoxCategory.SelectedItem.ToString());
            comboBoxCreateBy.ItemsSource = pm.ToStringArray();
            comboBoxCreateBy.SelectedIndex = 0;
        }

        /// <summary>
        /// Event method that will respond when the clicks the cancel button and close the window without saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method that responds when the user clicks the save button.
        /// The method will check if the caseID is empty then its a new case to create
        /// if there already is a cased id then it's a case that will be updated and call respective methods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Method that will be called if there is a new case that will be created.
        /// Verify that the user has filled the forms correct and then initalize and declare
        /// an object of the Case class. Fill the case variables with corresponding values from the user
        /// and call the addcase method in the casemanager class with t reference to the case object.
        /// 
        /// Before method ending it verify if the user has entered any comments, if so the method
        /// will initialize and declare a Comments object att store user input values to the commet object
        /// variables and then call the addcomment method in CommentManager class with the reference to the comment object.
        /// </summary>
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

        /// <summary>
        /// Method that will respond if there is an existing object that will be updated.
        /// Creates a case object with the case id and thencall a method in the CaseManager class
        /// that will update the row in the database table.
        /// 
        /// The method will also verify if any comments or solutions is entered and if so
        /// solution will be to the case inforatiom to be stored in the solution table.
        /// If comments i written by the user, then a new comment object will be initalized and a
        /// call to the Commentmanager will be made to make a new row in the comment table in database
        /// with the caseid as foreign key
        /// </summary>
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

        /// <summary>
        /// Method that will be called from the MainWindow class before showing window for the user.
        /// This method is used to fill the window with information about the case selected in the MainWindow list
        /// </summary>
        /// <param name="id"></param>
        public void openCase(int id)
        {
            try
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
                txtBoxSolution.Text = c.Solution;

                ShowTaskList(c.CaseID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } //End method openCase

        /// <summary>
        /// Method that will call the ShowTaskList in the WorkTaskManager class.
        /// That is to display the worktaskt associate with each case
        /// </summary>
        /// <param name="id"></param>
        private void ShowTaskList(int id)
        {
            try
            {
                this.dgwTask.ItemsSource = wtm.ShowTaskList(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading the worktask list: \n" + +ex.Message);
            }
        }

        /// <summary>
        /// Validation method to ensure that a case can't be saved with inappropriate values
        /// </summary>
        /// <returns></returns>
        private bool validateSelections()
        {
            string selectedAssigne = comboBoxAssigne.SelectedItem.ToString();
            string statustemp = comboBoxState.SelectedItem.ToString();
            

            if (statustemp.Equals("Assigned") && selectedAssigne.Equals("Not Assigned"))
            {
                MessageBox.Show("An assigne must be selected when status Assigned is selected");
                return false;
            }
            else if (statustemp.Equals("Open") && !selectedAssigne.Equals("Not Assigned"))
            {
                MessageBox.Show("You can't selected an assigne when selected status i opened");
                return false;
            }
            else if (statustemp.Equals("Closed") && selectedAssigne.Equals("Not Assigned"))
            {
                MessageBox.Show("An assigne must be selected when closing the case");
                return false;
            }
            else if (statustemp.Equals("Closed")  && string.IsNullOrEmpty(txtBoxSolution.Text))
            {
                MessageBox.Show("You must enter a solution when closing the case");
                return false;
            }
            
            else
                return true;
            

        }

        /// <summary>
        /// Validation method to ensure that a case can't be saved with inappropriate values
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Validation method to ensure that a case can't be saved with inappropriate values
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Validation method to ensure that a case can't be saved with inappropriate values
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Method that will be called when the user clicks the Add task button.
        /// The method will initalize the AddTaskWindow class with a two variables constructor
        /// that will recieve current caseid and the processleaders name, this informaton is used in the
        /// AddTaskWindow to store the task with foreing key to the caseid.
        /// 
        /// The windows will be displayed as a dialog because the application to pause and resume when window is closed and
        /// update the tasklist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBoxCaseID.Text))
                {
                    int id = int.Parse(txtBoxCaseID.Text);
                    AddTaskWindow atw = new AddTaskWindow(id, comboBoxCreateBy.SelectedItem.ToString());
                    atw.ShowDialog();
                    ShowTaskList(id);
                }
                else
                {
                    MessageBox.Show("You must save the case before adding a task");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurered: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Event method that will be called when Category is changed, the method polpulatePersonalLists
        /// will be called changing the processleader and assignes that has the category as competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populatePersonalLists();
        }

        /// <summary>
        /// Method that will be called when the user clicks the Read comments button,
        /// Initialixe and declare an object of the window class CommentsWindow to its
        /// custom one variable constructor recieving the caseid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event method that will be called when the user clicks the Delete task button.
        /// The method will verify that the user selects a task to delete then calls deltask method
        /// in the WorkTaskManager class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selected = dgwTask.SelectedIndex;
                if (selected < 0)
                {
                    MessageBox.Show("You must select a task in the list to delete");
                }
                else
                {
                    if (wtm.DelTask(((WorkTask)dgwTask.SelectedItem).TaskTodo))
                    {
                        ShowTaskList(int.Parse(txtBoxCaseID.Text));

                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured \n" + ex.Message);
            }
        }

    }
}
