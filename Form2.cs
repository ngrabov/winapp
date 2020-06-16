using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace XML_Izvještaj
{
    public partial class Form2 : Form
    {
        string type = @"315*.xml";
        string shrt = @"C:\Users\Radnik\Desktop\niks\";
        public Form2()
        {
            InitializeComponent();
        }
        public void Populate_listbox(string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                listBox1.Items.Add(file.Name);
            }
        }

        public void Clear_listbox()
        {
            listBox1.Items.Clear();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Clear_listbox();
            Populate_listbox(shrt, type);
            int j = listBox1.Items.Count;
            for (int i = 0; i < j ; i++)
            {
                if(!listBox1.Items[i].ToString().ToLower().Contains(textBox1.Text.ToString().ToLower()))
                {
                    listBox1.Items.Remove(listBox1.Items[i]);
                    i--;
                    j--;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pat = $@"C:\Users\Radnik\Desktop\niks\{listBox1.SelectedItem}";

            XmlReaderSettings st = new XmlReaderSettings();
            st.IgnoreComments = true;
            st.IgnoreWhitespace = true;

            XmlReader inx = XmlReader.Create(pat, st);
            if (inx.ReadToDescendant("BrojTankova"))
            {
                int s = inx.ReadElementContentAsInt();
                inx.ReadToFollowing("ID");

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Tank/Spremnik", typeof(string));
                dt.Columns.Add("IDFMT", typeof(string));
                dt.Columns.Add("PočetnoStanje", typeof(double));
                dt.Columns.Add("UlaznaKoličina", typeof(double));
                dt.Columns.Add("IzlaznaKoličina", typeof(double));
                dt.Columns.Add("ProdajnaCijena", typeof(double));

                for (int i = 0; i < s; i++)
                {
                    dt.Rows.Add(
                    inx.ReadElementContentAsString(),
                    inx.ReadElementContentAsString(),
                    inx.ReadElementContentAsString(),
                    inx.ReadElementContentAsString(),
                    inx.ReadElementContentAsString(),
                    inx.ReadElementContentAsString(),
                    inx.ReadElementContentAsString());
                    inx.ReadToFollowing("ID");
                }
                double ps = .0;
                double uk = .0;
                double ik = .0;
                double km = .0;

                for (int j = 0; j < s; j++)
                {
                    ps += (double)dt.Rows[j][3];
                    uk += (double)dt.Rows[j][4];
                    ik += (double)dt.Rows[j][5];
                    km += (double)dt.Rows[j][6] * (double)dt.Rows[j][5];
                }

                km = Math.Round(km, 4);
                dt.Rows.Add("", "Ukupno", "", ps, uk, ik, km);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = 20;
                dataGridView1.Columns[1].Width = 140;
                dataGridView1.Columns[2].Width = 45;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 80;
                dataGridView1.Columns[5].Width = 80;
                dataGridView1.Columns[6].Width = 80;
            }
            inx.Close();
        }
    }
}
