using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_NeighborhoodCommunity.DB;
using WPF_NeighborhoodCommunity.Models;

namespace WPF_NeighborhoodCommunity.ViewModel
{
    internal class ParkingModelView : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private const string cnstr = "server=localhost;uid=miguel;pwd=miguel;database=community";
        private ObservableCollection<Parking> _listParking;
        private int _idParking;
        private int _numeroParking;
        #endregion

        #region OBJECTS
        public ObservableCollection<Parking> ListParking
        {
            get { return _listParking; }
            set
            {
                _listParking = value;
                OnPropertyChanged("ListParking");
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int NewParking()
        {
            string SQL = $"INSERT INTO parking (numeroParking) VALUES ('{NumeroParking}');";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            int newParkingId = GetLastInsertedId();

            return newParkingId;
        }

        private int GetLastInsertedId()
        {
            string SQL = "SELECT LAST_INSERT_ID();";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }

            return -1;
        }

        public void UpdateParking()
        {
            string SQL = $"UPDATE parking SET " +
                         $"numeroParking = '{NumeroParking}' " +
                         $"WHERE idParking = '{IdParking}';";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void LoadParking()
        {
            string SQL = $"SELECT idParking, numeroParking FROM parking;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (ListParking == null)
            {
                ListParking = new ObservableCollection<Parking>();
            }
            else
            {
                ListParking.Clear();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    ListParking.Add(new Parking
                    {
                        IdParking = int.Parse(i[0].ToString()),
                        NumeroParking = int.Parse(i[1].ToString())
                    });
                }
            }

            dt.Dispose();
        }
    }
}
