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
    internal class FloorModelView
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        // Declaro la constante para la conexión a la BDD
        private const String cnstr = "server=localhost;uid=miguel;pwd=miguel;database=community";
        // Modelo de la lista de registros a mostrar
        private ObservableCollection<Floor> _listPlantas;
        private int _idPlanta;
        private int _numLetras;
        private int _idEscalera;
        private int _numPlanta;
        #endregion

        #region OBJETOS
        public ObservableCollection<Floor> ListPlantas
        {
            get { return _listPlantas; }
            set
            {
                _listPlantas = value;
                OnPropertyChange("ListPlantas");
            }
        }

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

        public int NewFloor()
        {
            String SQL = $"INSERT INTO planta (numLetras, idEscalera, numPlanta) VALUES ('{NumLetras}', '{IdEscalera}', '{NumPlanta}');";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            int newfloorId = GetLastInsertedId();

            return newfloorId;
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

        public void UpdateFloor()
        {
            String SQL = $"UPDATE planta SET " +
                         $"numLetras = '{NumLetras}', " +
                         $"idEscalera = '{IdEscalera}', " +
                         $"numPlanta = '{NumPlanta}' " +
                         $"WHERE idPlanta = '{IdPlanta}';";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void LoadFloor()
        {
            String SQL = $"SELECT idPlanta, numLetras, idEscalera, numPlanta FROM planta;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (ListPlantas == null)
            {
                ListPlantas = new ObservableCollection<Floor>();
            }
            else
            {
                ListPlantas.Clear();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    ListPlantas.Add(new Floor
                    {
                        IdPlanta = int.Parse(i[0].ToString()),
                        NumLetras = int.Parse(i[1].ToString()),
                        IdEscalera = int.Parse(i[2].ToString()),
                        NumPlanta = int.Parse(i[3].ToString())
                    });
                }
            }

            dt.Dispose();
        }
    }
}

