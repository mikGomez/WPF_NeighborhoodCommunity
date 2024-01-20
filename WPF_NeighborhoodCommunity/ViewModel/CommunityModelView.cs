using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_NeighborhoodCommunity.Models;
using WPF_NeighborhoodCommunity.DB;

namespace WPF_NeighborhoodCommunity.ViewModel
{
    internal class CommunityModelView
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        // Declaro la constante para la conexión a la BDD
        private const String cnstr = "server=localhost;uid=miguel;pwd=miguel;database=community";
        // Modelo de la lista de registros a mostrar
        private ObservableCollection<Community> _listComunidad;
        private int _idComunidad;
        private String _name="";
        private String _direccion = "";
        private int _numPortales= 0;
        private DateTime _fechaCreacion = DateTime.Now;
        private decimal _metrosCuadrados = 0;
        private bool _piscina = false;
        private bool _pisoPortero = false;
        private bool _duchas = false;
        private bool _parque = false;
        private bool _maquinasEjercicio = false;
        private bool _salaReuniones = false;
        private bool _pistaTenis = false;
        private bool _pistaPadel = false;
        #endregion

        #region OBJETOS
        public ObservableCollection<Community> ListComunidad
        {
            get { return _listComunidad; }
            set
            {
                _listComunidad = value;
                OnPropertyChange("ListComunidad");
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
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChange("Name");
            }
        }

        public String Direccion
        {
            get { return _direccion; }
            set
            {
                _direccion = value;
                OnPropertyChange("Direccion");
            }
        }

        public int NumPortales
        {
            get { return _numPortales; }
            set
            {
                _numPortales = value;
                OnPropertyChange("NumPortales");
            }
        }

        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set
            {
                _fechaCreacion = value;
                OnPropertyChange("FechaCreacion");
            }
        }

        public decimal MetrosCuadrados
        {
            get { return _metrosCuadrados; }
            set
            {
                _metrosCuadrados = value;
                OnPropertyChange("MetrosCuadrados");
            }
        }

        public bool Piscina
        {
            get { return _piscina; }
            set
            {
                _piscina = value;
                OnPropertyChange("Piscina");
            }
        }

        public bool PisoPortero
        {
            get { return _pisoPortero; }
            set
            {
                _pisoPortero = value;
                OnPropertyChange("PisoPortero");
            }
        }

        public bool Duchas
        {
            get { return _duchas; }
            set
            {
                _duchas = value;
                OnPropertyChange("Duchas");
            }
        }

        public bool Parque
        {
            get { return _parque; }
            set
            {
                _parque = value;
                OnPropertyChange("Parque");
            }
        }

        public bool MaquinasEjercicio
        {
            get { return _maquinasEjercicio; }
            set
            {
                _maquinasEjercicio = value;
                OnPropertyChange("MaquinasEjercicio");
            }
        }

        public bool SalaReuniones
        {
            get { return _salaReuniones; }
            set
            {
                _salaReuniones = value;
                OnPropertyChange("SalaReuniones");
            }
        }

        public bool PistaTenis
        {
            get { return _pistaTenis; }
            set
            {
                _pistaTenis = value;
                OnPropertyChange("PistaTenis");
            }
        }

        public bool PistaPadel
        {
            get { return _pistaPadel; }
            set
            {
                _pistaPadel = value;
                OnPropertyChange("PistaPadel");
            }
        }
        #endregion

        // Método que se encarga de actualizar las propiedades en cada cambio
        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NewComunidad()
        {
            
            String SQL = $"INSERT INTO comunidad (name,direccion, numPortales, fechaCreacion, metrosCuadrados, piscina, pisoPortero, duchas, parque, maquinasEjercicio, salaReuniones, pistaTenis, pistaPadel) VALUES ('{Name}','{Direccion}', '{NumPortales}', '{FechaCreacion.ToString("yyyy-MM-dd")}', '{MetrosCuadrados}', '{(Piscina ? 1 : 0)}', '{(PisoPortero ? 1 : 0)}', '{(Duchas ? 1 : 0)}', '{(Parque ? 1 : 0)}', '{(MaquinasEjercicio ? 1 : 0)}', '{(SalaReuniones ? 1 : 0)}', '{(PistaTenis ? 1 : 0)}', '{(PistaPadel ? 1 : 0)}');";
            
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void UpdateCommunity()
        {
            String SQL = $"UPDATE comunidad SET " +
                         $"direccion = '{Direccion}', " +
                         $"numPortales = '{NumPortales}', " +
                         $"fechaCreacion = '{FechaCreacion.ToString("yyyy-MM-dd")}', " +
                         $"metrosCuadrados = '{MetrosCuadrados}', " +
                         $"piscina = '{(Piscina ? 1 : 0)}', " +
                         $"pisoPortero = '{(PisoPortero ? 1 : 0)}', " +
                         $"duchas = '{(Duchas ? 1 : 0)}', " +
                         $"parque = '{(Parque ? 1 : 0)}', " +
                         $"maquinasEjercicio = '{(MaquinasEjercicio ? 1 : 0)}', " +
                         $"salaReuniones = '{(SalaReuniones ? 1 : 0)}', " +
                         $"pistaTenis = '{(PistaTenis ? 1 : 0)}', " +
                         $"pistaPadel = '{(PistaPadel ? 1 : 0)}' " +
                         $"WHERE name = '{Name}';";

            // Ejecutar la consulta de actualización
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void LoadComunidades()
        {
            String SQL = $"SELECT idcomunidad, direccion, numPortales, fechaCreacion, metrosCuadrados, piscina, pisoPortero, duchas, parque, maquinasEjercicio, salaReuniones, pistaTenis, pistaPadel,name FROM comunidad;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);
            if (ListComunidad == null)
            {
                ListComunidad = new ObservableCollection<Community>();
            }
            else
            {
                // Le faltaba limpiar la lista para que cuando llamemos no duplique la lista 
                ListComunidad.Clear();
            }
            if (dt.Rows.Count > 0)
            {
                // Cambiamos para recorrer las filas, y asignar según la posición donde esté
                foreach (DataRow i in dt.Rows)
                {
                    ListComunidad.Add(new Community
                    {
                        IdComunidad = int.Parse(i[0].ToString()),
                        Direccion = i[1].ToString(),
                        NumPortales = int.Parse(i[2].ToString()),
                        FechaCreacion = DateTime.Parse(i[3].ToString()),
                        MetrosCuadrados = decimal.Parse(i[4].ToString()),
                        Piscina = i[5].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        PisoPortero = i[6].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        Duchas = i[7].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        Parque = i[8].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        MaquinasEjercicio = i[9].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        SalaReuniones = i[10].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        PistaTenis= i[11].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        PistaPadel = i[12].ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false,
                        Name = i[13].ToString()
                    });
                }
            }
            dt.Dispose();
        }

    }
}
