using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_NeighborhoodCommunity.Models
{
    class Propietario : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler PropertyChanged;

        private string _dni;
        private string _nombre;
        private string _calle;
        private string _localidad;
        private int _cp;
        private string _provincia;
        private int _idPiso;
        #endregion

        #region OBJECTS
        public string Dni
        {
            get { return _dni; }
            set
            {
                _dni = value;
                OnPropertyChanged("Dni");
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string Calle
        {
            get { return _calle; }
            set
            {
                _calle = value;
                OnPropertyChanged("Calle");
            }
        }

        public string Localidad
        {
            get { return _localidad; }
            set
            {
                _localidad = value;
                OnPropertyChanged("Localidad");
            }
        }

        public int Cp
        {
            get { return _cp; }
            set
            {
                _cp = value;
                OnPropertyChanged("Cp");
            }
        }

        public string Provincia
        {
            get { return _provincia; }
            set
            {
                _provincia = value;
                OnPropertyChanged("Provincia");
            }
        }

        public int IdPiso
        {
            get { return _idPiso; }
            set
            {
                _idPiso = value;
                OnPropertyChanged("IdPiso");
            }
        }
        #endregion

        // Método que se encarga de actualizar las propiedades en cada cambio
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
