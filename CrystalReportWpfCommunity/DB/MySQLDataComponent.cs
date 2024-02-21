using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_NeighborhoodCommunity.DB
{
    internal class MySQLDataManagement
    {
        public static void ExecuteNonQuery(String SQL, String cnstr)
        {
            MySqlConnection con = new MySqlConnection(cnstr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(SQL, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public static DataTable LoadData(String SQL, String cnstr)
        {

            MySqlConnection con = new MySqlConnection(cnstr);
            con.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(SQL, con);
            da.Fill(dt);
            da.Dispose();
            con.Close();
            return dt;
        }

        public static object ExecuteScalar(string SQL, string cnstr)
        {      
                using (MySqlConnection con = new MySqlConnection(cnstr))
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand(SQL, con))
                    {
                        return cmd.ExecuteScalar();
                    }
                }
        }
    }
}

