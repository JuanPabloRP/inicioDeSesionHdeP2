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


        string connectionString = "Data Source=.;Initial Catalog=InicioSesion;User ID=DESKTOP-8ASV2F4\\jprp;Password=";



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
            bool ok = false;


            foreach (User u in usuarios)
            {
                if (txtUser.Text.Trim() == u.user)
                {
                    if (txtPassword.Text.Trim() == u.password)
                    {
                        user = u;
                        ok = true;
                        break;
                    }
                    else
                    {
                        ok = false;


                    }
                }
                else
                {
                    ok = false;
                }
            }

            if (ok)
            {

                MessageBox.Show("Bienvenido");

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

            

        }

        private void pbxBack_Click(object sender, EventArgs e)
        {

            frmPrincipal principal = new frmPrincipal();
            principal.Show();
            Hide();
        }

        

    }
}
