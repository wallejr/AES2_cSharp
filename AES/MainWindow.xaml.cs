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
        }

        private void LoadCases()
        {
            this.DgCases.ItemsSource = cm.a_List;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            CaseWindow cw = new CaseWindow();
            cw.Show();
        }
    }
}
