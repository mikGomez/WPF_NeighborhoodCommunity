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
