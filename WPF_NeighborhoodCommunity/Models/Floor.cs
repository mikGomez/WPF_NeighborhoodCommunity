using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_NeighborhoodCommunity.Models
{
    internal class Floor : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _idPlanta;
        private int _numLetras;
        private int _idEscalera;
        private int _numPlanta;
        #endregion

        #region OBJETOS
        public int IdPlanta
        {
            get { return _idPlanta; }
            set
            {
                _idPlanta = value;
                OnPropertyChange("IdPlanta");
            }
        }

        public int NumLetras
        {
            get { return _numLetras; }
            set
            {
                _numLetras = value;
                OnPropertyChange("NumLetras");
            }
        }

        public int IdEscalera
        {
            get { return _idEscalera; }
            set
            {
                _idEscalera = value;
                OnPropertyChange("IdEscalera");
            }
        }

        public int NumPlanta
        {
            get { return _numPlanta; }
            set
            {
                _numPlanta = value;
                OnPropertyChange("NumPlanta");
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
