using MahApps.Metro.Controls;
using Mysqlx.Crud;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_NeighborhoodCommunity.Views;

namespace WPF_NeighborhoodCommunity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnOpenNewCommunity(object sender, RoutedEventArgs e)
        {
            NewCommunity otherWindow = new NewCommunity();
            otherWindow.Show();
            this.Close();
        }
        private void btnOpenNewOwner(object sender, RoutedEventArgs e)
        {
            NewOwner other = new NewOwner();
            other.Show();
            this.Close();
        }
        private void btnListCommunity(object sender, RoutedEventArgs e)
        {
            ListCommunity other = new ListCommunity();
            other.Show();
            this.Close();
        }

        private void btnOpenNewInf(object sender, RoutedEventArgs e)
        {

        }
    }
}