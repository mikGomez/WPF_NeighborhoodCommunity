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
    public partial class CommunityFloor : Window
    {
        private FloorModelView modelFloorCommunity = new FloorModelView();
        int numFloorRestantes;
        int idPlan = 0;
        public CommunityFloor(int idP, int numPlantasTotales)
        {
            DataContext = modelFloorCommunity;
            modelFloorCommunity.LoadFloor();
            InitializeComponent();
            modelFloorCommunity.IdEscalera = idP;
            numFloorRestantes = numPlantasTotales;
            contador.Text = numFloorRestantes.ToString();
        }
        private void Button_floor(object sender, RoutedEventArgs e)
        {
            if (numFloorRestantes > 0)
            {
                Createfloor();

                //CommunityFloor otherWindow = new CommunityFloor(idStair, modelStairsCommunity.NumPlantas);
                //otherWindow.Show();
                numFloorRestantes--;
                contador.Text = numFloorRestantes.ToString();

                if (numFloorRestantes <= 0)
                {
                    this.Close();
                    save.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Ya has alcanzado el límite de portales.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Createfloor()
        {
            Floor escalera = new Floor
            {
                IdEscalera = modelFloorCommunity.IdEscalera,
                NumPlanta = modelFloorCommunity.NumPlanta,
                NumLetras = modelFloorCommunity.NumLetras
            };

            if (modelFloorCommunity.ListPlantas == null)
            {
                modelFloorCommunity.ListPlantas = new ObservableCollection<Floor>();
            }
            modelFloorCommunity.ListPlantas.Add(escalera);
            idPlan = modelFloorCommunity.NewFloor();

        }
    }
}
