using FoxLearn.License;
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
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        Form1 _form1 = new Form1();
        const int ProductCode = 1;

        private void btnOK_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProductID.Text);
            string productKey = txtProductKey.Text;
            if(km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if(km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "FoxLearn";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }
                    km.SaveSuretyFile(string.Format(@"{0}\Key.Lic", Application.StartupPath), lic);
                    MessageBox.Show("You have been successfully " +
                        "registered.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();

                    var first = Application.OpenForms.OfType<Form3>().FirstOrDefault();
                    if (first != null)
                    {
                        first.Hide();
                    }

                    _form1.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Your product key is invalid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            txtProductID.Text = ComputerInfo.GetComputerId();
        }
    }
}
