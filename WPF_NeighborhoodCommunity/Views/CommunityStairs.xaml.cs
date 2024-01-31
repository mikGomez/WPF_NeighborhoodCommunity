using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
    public partial class CommunityStairs : Window
    {
        private StairsModelView modelStairsCommunity = new StairsModelView();
        int numStairsRestantes;
        int idStair = 0;
        private CommunityPortal portalWindow;

        public CommunityStairs(int idP, int numEscalerasTotales, CommunityPortal portalWindow)
        {
            DataContext = modelStairsCommunity;
            modelStairsCommunity.loadStairs();
            InitializeComponent();
            modelStairsCommunity.IdPortal = idP;
            numStairsRestantes = numEscalerasTotales;
            contador.Text = numStairsRestantes.ToString();
            this.portalWindow = portalWindow;   
        }

        private void Button_stairs(object sender, RoutedEventArgs e)
        {

            if (numStairsRestantes > 0)
            {
                this.Hide();
                CreateStairs();

                CommunityFloor otherWindow = new CommunityFloor(idStair,modelStairsCommunity.NumPlantas,this, portalWindow,numStairsRestantes);
                otherWindow.Show();
                numStairsRestantes--;
                contador.Text = numStairsRestantes.ToString();

                if (numStairsRestantes <= 0)
                {
                    save.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Ya has alcanzado el límite de portales.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void CreateStairs()
        {
            Stair escalera = new Stair
            {
                IdPortal = modelStairsCommunity.IdPortal,
                NumPlantas = modelStairsCommunity.NumPlantas,
                NumEscalera = modelStairsCommunity.NumEscalera
            };

            if (modelStairsCommunity.ListEscaleras == null)
            {
                modelStairsCommunity.ListEscaleras = new ObservableCollection<Stair>();
            }
            modelStairsCommunity.ListEscaleras.Add(escalera);
            idStair = modelStairsCommunity.NewStairs();
            
        }
    }
}
