using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_NeighborhoodCommunity.Models
{
    internal class Piso : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _idpiso;
        private char _letra;
        private int _idParking;
        private int _idTrastero;
        private int _idplanta;
        #endregion

        #region OBJETOS
        public int IdPiso
        {
            get { return _idpiso; }
            set
            {
                _idpiso = value;
                OnPropertyChanged("IdPiso");
            }
        }

        public char Letra
        {
            get { return _letra; }
            set
            {
                _letra = value;
                OnPropertyChanged("Letra");
            }
        }

        public int IdParking
        {
            get { return _idParking; }
            set
            {
                _idParking = value;
                OnPropertyChanged("IdParking");
            }
        }

        public int IdTrastero
        {
            get { return _idTrastero; }
            set
            {
                _idTrastero = value;
                OnPropertyChanged("IdTrastero");
            }
        }

        public int IdPlanta
        {
            get { return _idplanta; }
            set
            {
                _idplanta = value;
                OnPropertyChanged("IdPlanta");
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
