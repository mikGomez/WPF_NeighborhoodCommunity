using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF_NeighborhoodCommunity.Models
{
    internal class Stair : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _idEscalera;
        private int _numPlantas;
        private int _idPortal;
        private int _numEscalera;
        #endregion

        #region OBJETOS
        public int IdEscalera
        {
            get { return _idEscalera; }
            set
            {
                _idEscalera = value;
                OnPropertyChange("IdEscalera");
            }
        }

        public int NumPlantas
        {
            get { return _numPlantas; }
            set
            {
                _numPlantas = value;
                OnPropertyChange("NumPlantas");
            }
        }

        public int IdPortal
        {
            get { return _idPortal; }
            set
            {
                _idPortal = value;
                OnPropertyChange("IdPortal");
            }
        }

        public int NumEscalera
        {
            get { return _numEscalera; }
            set
            {
                _numEscalera = value;
                OnPropertyChange("NumEscalera");
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


