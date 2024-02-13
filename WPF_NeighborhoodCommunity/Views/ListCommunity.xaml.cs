using MahApps.Metro.Controls;
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
using WPF_NeighborhoodCommunity.Models;
using WPF_NeighborhoodCommunity.ViewModel;

namespace WPF_NeighborhoodCommunity.Views
{
    /// <summary>
    /// Lógica de interacción para ListCommunity.xaml
    /// </summary>
    public partial class ListCommunity : MetroWindow
    {
        CommunityModelView modelViewCommunity = new CommunityModelView();
        public ListCommunity()
        {
            InitializeComponent();
            DataContext = modelViewCommunity;
            modelViewCommunity.LoadComunidades();
            dgvCommunity.ItemsSource = modelViewCommunity.ListComunidad;
        }
        private void volverMenu(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); 
            this.Close();
        }
        private void BorrarComu(object sender, RoutedEventArgs e)
        {
            // Si no la creamos da error, he encontrado que salta error por un tema de seguridad
            List<Community> selectedCommunity = dgvCommunity.SelectedItems.Cast<Community>().ToList();

            foreach (Community prop in selectedCommunity)
            {
                modelViewCommunity.Name = prop.Name;
                modelViewCommunity.DeleteComunidad(modelViewCommunity.Name);
                modelViewCommunity.ListComunidad.Remove(prop);
            }
        }
    }
}
