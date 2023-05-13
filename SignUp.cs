using inicioDeSesion.config;
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

       

        public static int verificarIDUnico(SQLiteConnection _conexionsqlite)
        {
            Random n = new Random();
            int id;
            bool idRepetido;
            do
            {
                idRepetido = false;
                id = n.Next(1000000, 9999999);

                try {
                    string checkIDUnico = string.Format("SELECT COUNT(*) FROM tbl_usuario WHERE id={0}", id);
                    

                    SQLiteCommand cmd_checkIDUnico = new SQLiteCommand(checkIDUnico, _conexionsqlite);

                    
                    if (Convert.ToInt32(cmd_checkIDUnico.ExecuteScalar()) > 0)
                    {
                        idRepetido = true;
                    }
                }
                catch (SQLiteException ex) {
                    MessageBox.Show("Error (sql): " + ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                }

            } while (idRepetido == true);


            return id;
        }



        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string queryAddUser = "INSERT INTO tbl_usuario VALUES (@id, @user, @password)";
            string queryLookingForUser = "SELECT COUNT(user) FROM tbl_usuario WHERE user=@user";
            

            using (SQLiteConnection conexion_sqlite = new ConexionDB("InicioSesion").ConectarDB()) {
                conexion_sqlite.Open();

                int id = verificarIDUnico(conexion_sqlite);

                SQLiteCommand cmd_lookingForUser = new SQLiteCommand(queryLookingForUser, conexion_sqlite);
                cmd_lookingForUser.Parameters.AddWithValue("@user", txtUser.Text);

                if(!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    //verifica que el usuario no este creado
                    if (Convert.ToInt32(cmd_lookingForUser.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Error, el usuario ya existe");
                        return;
                    }

                    //comando para agregar usuarioi
                    SQLiteCommand cmd_addUser = new SQLiteCommand(queryAddUser, conexion_sqlite);

                    
                    //parametros pa que no nos metan cosas raras a la DB, si sabe brr
                    cmd_addUser.Parameters.AddWithValue("@id", id);
                    cmd_addUser.Parameters.AddWithValue("@user", txtUser.Text);
                    cmd_addUser.Parameters.AddWithValue("@password", txtPassword.Text);

                    
                    SQLiteDataReader datareader_sqlite;

                    datareader_sqlite = cmd_addUser.ExecuteReader();

                    datareader_sqlite.Close();


                    MessageBox.Show("Registrado");

                }
                else
                {
                    MessageBox.Show("Ingrese el usuario y la contraseña");
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
