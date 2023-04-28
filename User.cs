using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inicioDeSesion
{
    public class User
    {

        public static List<User> usuarios = new List<User>();
        public int id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
       

        public User(int _id, string _name, string _password)
        {
            id = _id;
            user = _name;
            password = _password;
            
        }


        //esto sirve para el login
        public bool validar(string _user, string _pass)
        {
            bool res = false;

            if (user == _user && password == _pass)
            {
                res = true;
            }

            return res;
        }

    }
}
