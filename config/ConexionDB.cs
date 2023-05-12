using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inicioDeSesion.config
{
    public class ConexionDB
    {
        string nombreDB;
        public ConexionDB(string _nombreDB) {
            nombreDB = _nombreDB;
        }


        public System.Data.SQLite.SQLiteConnection ConectarDB()
        {
            SQLiteConnection conexion_sqlite = null;

            try
            {
                conexion_sqlite = new SQLiteConnection(string.Format("Data Source={0}.db;Version=3;Compress=True;", nombreDB));
               
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error conectando a la base de datos " + nombreDB + "\nError: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            

            return conexion_sqlite;
        }
    }
}
