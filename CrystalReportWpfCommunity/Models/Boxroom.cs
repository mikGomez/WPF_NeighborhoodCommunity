using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_NeighborhoodCommunity.Models
{
    class Boxroom : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler PropertyChanged;

        private int _trasteroID;
        private int _numeroTrastero;
        #endregion

        #region OBJETOS
        public int TrasteroID
        {
            get { return _trasteroID; }
            set
            {
                _trasteroID = value;
                OnPropertyChanged("TrasteroID");
            }
        }

        public int NumeroTrastero
        {
            get { return _numeroTrastero; }
            set
            {
                _numeroTrastero = value;
                OnPropertyChanged("NumeroTrastero");
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
