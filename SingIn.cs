using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;


namespace inicioDeSesion
{
    public partial class frmSingIn : Form
    {
        int intentos = 0;
        List<User> usuarios = User.usuarios;
        User user;


        public frmSingIn()
        {
            InitializeComponent();
            leerArc();

        }

        public void llenarArc()
        {

            StreamWriter sw = new StreamWriter("..\\..\\users.txt");

            foreach (User u in usuarios)
            {
                sw.WriteLine($"{u.id}|{u.user}|{u.password}");
            }
            sw.Close();

        }

        public void leerArc()
        {
            StreamReader sr = new StreamReader("..\\..\\users.txt");
            string linea;
            linea = sr.ReadLine();
            bool usuarioRepetido = false;
            while (linea != null)
            {
                string[] vec = linea.Split('|');
                try
                {
                    foreach (User u in usuarios)
                    {
                        if (u.user == vec[1])
                        {
                            usuarioRepetido = true;
                        }
                    }

                    if (usuarioRepetido == false)
                    {
                        usuarios.Add(new User(Convert.ToInt32(vec[0]), vec[1], vec[2]));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }

                linea = sr.ReadLine();
            }
            sr.Close();
        }


        private void btnSignIn_Click(object sender, EventArgs e)
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


                cmd_sqlite.CommandText = string.Format("SELECT * FROM tblUsuario WHERE user='{0}' AND password='{1}'", txtUser.Text, txtPassword.Text);                

                int count = Convert.ToInt32(cmd_sqlite.ExecuteScalar());

                if (count > 0) { 
                    MessageBox.Show("Usuario registrado :)");
                }
                else
                {
                    intentos++;

                    if (intentos >= 3)
                    {
                        Application.Exit();
                    }

                    MessageBox.Show("El usuario o la contraseña estan incorrectas");
                }


                conexion_sqlite.Close();


                txtUser.Clear();
                txtPassword.Clear();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error al registrar al usuario " + err);
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
