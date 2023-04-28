using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            //carga los datos de los usuarios en la lista usuarios para las validaciones en el registro
            leerArc();
        }



        //metodos propios

        //pa llenar el archivo plano con los datos del usuario
        public void llenarArc()
        {
            StreamWriter sw = new StreamWriter("..\\..\\users.txt");

            foreach (User u in users)
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
                    foreach (User u in users)
                    {
                        if (u.user == vec[1])
                        {
                            usuarioRepetido = true;
                        }
                    }

                    if (usuarioRepetido == false)
                    {
                        users.Add(new User(Convert.ToInt32(vec[0]), vec[1], vec[2]));
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
                    users.Add(new User(id, txtUser.Text, txtPassword.Text));
                    llenarArc();
                    MessageBox.Show("Usuario registrado :)");
                    txtUser.Clear();
                    txtPassword.Clear();
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
