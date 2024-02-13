using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
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
using WPF_NeighborhoodCommunity.Views;

namespace WPF_NeighborhoodCommunity
{

    public partial class NewCommunity : MetroWindow
    {
        private CommunityModelView modelCommunity = new CommunityModelView();
        private PortalModelView modelportalCommunity = new PortalModelView();
        private StairsModelView modelstairCommunity = new StairsModelView();
        private FloorModelView modelfloorCommunity = new FloorModelView();
        private int contPortal = 1;
        private int contStair = 1;
        private int contFloor = 1;
        private int idPortal = 0;
        private int idStair = 0;
        private int idFloor = 0;
        private int numTras = 1;
        private int numParking = 1;
        List<string> plantaNames = new List<string>();
        List<string> escaleraNames = new List<string>();
        List<string> portalNames = new List<string>();
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

        //Si todo esta correcto guardaremos los datos en la base de datos, si ya existe se actualizará, se podría haber puesto que si existe que no se pueda crear
        //Mejor actualizar por si en un futuro quieren poner piscina o algo
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
        //Iremos paso a paso por lo que guardaremos portal, luego escalera, luego planta, luego si hay más portales repetira el proceso
        private void Button_Save_portal(object sender, RoutedEventArgs e)
        {
            if (modelportalCommunity.NumEscaleras > 0)
            {
                CreatePortal();
                comboBoxEscalera.Visibility = Visibility.Visible;
                ComboBoxEscalera();
                DataContext = modelstairCommunity;
                savePortal.Visibility = Visibility.Collapsed;
                txtEsca.IsEnabled = false;
                MessageBox.Show("Guardado correctamente!, Pasaremos a guardar Escalera", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else {
                MessageBox.Show("Ingrese un numero de escaleras mayor que 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void CreatePortal()
        {
            int idComun = modelportalCommunity.GetIdComunidadByName(modelCommunity.Name);
            modelportalCommunity.IdComunidad = idComun;
            modelportalCommunity.NumPortal = ObtenerNumeroDesdeComboBox(comboBoxPortales);
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
                idPortal = modelportalCommunity.NewPortal();
            contPortal++;
        }

        private void ComboBoxPortal()
        {
            if (portalNames.Count <= 0) {
                for (int i = 1; i <= modelCommunity.NumPortales; i++)
                {
                    portalNames.Add("Portal " + i);
                }
            }
            

            // Elimina la opción seleccionada previamente
            if (comboBoxPortales.SelectedIndex != -1)
            {
                portalNames.RemoveAt(comboBoxPortales.SelectedIndex);
            }

            comboBoxPortales.ItemsSource = null;
            comboBoxPortales.Items.Clear();

            comboBoxPortales.ItemsSource = portalNames;
            comboBoxPortales.SelectedIndex = -1;

        }



        private void ComboBoxPortalesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPortales.SelectedItem != null)
            {
                txtEcs.Visibility = Visibility.Visible;
                txtEsca.Visibility = Visibility.Visible;
                comboBoxPortales.IsEnabled = false;
                savePortal.Visibility = Visibility.Visible;
            }
        }
        private void ComboBoxEscalera()
        {
            if(escaleraNames.Count <= 0)
            {
                for (int i = 1; i <= modelportalCommunity.NumEscaleras; i++)
                {
                    escaleraNames.Add("Escalera " + i);
                }
            }
            if (comboBoxEscalera.SelectedIndex != -1)
            {
                escaleraNames.RemoveAt(comboBoxEscalera.SelectedIndex);
            }

            comboBoxEscalera.ItemsSource = null;
            comboBoxEscalera.Items.Clear();

            comboBoxEscalera.ItemsSource = escaleraNames;
            comboBoxEscalera.SelectedIndex = -1;
        }


        private void Button_Save_escalera(object sender, RoutedEventArgs e)
        {
            if (modelstairCommunity.NumPlantas > 0) {
                CreateEscalera();
                comboBoxPlantas.Visibility = Visibility.Visible;
                ComboBoxPlanta();
                txtPlant.IsEnabled = false;
                DataContext = modelfloorCommunity;
                saveEscalera.Visibility = Visibility.Collapsed;
                MessageBox.Show("Guardado correctamente!, Pasaremos a guardar Planta", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }   
            else
            {
                MessageBox.Show("Ingrese un numero de plantas mayor que 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateEscalera()
        {
            modelstairCommunity.NumEscalera = ObtenerNumeroDesdeComboBox(comboBoxEscalera);
            modelstairCommunity.IdPortal = idPortal;
            Stair escalera = new Stair
            {
                IdPortal = modelstairCommunity.IdPortal,
                NumPlantas = modelstairCommunity.NumPlantas,
                NumEscalera = modelstairCommunity.NumEscalera
            };

            if (modelstairCommunity.ListEscaleras == null)
            {
                modelstairCommunity.ListEscaleras = new ObservableCollection<Stair>();
            }
            modelstairCommunity.ListEscaleras.Add(escalera);
            idStair = modelstairCommunity.NewStairs();
            contStair++;
        }

        private void ComboBoxEscaleraChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxEscalera.SelectedItem != null)
            {
                txtPlan.Visibility = Visibility.Visible;
                txtPlant.Visibility = Visibility.Visible;
                comboBoxEscalera.IsEnabled = false;
                saveEscalera.Visibility = Visibility.Visible;
            }
        }
        private void ComboBoxPlanta()
        {
            if (plantaNames.Count <= 0)
            {
                for (int i = 0; i < modelstairCommunity.NumPlantas; i++)
                {
                    plantaNames.Add("Planta " + i);
                }
            }
            

            // Elimina la opción seleccionada previamente
            if (comboBoxPlantas.SelectedIndex != -1)
            {
                plantaNames.RemoveAt(comboBoxPlantas.SelectedIndex);
            }

            comboBoxPlantas.ItemsSource = null;
            comboBoxPlantas.Items.Clear();

            comboBoxPlantas.ItemsSource = plantaNames;
            comboBoxPlantas.SelectedIndex = -1;
        }


        private void Button_Save_planta(object sender, RoutedEventArgs e)
        {
            if (modelfloorCommunity.NumLetras >= 0)
            {
                CreatePlanta();
                ComboBoxPlanta();
                comboBoxPlantas.IsEnabled = true;
                savePlanta.Visibility = Visibility.Collapsed;
                MessageBox.Show("Guardado correctamente!,", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                if (contFloor > modelstairCommunity.NumPlantas)
                {
                    txtPiso.Visibility = Visibility.Collapsed;
                    txtPisoss.Visibility = Visibility.Collapsed;
                    comboBoxPlantas.Visibility = Visibility.Collapsed;
                    ComboBoxEscalera();
                    comboBoxEscalera.IsEnabled = true;
                    txtPlant.IsEnabled = true;
                    DataContext = modelstairCommunity;
                    contFloor = 1;
                    if (contStair > modelportalCommunity.NumEscaleras)
                    {
                        txtPlan.Visibility = Visibility.Collapsed;
                        txtPlant.Visibility = Visibility.Collapsed;
                        comboBoxEscalera.Visibility = Visibility.Collapsed;
                        comboBoxPortales.IsEnabled = true;
                        txtEsca.IsEnabled = true;
                        DataContext = modelportalCommunity;
                        ComboBoxPortal();
                        contStair = 1;
                        if (contPortal > modelCommunity.NumPortales)
                        {
                            DataContext = modelportalCommunity;
                            comboBoxPortales.Visibility = Visibility.Collapsed;
                            txtEcs.Visibility = Visibility.Collapsed;
                            txtEsca.Visibility = Visibility.Collapsed;
                            MessageBox.Show("Guardado correctamente el portal entero!, Ahora puedes crear los propietarios de este portal", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtCreada.Visibility = Visibility.Visible;
                            btnMenu.Visibility = Visibility.Visible;
                            btnProp.Visibility = Visibility.Visible;
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Ingrese un numero de letras de Pisos mayor que 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Cuando guardamos piso, le asociamos un parking y un trastero
        private void CreatePlanta()
        {
            modelfloorCommunity.IdEscalera = idStair;
            modelfloorCommunity.NumPlanta = ObtenerNumeroDesdeComboBox(comboBoxPlantas);

            Floor escalera = new Floor
            {
                IdEscalera = modelfloorCommunity.IdEscalera,
                NumPlanta = modelfloorCommunity.NumPlanta,
                NumLetras = modelfloorCommunity.NumLetras
            };


            if (modelfloorCommunity.ListPlantas == null)
            {
                modelfloorCommunity.ListPlantas = new ObservableCollection<Floor>();
            }
            modelfloorCommunity.ListPlantas.Add(escalera);
            idFloor = modelfloorCommunity.NewFloor();
            contFloor++;
            char letraInicial = 'A';

            for (int i = 0; i < modelfloorCommunity.NumLetras; i++)
            {
                PisoViewModel pisoViewModel = new PisoViewModel
                {
                    Letra = (char)(letraInicial + i),
                    IdParking = CrearNuevoParking(),
                    IdTrastero = CrearNuevoTrastero(),
                    IdPlanta = idFloor
                };

                pisoViewModel.NewPiso();
                
            }
        }
        private int CrearNuevoParking()
        {
            ParkingModelView parkingViewModel = new ParkingModelView
            {
                NumeroParking = numParking
            };
            numParking++;

            return parkingViewModel.NewParking();
        }

        private int CrearNuevoTrastero()
        {
            BoxroomModelView boxroomViewModel = new BoxroomModelView
            {
                NumeroTrastero = numTras
            };
            numTras++;

            return boxroomViewModel.NewTrastero();
        }



        private void ComboBoxPlantaChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPlantas.SelectedItem != null)
            {
                txtPiso.Visibility = Visibility.Visible;
                txtPisoss.Visibility = Visibility.Visible;
                comboBoxPlantas.IsEnabled = false;
                savePlanta.Visibility = Visibility.Visible;
            }
        }
        private int ObtenerNumeroDesdeComboBox(ComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                string selectedItemString = comboBox.SelectedItem.ToString();

                // Quitamos todo lo anterior al espacio en blanco y nos quedamos con el número, lo pasamos a int y ya tendriamos el numPortal
                int numeroExtraido;
                if (int.TryParse(selectedItemString.Split(' ')[1], out numeroExtraido))
                {
                    return numeroExtraido;
                }
                else
                {
                    throw new InvalidOperationException("No se pudo extraer el número del ComboBox");
                }
            }
            else
            {
                throw new InvalidOperationException("No hay nada seleccionado en el ComboBox");
            }
        }

        private void btnPropietarios(object sender, RoutedEventArgs e)
        {
            NewOwner ownerWindow = new NewOwner();
            ownerWindow.Show();
            this.Close();
        }
        private void btnMen(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}