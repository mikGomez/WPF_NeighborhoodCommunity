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
    internal class PortalModelView : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private const String cnstr = "server=localhost;uid=miguel;pwd=miguel;database=community";
        private ObservableCollection<Portal> _listPortales;
        private int _idPortal;
        private int _numEscaleras;
        private int _idComunidad;
        private int _numPortal;
        #endregion

        #region OBJETOS
        public ObservableCollection<Portal> ListPortales
        {
            get { return _listPortales; }
            set
            {
                _listPortales = value;
                OnPropertyChange("ListPortales");
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
        public int NumPortal
        {
            get { return _numPortal; }
            set
            {
                _numPortal = value;
                OnPropertyChange("NumPortal");
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

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int NewPortal()
        {
            String SQL = $"INSERT INTO portal (numEscaleras, idComunidad, numPortal) VALUES ('{NumEscaleras}', '{IdComunidad}', '{NumPortal}');";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            int newPortalId = GetLastInsertedId();

            return newPortalId;
        }
        private int GetLastInsertedId()
        {
            String SQL = "SELECT LAST_INSERT_ID();";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (dt.Rows.Count > 0)
            {
                // Devolver el último ID insertado
                return Convert.ToInt32(dt.Rows[0][0]);
            }

            return -1;
        }

        public void UpdatePortal()
        {
            String SQL = $"UPDATE portal SET " +
                         $"numEscaleras = '{NumEscaleras}', " +
                         $"idComunidad = '{IdComunidad}' " +
                         $"numPortal = '{NumPortal}' " +
                         $"WHERE idportal = '{IdPortal}';";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }
        public int GetIdComunidadByName(string comunidadName)
        {
            // Construir la consulta SQL para obtener el idComunidad por nombre
            string SQL = $"SELECT idcomunidad FROM comunidad WHERE name = '{comunidadName}';";

            object result = MySQLDataManagement.ExecuteScalar(SQL, cnstr);

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }

            return -1;
        }

        public void LoadPortales()
        {
            String SQL = $"SELECT idportal, numEscaleras, idComunidad, numPortal FROM portal;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);
            if (ListPortales == null)
            {
                ListPortales = new ObservableCollection<Portal>();
            }
            else
            {
                ListPortales.Clear();
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    ListPortales.Add(new Portal
                    {
                        IdPortal = int.Parse(i[0].ToString()),
                        NumEscaleras = int.Parse(i[1].ToString()),
                        IdComunidad = int.Parse(i[2].ToString()),
                        NumPortal = int.Parse(i[3].ToString())
                    });
                }
            }
            dt.Dispose();
        }
    }
}
