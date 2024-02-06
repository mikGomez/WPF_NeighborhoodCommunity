using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using WPF_NeighborhoodCommunity.DB;
using WPF_NeighborhoodCommunity.Models;

namespace WPF_NeighborhoodCommunity.ViewModel
{
    internal class PisoViewModel : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        private const string cnstr = "server=localhost;uid=miguel;pwd=miguel;database=community";
        private ObservableCollection<Piso> _listPisos;
        private int _idPiso;
        private char _letra;
        private int _idParking;
        private int _idTrastero;
        private int _idPlanta;
        #endregion

        #region OBJECTS
        public ObservableCollection<Piso> ListPisos
        {
            get { return _listPisos; }
            set
            {
                _listPisos = value;
                OnPropertyChanged("ListPisos");
            }
        }

        public int IdPiso
        {
            get { return _idPiso; }
            set
            {
                _idPiso = value;
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
            get { return _idPlanta; }
            set
            {
                _idPlanta = value;
                OnPropertyChanged("IdPlanta");
            }
        }
        #endregion
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //HAcemos una consulta para sacar el total de portales
        public int totalPortales(string nameComunidad)
        {
            string SQL = $"SELECT MAX(numPortal) FROM portal WHERE idcomunidad = (SELECT idcomunidad FROM comunidad WHERE name = '{nameComunidad}');";
            object numPortal = MySQLDataManagement.ExecuteScalar(SQL, cnstr);

            if (numPortal != null && numPortal != DBNull.Value)
            {
                return Convert.ToInt32(numPortal);
            }
            else
            {
                return 0; 
            }
        }

        //Consulta para sacar total de escaleras, se podria hacer con un COUNt tambien
        public int totalEscaleras(int numPortal, string nombreComun)
        {
            
            string SQL = $"SELECT numEscaleras FROM portal WHERE numPortal = {numPortal} AND idComunidad IN(SELECT idComunidad FROM comunidad WHERE name = '{nombreComun}');";
            object numEsca = MySQLDataManagement.ExecuteScalar(SQL, cnstr);

            if (numEsca != null && numEsca != DBNull.Value)
            {
                return Convert.ToInt32(numEsca);
            }
            else
            {
                return 0;
            }
        }

        //Consulta para sacar el total de plantas
        public int totalPlantas(int numPortal, int numEscalera, string nombreComunidad)
        {
            string SQL = $@"SELECT e.numPlantas
                    FROM portal AS p
                    INNER JOIN escalera AS e ON p.idportal = e.idPortal
                    INNER JOIN planta AS pl ON e.idEscalera = pl.idEscalera
                    WHERE p.numPortal = {numPortal}
                    AND e.numEscalera = {numEscalera}
                    AND p.idComunidad = (SELECT idcomunidad FROM comunidad WHERE name = '{nombreComunidad}');";

            object numPlantas = MySQLDataManagement.ExecuteScalar(SQL, cnstr);

            if (numPlantas != null && numPlantas != DBNull.Value)
            {
                return Convert.ToInt32(numPlantas);
            }
            else
            {
                return 0;
            }
        }
        //Consulta para sacar total pisos pero como esta por letras, para no liarnos
        public int totalLetras(int numPortal, int numEscalera,int numPlanta, string nombreComunidad)
        {
            string SQL = $@"
                SELECT pl.numLetras
                FROM portal AS p
                INNER JOIN escalera AS e ON p.idportal = e.idPortal
                INNER JOIN planta AS pl ON e.idEscalera = pl.idEscalera
                WHERE p.numPortal = {numPortal}
                AND e.numEscalera = {numEscalera}
                AND pl.numPlanta = {numPlanta}
                AND p.idComunidad = (SELECT idcomunidad FROM comunidad WHERE name = '{nombreComunidad}');";

            object numLetras = MySQLDataManagement.ExecuteScalar(SQL, cnstr);

            if (numLetras != null && numLetras != DBNull.Value)
            {
                return Convert.ToInt32(numLetras);
            }
            else
            {
                return 0;
            }
        }
        public int NewPiso()
        {
            string SQL = $"INSERT INTO piso (letra, idParking, idTrastero, idPlanta) " +
                         $"VALUES ('{Letra}', '{IdParking}', '{IdTrastero}', '{IdPlanta}');";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            int newPisoId = GetLastInsertedId();

            return newPisoId;
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

        public void UpdatePiso()
        {
            string SQL = $"UPDATE piso SET " +
                         $"letra = '{Letra}', " +
                         $"idParking = '{IdParking}', " +
                         $"idTrastero = '{IdTrastero}', " +
                         $"idPlanta = '{IdPlanta}' " +
                         $"WHERE idPiso = '{IdPiso}';";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void LoadPisos()
        {
            string SQL = $"SELECT idPiso, letra, idParking, idTrastero, idPlanta FROM piso;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (ListPisos == null)
            {
                ListPisos = new ObservableCollection<Piso>();
            }
            else
            {
                ListPisos.Clear();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    ListPisos.Add(new Piso
                    {
                        IdPiso = int.Parse(i[0].ToString()),
                        Letra = char.Parse(i[1].ToString()),
                        IdParking = int.Parse(i[2].ToString()),
                        IdTrastero = int.Parse(i[3].ToString()),
                        IdPlanta = int.Parse(i[4].ToString())
                    });
                }
            }

            dt.Dispose();
        }
    }
}
