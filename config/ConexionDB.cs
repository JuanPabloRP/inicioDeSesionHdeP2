using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace inicioDeSesion.config
{
    public class ConexionDB
    {
        string nombreDB;
        public ConexionDB(string _nombreDB) {
            nombreDB = _nombreDB;
        }


        public SQLiteConnection ConectarDB()
        {
            SQLiteConnection conexion_sqlite = null;

            try
            {
                conexion_sqlite = new SQLiteConnection(string.Format("Data Source={0}.db;Version=3;Compress=True;", nombreDB));
                //MessageBox.Show("conectado");

                conexion_sqlite.Open();

                // Crear la tabla solo si no existe
                string queryCrearTabla = @"CREATE TABLE IF NOT EXISTS tbl_usuario (
	                                    id	INTEGER NOT NULL,
	                                    user	TEXT NOT NULL UNIQUE,
	                                    password	TEXT NOT NULL,
	                                    PRIMARY KEY(id)
                                        )";

                SQLiteCommand cmd_crearTabla = new SQLiteCommand(queryCrearTabla, conexion_sqlite);

                //pa que ejecute el comando pero no devuelve na
                cmd_crearTabla.ExecuteNonQuery();

                //cerramos la wea
                conexion_sqlite.Close();

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



        public void AgregarRegistro(string user, string password)
        {

        }


        
    }
}

