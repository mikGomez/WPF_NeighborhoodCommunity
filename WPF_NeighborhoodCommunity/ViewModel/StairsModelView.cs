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
    internal class StairsModelView
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        // Declaro la constante para la conexión a la BDD
        private const String cnstr = "server=localhost;uid=miguel;pwd=miguel;database=community";
        // Modelo de la lista de registros a mostrar
        private ObservableCollection<Stair> _listEscaleras;
        private int _idEscalera;
        private int _numPlantas;
        private int _idPortal;
        private int _numEscalera;
        #endregion

        #region OBJETOS
        public ObservableCollection<Stair> ListEscaleras
        {
            get { return _listEscaleras; }
            set
            {
                _listEscaleras = value;
                OnPropertyChange("ListEscaleras");
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

        public int NewStairs()
        {
            String SQL = $"INSERT INTO escalera (numPlantas, idPortal, numEscalera) VALUES ('{NumPlantas}', '{IdPortal}', '{NumEscalera}');";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            int newStairId = GetLastInsertedId();

            return newStairId;
        }
        private int GetLastInsertedId()
        {
            String SQL = "SELECT LAST_INSERT_ID();";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }

            return -1;
        }

        public void UpdateStairs()
        {
            String SQL = $"UPDATE escalera SET " +
                         $"numPlantas = '{NumPlantas}', " +
                         $"idPortal = '{IdPortal}', " +
                         $"numEscalera = '{NumEscalera}' " +
                         $"WHERE idEscalera = '{IdEscalera}';";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void loadStairs()
        {
            String SQL = $"SELECT idEscalera, numPlantas, idPortal, numEscalera FROM escalera;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (ListEscaleras == null)
            {
                ListEscaleras = new ObservableCollection<Stair>();
            }
            else
            {
                ListEscaleras.Clear();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    ListEscaleras.Add(new Stair
                    {
                        IdEscalera = int.Parse(i[0].ToString()),
                        NumPlantas = int.Parse(i[1].ToString()),
                        IdPortal = int.Parse(i[2].ToString()),
                        NumEscalera = int.Parse(i[3].ToString())
                    });
                }
            }

            dt.Dispose();
        }
    }
}

