using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_NeighborhoodCommunity.Models
{
    class Parking : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _idParking;
        private int _numeroParking;
        #endregion

        #region OBJECTS
        public int IdParking
        {
            get { return _idParking; }
            set
            {
                _idParking = value;
                OnPropertyChanged("IdParking");
            }
        }

        public int NumeroParking
        {
            get { return _numeroParking; }
            set
            {
                _numeroParking = value;
                OnPropertyChanged("NumeroParking");
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
