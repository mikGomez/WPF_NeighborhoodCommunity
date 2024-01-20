using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_NeighborhoodCommunity.DB;

namespace WPF_NeighborhoodCommunity.Models
{
    internal class Community
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        // Modelo de la lista de registros a mostrar
        private int _idComunidad;
        private String _name = "";
        private String _direccion = "";
        private int _numPortales = 0;
        private DateTime _fechaCreacion = DateTime.Now;
        private decimal _metrosCuadrados = 0;
        private bool _piscina = false;
        private bool _pisoPortero = false;
        private bool _duchas = false;
        private bool _parque = false;
        private bool _maquinasEjercicio = false;
        private bool _salaReuniones = false;
        private bool _pistaTenis = false;
        private bool _pistaPadel = false;
        #endregion

        #region OBJETOS
        public int IdComunidad
        {
            get { return _idComunidad; }
            set
            {
                _idComunidad = value;
                OnPropertyChange("IdComunidad");
            }
        }
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChange("Name");
            }
        }

        public String Direccion
        {
            get { return _direccion; }
            set
            {
                _direccion = value;
                OnPropertyChange("Direccion");
            }
        }

        public int NumPortales
        {
            get { return _numPortales; }
            set
            {
                _numPortales = value;
                OnPropertyChange("NumPortales");
            }
        }

        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set
            {
                _fechaCreacion = value;
                OnPropertyChange("FechaCreacion");
            }
        }

        public decimal MetrosCuadrados
        {
            get { return _metrosCuadrados; }
            set
            {
                _metrosCuadrados = value;
                OnPropertyChange("MetrosCuadrados");
            }
        }

        public bool Piscina
        {
            get { return _piscina; }
            set
            {
                _piscina = value;
                OnPropertyChange("Piscina");
            }
        }

        public bool PisoPortero
        {
            get { return _pisoPortero; }
            set
            {
                _pisoPortero = value;
                OnPropertyChange("PisoPortero");
            }
        }

        public bool Duchas
        {
            get { return _duchas; }
            set
            {
                _duchas = value;
                OnPropertyChange("Duchas");
            }
        }

        public bool Parque
        {
            get { return _parque; }
            set
            {
                _parque = value;
                OnPropertyChange("Parque");
            }
        }

        public bool MaquinasEjercicio
        {
            get { return _maquinasEjercicio; }
            set
            {
                _maquinasEjercicio = value;
                OnPropertyChange("MaquinasEjercicio");
            }
        }

        public bool SalaReuniones
        {
            get { return _salaReuniones; }
            set
            {
                _salaReuniones = value;
                OnPropertyChange("SalaReuniones");
            }
        }

        public bool PistaTenis
        {
            get { return _pistaTenis; }
            set
            {
                _pistaTenis = value;
                OnPropertyChange("PistaTenis");
            }
        }

        public bool PistaPadel
        {
            get { return _pistaPadel; }
            set
            {
                _pistaPadel = value;
                OnPropertyChange("PistaPadel");
            }
        }
        #endregion

        // Método que se encarga de actualizar las propiedades en cada cambio
        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
