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
    class BoxroomModelView : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler PropertyChanged;

        private const string cnstr = "server=localhost;uid=root;pwd=miguel;database=community";
        private ObservableCollection<Boxroom> _listTrasteros;
        private int _trasteroID;
        private int _numeroTrastero;
        #endregion

        #region OBJECTS
        public ObservableCollection<Boxroom> ListTrasteros
        {
            get { return _listTrasteros; }
            set
            {
                _listTrasteros = value;
                OnPropertyChanged("ListTrasteros");
            }
        }

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
                OnPropertyChanged(nameof(NumeroTrastero));
            }
        }
        #endregion
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int NewTrastero()
        {
            string SQL = $"INSERT INTO trastero (numeroTrastero) VALUES ('{NumeroTrastero}');";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            int newTrasteroId = GetLastInsertedId();

            return newTrasteroId;
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

        public void UpdateTrastero()
        {
            string SQL = $"UPDATE trastero SET " +
                         $"numeroTrastero = '{NumeroTrastero}' " +
                         $"WHERE TrasteroID = '{TrasteroID}';";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void LoadTrasteros()
        {
            string SQL = $"SELECT TrasteroID, numeroTrastero FROM trastero;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (ListTrasteros == null)
            {
                ListTrasteros = new ObservableCollection<Boxroom>();
            }
            else
            {
                ListTrasteros.Clear();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    ListTrasteros.Add(new Boxroom
                    {
                        TrasteroID = int.Parse(i[0].ToString()),
                        NumeroTrastero = int.Parse(i[1].ToString())
                    });
                }
            }

            dt.Dispose();
        }
    }
}
