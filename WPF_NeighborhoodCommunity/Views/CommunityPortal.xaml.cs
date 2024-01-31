using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para CommunityPortal.xaml
    /// </summary>
    public partial class CommunityPortal : Window
    {
        private PortalModelView modelportalCommunity = new PortalModelView();
        int idComun = 0;
        int numPortalesRestantes;
        int idP = 0;

        public CommunityPortal(string communityName, int numPortales)
        {
            InitializeComponent();
            DataContext = modelportalCommunity;
            modelportalCommunity.LoadPortales();
            idComun = modelportalCommunity.GetIdComunidadByName(communityName);
            numPortalesRestantes = numPortales;
            contador.Text = numPortalesRestantes.ToString();
        }
        private void Button_portal(object sender, RoutedEventArgs e)
        {
            if (numPortalesRestantes > 0)
            {
                this.Hide();
                CreatePortal();
                CommunityStairs otherWindow = new CommunityStairs(idP, modelportalCommunity.NumEscaleras,this);
                otherWindow.Show();
                
                numPortalesRestantes--;
                contador.Text = numPortalesRestantes.ToString();

                if (numPortalesRestantes <= 0)
                {
                    save.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Ya has alcanzado el límite de portales.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CreatePortal()
        {
            modelportalCommunity.IdComunidad = idComun;
            Portal portal = new Portal
            {
                IdComunidad = modelportalCommunity.IdComunidad,
                NumEscaleras = modelportalCommunity.NumEscaleras,
                NumPortal = modelportalCommunity.NumPortal
            };

            if (modelportalCommunity.ListPortales == null)
            {
                modelportalCommunity.ListPortales = new ObservableCollection<Portal>();
            }
            modelportalCommunity.ListPortales.Add(portal);

            idP = modelportalCommunity.NewPortal();
        }
    }
}
