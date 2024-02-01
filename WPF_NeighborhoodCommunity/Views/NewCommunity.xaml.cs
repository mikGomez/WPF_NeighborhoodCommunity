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
using System.Xml.Linq;
using WPF_NeighborhoodCommunity.Models;
using WPF_NeighborhoodCommunity.ViewModel;

namespace WPF_NeighborhoodCommunity
{

    public partial class NewCommunity : Window
    {
        private CommunityModelView modelCommunity = new CommunityModelView();
        private PortalModelView modelportalCommunity = new PortalModelView();
        public NewCommunity()
        {
            InitializeComponent();
            DataContext = modelCommunity;
            modelCommunity.LoadComunidades();
        }
        private void Button_Next(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(modelCommunity.Name)
                || string.IsNullOrWhiteSpace(modelCommunity.Direccion)
                || modelCommunity.FechaCreacion == null
                || modelCommunity.NumPortales == 0
                || modelCommunity.MetrosCuadrados == 0)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SpecificCommunity.IsEnabled = true;
                Control.SelectedIndex = 1;
            }
        }


        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(modelCommunity.Direccion) || modelCommunity.NumPortales <= 0 || modelCommunity.MetrosCuadrados <= 0))
            {
                Community communityExist = modelCommunity.ListComunidad.FirstOrDefault(c => c.Name == modelCommunity.Name);

                if (communityExist != null)
                {
                    communityExist.Direccion = modelCommunity.Direccion;
                    communityExist.NumPortales = modelCommunity.NumPortales;
                    communityExist.FechaCreacion = modelCommunity.FechaCreacion;
                    communityExist.MetrosCuadrados = modelCommunity.MetrosCuadrados;
                    communityExist.Piscina = modelCommunity.Piscina;
                    communityExist.PisoPortero = modelCommunity.PisoPortero;
                    communityExist.Duchas = modelCommunity.Duchas;
                    communityExist.Parque = modelCommunity.Parque;
                    communityExist.MaquinasEjercicio = modelCommunity.MaquinasEjercicio;
                    communityExist.SalaReuniones = modelCommunity.SalaReuniones;
                    communityExist.PistaTenis = modelCommunity.PistaTenis;
                    communityExist.PistaPadel = modelCommunity.PistaPadel;
                    modelCommunity.UpdateCommunity();
                    MessageBox.Show("Actualizado correctamente!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
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

                    MessageBox.Show("Guardado correctamente!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Portal.IsEnabled = true;
                    ComboBoxPortal();
                    DataContext = modelportalCommunity;
                    Control.SelectedIndex = 2;
                }
            }
        }
        private void Button_Save_portal(object sender, RoutedEventArgs e)
        {
            CreatePortal();
        }
        private void CreatePortal()
        {
            int idComun = modelportalCommunity.GetIdComunidadByName(modelCommunity.Name);
            modelportalCommunity.IdComunidad = idComun;
            for (int i = 1; i < modelCommunity.NumPortales; i++)
            {
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
                modelportalCommunity.NewPortal();
            }

        }
        private void ComboBoxPortal()
        {
            comboBoxPortales.Items.Clear();

            List<string> portalNames = new List<string>();

            for (int i = 1; i <= modelCommunity.NumPortales; i++)
            {
                portalNames.Add("Portal " + i);
            }
            comboBoxPortales.ItemsSource = portalNames;
        }
        private void ComboBoxPortalesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPortales.SelectedItem != null)
            {
                txtEcs.Visibility = Visibility.Visible;
                txtEsca.Visibility = Visibility.Visible;
                comboBoxPortales.IsEnabled = false;
            }
        }
        private void Button_Save_escalera(object sender, RoutedEventArgs e)
        {
            //CreateEscalera();
        }

        private void CreateEscalera()
        {

        }

        private void ComboBoxEscaleraChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Save_planta(object sender, RoutedEventArgs e)
        {
            //CreatePlanta();
        }

        private void CreatePlanta()
        {

        }
        private void ComboBoxPlantaChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
    }
}