using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_NeighborhoodCommunity.DB;

namespace WPF_NeighborhoodCommunity.Models
{
    internal class Community
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        // Declaro la constante para la conexión a la BDD
        private const String cnstr = "server=localhost;uid=miguel;pwd=miguel;database=community";
        // Modelo de la lista de registros a mostrar
        private ObservableCollection<Community> _listComunidad;
        private int _idComunidad;
        private String _direccion = "";
        private int _numPortales;
        private DateTime _fechaCreacion;
        private decimal _metrosCuadrados;
        private bool _piscina;
        private bool _pisoPortero;
        private bool _duchas;
        private bool _parque;
        private bool _maquinasEjercicio;
        private bool _salaReuniones;
        private bool _pistaTenis;
        private bool _pistaPadel;
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
            String SQL = $"INSERT INTO comunidad (direccion, numPortales, fechaCreacion, metrosCuadrados, piscina, pisoPortero, duchas, parque, maquinasEjercicio, salaReuniones, pistaTenis, pistaPadel) VALUES ('{Direccion}', '{NumPortales}', '{FechaCreacion.ToString("yyyy-MM-dd")}', '{MetrosCuadrados}', '{Piscina}', '{PisoPortero}', '{Duchas}', '{Parque}', '{MaquinasEjercicio}', '{SalaReuniones}', '{PistaTenis}', '{PistaPadel}');";
            // Usaremos las clases de la librería de MySQL para ejecutar queries
            // Asegúrate de tener instalado el paquete MySQL.Data

            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            // Para que cargue si hemos agregado nueva comunidad
            // LoadComunidades();
        }

        public void UpdateComunidad()
        {
            String SQL = $"UPDATE comunidad SET direccion = '{Direccion}', numPortales = '{NumPortales}', fechaCreacion = '{FechaCreacion.ToString("yyyy-MM-dd")}', metrosCuadrados = '{MetrosCuadrados}', piscina = '{Piscina}', pisoPortero = '{PisoPortero}', duchas = '{Duchas}', parque = '{Parque}', maquinasEjercicio = '{MaquinasEjercicio}', salaReuniones = '{SalaReuniones}', pistaTenis = '{PistaTenis}', pistaPadel = '{PistaPadel}' WHERE idcomunidad = '{IdComunidad}';";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void DeleteComunidad()
        {
            String SQL = $"DELETE FROM comunidad WHERE idcomunidad = '{IdComunidad}';";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void LoadComunidades()
        {
            String SQL = $"SELECT idcomunidad, direccion, numPortales, fechaCreacion, metrosCuadrados, piscina, pisoPortero, duchas, parque, maquinasEjercicio, salaReuniones, pistaTenis, pistaPadel FROM comunidad;";
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
                        Piscina = bool.Parse(i[5].ToString()),
                        PisoPortero = bool.Parse(i[6].ToString()),
                        Duchas = bool.Parse(i[7].ToString()),
                        Parque = bool.Parse(i[8].ToString()),
                        MaquinasEjercicio = bool.Parse(i[9].ToString()),
                        SalaReuniones = bool.Parse(i[10].ToString()),
                        PistaTenis = bool.Parse(i[11].ToString()),
                        PistaPadel = bool.Parse(i[12].ToString())
                    });
                }
            }
            dt.Dispose();
        }
    }
}
