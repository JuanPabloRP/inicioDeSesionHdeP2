using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inicioDeSesion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            frmSingIn si = new frmSingIn();
            si.Show();
            Hide();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            frmSignUp su = new frmSignUp();

            su.Show();
            Hide();
        }
    }
}
