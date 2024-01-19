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

namespace WPF_NeighborhoodCommunity
{
   
    public partial class NewCommunity : Window
    {
        private CommunityModelView modelCommunity = new CommunityModelView();
        public NewCommunity()
        {
            InitializeComponent();
            DataContext = modelCommunity;
            modelCommunity.LoadComunidades();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {

            if (!(string.IsNullOrEmpty(modelCommunity.Direccion)|| modelCommunity.NumPortales <= 0 || modelCommunity.MetrosCuadrados <= 0))
            {
                Community nuevaComunidad = new Community
                {
                    Direccion = modelCommunity.Direccion,
                    NumPortales = modelCommunity.NumPortales,
                    FechaCreacion = modelCommunity.FechaCreacion,
                    MetrosCuadrados = modelCommunity.MetrosCuadrados,
                    Piscina = modelCommunity.Piscina,
                    PisoPortero = modelCommunity.PisoPortero,
                    Duchas = modelCommunity.Duchas,
                    Parque = modelCommunity.Parque,
                    MaquinasEjercicio = modelCommunity.MaquinasEjercicio,
                    SalaReuniones = modelCommunity.SalaReuniones,
                    PistaTenis = modelCommunity.PistaTenis,
                    PistaPadel = modelCommunity.PistaPadel,
                    Name = modelCommunity.Name

                };

                if (modelCommunity.ListComunidad == null)
                {
                    modelCommunity.ListComunidad = new ObservableCollection<Community>();
                }
                modelCommunity.ListComunidad.Add(nuevaComunidad);

                modelCommunity.NewComunidad();

                modelCommunity.Direccion = "";
                modelCommunity.NumPortales = 0;
                modelCommunity.FechaCreacion = DateTime.Now;
                modelCommunity.MetrosCuadrados = 0;
                modelCommunity.Piscina = false;
                modelCommunity.PisoPortero = false;
                modelCommunity.Duchas = false;
                modelCommunity.Parque = false;
                modelCommunity.MaquinasEjercicio = false;
                modelCommunity.SalaReuniones = false;
                modelCommunity.PistaTenis = false;
                modelCommunity.PistaPadel = false;
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
