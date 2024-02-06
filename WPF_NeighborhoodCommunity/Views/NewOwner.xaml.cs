using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public partial class NewOwner : Window
    {
        PisoViewModel pisoModelView = new PisoViewModel();
        PropietarioModelView propietariomodelView = new PropietarioModelView();
        private int portalNumber = 0;
        private int escaleraNumber = 0;
        private int letraNumber = 0;
        public NewOwner()
        {
            DataContext = pisoModelView;

            InitializeComponent();
        }
        private void Button_BuscarComu(object sender, RoutedEventArgs e)
        {
            
            int numPortal = pisoModelView.totalPortales(nombre.Text);
            if (numPortal > 0)
            {
                comboBoxPortales.Items.Clear();
                for (int i = 1; i <= numPortal; i++)
                {
                    comboBoxPortales.Items.Add($"Portal {i}");
                }
                comboBoxPortales.SelectedIndex = 0;
            }
            else {
                MessageBox.Show("No existe ninguna comunidad con ese nombre", "Información incompleta", MessageBoxButton.OK, MessageBoxImage.Warning);
                comboBoxEscalera.SelectedIndex = -1;
                comboBoxLetra.SelectedIndex = -1;
                comboBoxPlanta.SelectedIndex = -1;
                comboBoxPortales.SelectedIndex = -1;
            }
            
        }
        private void ComboBoxPortalesChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedPortalIndex = comboBoxPortales.SelectedIndex;

            if (selectedPortalIndex >= 0)
            {
                portalNumber = selectedPortalIndex + 1;
                LlenarComboBoxEscalera();
            }
        }

        private void LlenarComboBoxEscalera()
        {
            int numEscalera = pisoModelView.totalEscaleras(portalNumber, nombre.Text);

            comboBoxEscalera.Items.Clear();
            for (int i = 1; i <= numEscalera; i++)
            {
                comboBoxEscalera.Items.Add($"Escalera {i}");
            }
            comboBoxEscalera.SelectedIndex = 0;
        }
        private void ComboBoxEscaleraChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedPortalIndex = comboBoxEscalera.SelectedIndex;

            if (selectedPortalIndex >= 0)
            {
                escaleraNumber = selectedPortalIndex + 1;
                LlenarComboBoxPlanta();
            }
        }
        private void LlenarComboBoxPlanta()
        {
            int numPlanta = pisoModelView.totalPlantas(portalNumber, escaleraNumber, nombre.Text);

            comboBoxPlanta.Items.Clear();
            for (int i = 0; i < numPlanta; i++)
            {
                comboBoxPlanta.Items.Add($"Planta {i}");
            }
            comboBoxPlanta.SelectedIndex = 0;
        }

        private void ComboBoxPlantaChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedPlantaIndex = comboBoxPlanta.SelectedIndex;


            if (selectedPlantaIndex >= 0)
            {
                letraNumber = selectedPlantaIndex;
                LlenarComboBoxLetra();
            }
        }
        private void LlenarComboBoxLetra()
        {
            int numLetra = pisoModelView.totalLetras(portalNumber, escaleraNumber,letraNumber ,nombre.Text);

            comboBoxLetra.Items.Clear();
            for (int i = 1; i <= numLetra; i++)
            {
                char letra = (char)('A' + i - 1);
                comboBoxLetra.Items.Add($"Letra {letra}");
            }
            comboBoxLetra.SelectedIndex = 0; 
        }
        private void Button_Next(object sender, RoutedEventArgs e)
        {
            if (comboBoxPortales.SelectedIndex >= 0 &&
        comboBoxEscalera.SelectedIndex >= 0 &&
        comboBoxPlanta.SelectedIndex >= 0 &&
        comboBoxLetra.SelectedIndex >= 0)
            {
                newOwner.IsEnabled = true;
                Control.SelectedIndex = 1;
                DataContext = propietariomodelView;

                //He buscado como coger la ultimo caracter de un comboBox, asi ya tengo la letra 
                string combo = comboBoxLetra.SelectedItem.ToString();
                char letra = combo[combo.Length - 1];
                string seleccionado = comboBoxPlanta.SelectedItem.ToString();

                if (int.TryParse(seleccionado.Split(' ')[1], out int numP))
                {
                    Console.WriteLine("Número de Planta Seleccionado: " + numP);
                }

                int idPi = propietariomodelView.GetIdPiso(portalNumber, escaleraNumber, numP, letra, nombre.Text);
                propietariomodelView.LoadPropietarios(idPi);
                dgvOwners.ItemsSource = propietariomodelView.ListPropietarios;
            }
            else
            {
                MessageBox.Show("Selecciona todas las opciones antes de continuar.", "Información incompleta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btn_saveOwner(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDNI.Text) ||
            string.IsNullOrWhiteSpace(txtName.Text) ||
            string.IsNullOrWhiteSpace(txtAddress.Text) ||
            string.IsNullOrWhiteSpace(txtCity.Text) ||
            string.IsNullOrWhiteSpace(txtPostalCode.Text) ||
            string.IsNullOrWhiteSpace(txtProvince.Text))
            {
                MessageBox.Show("Rellene todas las opciones", "Información incompleta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (propietariomodelView.ListPropietarios == null)
                {
                    propietariomodelView.ListPropietarios = new ObservableCollection<Propietario>();
                }
                if (propietariomodelView.comprobarDNI(txtDNI.Text))
                {
                    MessageBox.Show("DNI ya existe", "DNI", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string combo = comboBoxLetra.SelectedItem.ToString();
                char letra = combo[combo.Length - 1];
                string seleccionado = comboBoxPlanta.SelectedItem.ToString();
                if (int.TryParse(seleccionado.Split(' ')[1], out int numP))
                {
                    Console.WriteLine("Número de Planta Seleccionado: " + numP);
                }

                int idPi = propietariomodelView.GetIdPiso(portalNumber, escaleraNumber, numP, letra, nombre.Text);
                propietariomodelView.IdPiso = idPi;
                
                Propietario owner = new Propietario
                {
                    Dni = propietariomodelView.Dni,
                    Nombre = propietariomodelView.Nombre,
                    Calle = propietariomodelView.Calle,
                    Localidad = propietariomodelView.Localidad,
                    Cp = propietariomodelView.Cp,
                    Provincia = propietariomodelView.Provincia,
                    IdPiso = propietariomodelView.IdPiso
                };
                propietariomodelView.ListPropietarios.Add(owner);
                propietariomodelView.NewPropietario();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (btnDelete.IsEnabled)
            {
                // Si no la creamos da error, he encontrado que salta error por un tema de seguridad
                List<Propietario> selectedOwner = dgvOwners.SelectedItems.Cast<Propietario>().ToList();

                foreach (Propietario prop in selectedOwner)
                {
                    propietariomodelView.Dni = prop.Dni;
                    propietariomodelView.DeleteProp();
                    propietariomodelView.ListPropietarios.Remove(prop);
                }
                propietariomodelView.Dni = "";
            }
        }
        private void btnVerComunidades(object sender, RoutedEventArgs e)
        {
            ListCommunity community = new ListCommunity();
            community.Show();
        }
    }
}
