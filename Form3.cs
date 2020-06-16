using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML_Izvještaj
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            using(frmGenerate frm = new frmGenerate())
            {
                Cursor = Cursors.WaitCursor;
                frm.ShowDialog();
            }
            Cursor = Cursors.Default;
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            using (frmRegistration frm = new frmRegistration())
            {
                Cursor = Cursors.WaitCursor;
                frm.ShowDialog();
            }
            Cursor = Cursors.Default;
        }
    }
}
