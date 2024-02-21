using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using WPF_NeighborhoodCommunity.DB;
using WPF_NeighborhoodCommunity.Models;

namespace WPF_NeighborhoodCommunity.ViewModel
{
    class PropietarioModelView : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler PropertyChanged;

        private const string cnstr = "server=localhost;uid=root;pwd=miguel;database=community";
        private ObservableCollection<Propietario> _listPropietarios;
        private string _dni;
        private string _nombre;
        private string _calle;
        private string _localidad;
        private int _cp;
        private string _provincia;
        private int _idPiso;
        #endregion

        #region OBJECTS
        public ObservableCollection<Propietario> ListPropietarios
        {
            get { return _listPropietarios; }
            set
            {
                _listPropietarios = value;
                OnPropertyChanged("ListPropietarios");
            }
        }

        public string Dni
        {
            get { return _dni; }
            set
            {
                _dni = value;
                OnPropertyChanged("Dni");
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string Calle
        {
            get { return _calle; }
            set
            {
                _calle = value;
                OnPropertyChanged("Calle");
            }
        }

        public string Localidad
        {
            get { return _localidad; }
            set
            {
                _localidad = value;
                OnPropertyChanged("Localidad");
            }
        }

        public int Cp
        {
            get { return _cp; }
            set
            {
                _cp = value;
                OnPropertyChanged("Cp");
            }
        }

        public string Provincia
        {
            get { return _provincia; }
            set
            {
                _provincia = value;
                OnPropertyChanged("Provincia");
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
        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NewPropietario()
        {
            string SQL = $"INSERT INTO propietario (dni, nombre, calle, localidad, cp, provincia, idPiso) " +
                         $"VALUES ('{Dni}', '{Nombre}', '{Calle}', '{Localidad}', {Cp}, '{Provincia}', {IdPiso});";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        //Sacamos el idPiso para la relacion (Más adelante intentare cambiarlo a N:M)
        public int GetIdPiso(int numPortal, int numEscalera, int numPlanta, char letra, string nombreComunidad)
        {
            string SQL = $@"SELECT p.idpiso
                    FROM comunidad c
                    INNER JOIN portal po ON c.idcomunidad = po.idComunidad
                    INNER JOIN escalera e ON po.idportal = e.idPortal
                    INNER JOIN planta pl ON e.idEscalera = pl.idEscalera
                    INNER JOIN piso p ON pl.idplanta = p.idplanta
                    WHERE c.name = '{nombreComunidad}'
                      AND po.numPortal = {numPortal}
                      AND e.numEscalera = {numEscalera}
                      AND pl.numPlanta = {numPlanta}
                      AND p.letra = '{letra}';";

            object idPiso = MySQLDataManagement.ExecuteScalar(SQL, cnstr);

            if (idPiso != null && idPiso != DBNull.Value)
            {
                return Convert.ToInt32(idPiso);
            }
            else
            {
                return 0;
            }
        }
        //Para borrar propietario
        public void DeleteProp()
        {
            String SQL = $"Delete FROM propietario WHERE dni = '{Dni}';";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void UpdatePropietario()
        {
            string SQL = $"UPDATE propietario SET " +
                         $"nombre = '{Nombre}', " +
                         $"calle = '{Calle}', " +
                         $"localidad = '{Localidad}', " +
                         $"cp = {Cp}, " +
                         $"provincia = '{Provincia}', " +
                         $"idPiso = {IdPiso} " +
                         $"WHERE dni = '{Dni}';";

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }
        //Comprobamos que no se repita el dni en la Base de datos, ya que en la lista solo cogeremos la gente de ese piso
        public bool comprobarDNI(string dni)
        {
            string query = $"SELECT COUNT(*) FROM propietario WHERE dni = '{dni}'";
            object comprobar = MySQLDataManagement.ExecuteScalar(query, cnstr);

            int count = 0;
            if (comprobar != null && comprobar != DBNull.Value)
            {
                count = Convert.ToInt32(comprobar);
            }

            return count > 0;
        }

        //Cargamos a la lista según el idPiso
        public void LoadPropietarios(int idPiso)
        {
            string SQL = $"SELECT dni, nombre, calle, localidad, cp, provincia, idPiso FROM propietario WHERE idPiso = {idPiso};";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);

            if (ListPropietarios == null)
            {
                ListPropietarios = new ObservableCollection<Propietario>();
            }
            else
            {
                ListPropietarios.Clear();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    ListPropietarios.Add(new Propietario
                    {
                        Dni = i[0].ToString(),
                        Nombre = i[1].ToString(),
                        Calle = i[2].ToString(),
                        Localidad = i[3].ToString(),
                        Cp = int.Parse(i[4].ToString()),
                        Provincia = i[5].ToString(),
                        IdPiso = int.Parse(i[6].ToString())
                    });
                }
            }

            dt.Dispose();
        }
    }
}
