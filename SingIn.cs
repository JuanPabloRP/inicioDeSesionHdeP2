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
using inicioDeSesion.config;

namespace inicioDeSesion
{
    public partial class frmSingIn : Form
    {

        int intentos = 0;

        public frmSingIn()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string users = "SELECT * FROM tbl_usuario";
            string queryCheckUserAndPassword = "SELECT * FROM tbl_usuario WHERE user=@User AND password=@Password LIMIT 1;";
            string queryCheckUser = "SELECT * FROM tbl_usuario WHERE user=@User LIMIT 1";


            //intentamos conectarnos a la db (en la clase ConexionDB hay validadores)
            using (SQLiteConnection conexion_sqlite = new ConexionDB("InicioSesion").ConectarDB())
            {
                conexion_sqlite.Open();

                SQLiteCommand cmd_getUsers = new SQLiteCommand(users, conexion_sqlite);


                //comando para verificar que el usuario y contraseña coincidan
                SQLiteCommand cmd_checkUserAndPassword = new SQLiteCommand(queryCheckUserAndPassword, conexion_sqlite);

                //se le agrega esto para evitar que inserten codigo sql
                cmd_checkUserAndPassword.Parameters.AddWithValue("@User", txtUser.Text);
                cmd_checkUserAndPassword.Parameters.AddWithValue("@Password", txtPassword.Text);

                // verifica que exista: 1 =  si existe / 0 =  no existe
                int userAndPasswordExist = Convert.ToInt32(cmd_checkUserAndPassword.ExecuteScalar());


                //comando para verificar que el usuario existe
                SQLiteCommand cmd_checkUser = new SQLiteCommand(queryCheckUser, conexion_sqlite);

                cmd_checkUser.Parameters.AddWithValue("@User", txtUser.Text);

                int userExist = Convert.ToInt32(cmd_checkUser.ExecuteScalar());


                if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {

                    if (userAndPasswordExist > 0)
                    {
                        MessageBox.Show("Bienvenido :D");

                        SQLiteDataReader datareader_sqlite;

                        datareader_sqlite = cmd_getUsers.ExecuteReader();

                        datareader_sqlite.Close();
                    }
                    else
                    {
                        intentos++;

                        if (intentos >= 3)
                        {
                            MessageBox.Show("Excediste el número máximo de intentos");

                            Application.Exit();
                        }

                        if (userExist == 0)
                        {
                            MessageBox.Show("El usuario no existe, por favor registrese");
                            return;
                        }

                        MessageBox.Show("El usuario o la contraseña son incorrectos");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese usuario y/o contraseña");

                }
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
