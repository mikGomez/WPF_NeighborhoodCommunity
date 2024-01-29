using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_NeighborhoodCommunity.Models
{
    internal class Portal
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _idPortal;
        private int _numEscaleras;
        private int _idComunidad;
        private int _numPortal;
        #endregion

        #region OBJETOS
        public int IdPortal
        {
            get { return _idPortal; }
            set
            {
                _idPortal = value;
                OnPropertyChange("IdPortal");
            }
        }

        public int NumEscaleras
        {
            get { return _numEscaleras; }
            set
            {
                _numEscaleras = value;
                OnPropertyChange("NumEscaleras");
            }
        }
        public int NumPortal
        {
            get { return _numPortal; }
            set
            {
                _numPortal = value;
                OnPropertyChange("NumPortal");
            }
        }

        public int IdComunidad
        {
            get { return _idComunidad; }
            set
            {
                _idComunidad = value;
                OnPropertyChange("IdComunidad");
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

