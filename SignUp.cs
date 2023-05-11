using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace inicioDeSesion
{
    public partial class frmSignUp : Form
    {
        List<User> users = User.usuarios;
        
        public frmSignUp()
        {
            InitializeComponent();

        
        }



        

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Random n = new Random();
            int id = 0;
            bool idRepetido = false;
            bool userRepetido = false;

            if (txtUser.Text.Trim() != "" && txtPassword.Text.Trim() != "" )
            {
                do
                {
                    id = n.Next(1000000, 9999999);

                    if (users.Count > 0)
                    {
                        foreach (User u in users)
                        {

                            if (id == u.id)
                            {
                                idRepetido = true;
                                break;
                            }


                        }
                    }

                } while (idRepetido == true);


                foreach (User u in users)
                {
                    if (txtUser.Text.Equals(u.user))
                    {
                        userRepetido = true;
                        MessageBox.Show("Por favor ingrese otro nombre de usuario");
                        txtUser.Clear();
                        break;
                    }
                }


                if (userRepetido == false)
                {
                    try
                    {
                        //Utilizamos estos tres objetos de SQLite
                        SQLiteConnection conexion_sqlite;
                        SQLiteCommand cmd_sqlite;
                        SQLiteDataReader datareader_sqlite;

                        //Crear una nueva conexión de la base de datos
                        conexion_sqlite = new SQLiteConnection("Data Source=InicioSesion.db;Version=3;Compress=True;");

                        //Abriremos la conexión
                        conexion_sqlite.Open();

                        //Creando el comando SQL
                        cmd_sqlite = conexion_sqlite.CreateCommand();
                        

                        cmd_sqlite.CommandText = string.Format("INSERT INTO tblUsuario values({0},'{1}','{2}')", id, txtUser.Text, txtPassword.Text);

                        datareader_sqlite = cmd_sqlite.ExecuteReader();

                        while (datareader_sqlite.Read())
                        {
                            //Mostrando los datos

                            int idU = Convert.ToInt32(datareader_sqlite.GetString(0));
                            string nameU = datareader_sqlite.GetString(1);
                            string passU = datareader_sqlite.GetString(2);

                            MessageBox.Show(idU + nameU + passU);

                        }


                        conexion_sqlite.Close();

                        MessageBox.Show("Usuario registrado :)");

                        txtUser.Clear();
                        txtPassword.Clear();
                    }
                    catch (Exception err) {
                        MessageBox.Show("Error al registrar al usuario " + err);
                    }
                    
                }

            }
            else
            {
                MessageBox.Show("Los campos estan vacios :(");
            }
        }

        private void pbxBack_Click(object sender, EventArgs e)
        {
            frmPrincipal principal = new frmPrincipal();
            principal.Show();
            Hide();
        }


    }
}
