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
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        WorkTaskManager wtm = new WorkTaskManager();
        private int caseID;
        private string Writtenby;

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        public AddTaskWindow(int id, string name)
        {
            caseID = id;
            Writtenby = name;
            InitializeComponent();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    WorkTask wt = new WorkTask();
                    wt.TaskTodo = txtBoxWorkTask.Text;
                    wt.CreatedBy = Writtenby;

                    wtm.AddTask(caseID, wt);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while adding a task: \n" + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            string temp = txtBoxWorkTask.Text;
            int checkInt;
            if (int.TryParse(temp, out checkInt))
            {
                MessageBox.Show("Please enter a correct task", "Inputer Error");
                return false;

            }
            else if (string.IsNullOrEmpty(temp))
            {
                MessageBox.Show("Please enter a name", "Inputer Error");
                return false;
            }
            else
                return true;
                
        }


    }
}
