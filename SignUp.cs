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
                try
                {
                    SQLiteConnection conexion_sqlite;
                    SQLiteCommand cmd_sqlite;
                    


                    conexion_sqlite = new SQLiteConnection("Data Source=InicioSesion.db;Version=3;Compress=True;");


                    conexion_sqlite.Open();

                    cmd_sqlite = conexion_sqlite.CreateCommand();

                    try
                    {
                        cmd_sqlite.CommandText = string.Format("SELECT * FROM tblUsuario WHERE user='{0}'", txtUser.Text);



                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }




                    cmd_sqlite.CommandText = string.Format("INSERT INTO tblUsuario values({0},'{1}','{2}')", id, txtUser.Text, txtPassword.Text);


                    /*
                     // SQLiteDataReader datareader_sqlite;
                    
                    datareader_sqlite = cmd_sqlite.ExecuteReader();


                    while (datareader_sqlite.Read())
                    {

                        int idU = Convert.ToInt32(datareader_sqlite.GetString(0));
                        string nameU = datareader_sqlite.GetString(1);
                        string passU = datareader_sqlite.GetString(2);

                        MessageBox.Show(idU + nameU + passU);

                    }
                    */

                    conexion_sqlite.Close();

                    MessageBox.Show("Usuario registrado :)");

                    txtUser.Clear();
                    txtPassword.Clear();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error al registrar al usuario\n\nError: " + err);
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
