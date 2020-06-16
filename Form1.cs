using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XML_Izvještaj
{
    public partial class Form1 : Form
    {
        double[] h = new double[100];
        int b = 0, c = 0;
        string type = @"315*.xml";
        string shrt = @"C:\Users\Radnik\Desktop\niks\";
        Form2 testDialog = new Form2();
        Form4 testDialo3 = new Form4();
        AboutBox1 testDialo2 = new AboutBox1();

        public Form1()
        {
            InitializeComponent();
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
            var s = dateTimePicker1.Value;
            dateTimePicker2.Value = s.AddDays(1);
            toolTip1.SetToolTip(pictureBox1, "Kliknite na sliku za posjet web stranici");
            testDialog.Populate_listbox(shrt, type);
        }

        public void SendXmlOut()
        {
            string xml;
            if(dateTimePicker2.Value.Day < 10 && dateTimePicker2.Value.Month < 10)
            {
                xml = $@"C:\Users\Radnik\Desktop" +
                        $@"\niks\{textBox2.Text}_{dateTimePicker2.Value.Year}" +
                        $@"0{dateTimePicker2.Value.Month}0{dateTimePicker2.Value.Day}.xml";
            }
            else if(dateTimePicker2.Value.Day < 10)
            {
                xml = $@"C:\Users\Radnik\Desktop" +
                        $@"\niks\{textBox2.Text}_{dateTimePicker2.Value.Year}" + 
                        $@"{dateTimePicker2.Value.Month}0{dateTimePicker2.Value.Day}.xml";
            }
            else if(dateTimePicker2.Value.Month < 10)
            {
                xml = $@"C:\Users\Radnik\Desktop" +
                        $@"\niks\{textBox2.Text}_{dateTimePicker2.Value.Year}" +
                        $@"0{dateTimePicker2.Value.Month}{dateTimePicker2.Value.Day}.xml";
            }
            else
            {
                xml = $@"C:\Users\Radnik\Desktop" +
                        $@"\niks\{textBox2.Text}_{dateTimePicker2.Value.Year}" +
                        $@"{dateTimePicker2.Value.Month}{dateTimePicker2.Value.Day}.xml";
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");

            XmlWriter xmlOut = XmlWriter.Create(xml, settings);
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("Pumpe");
                    xmlOut.WriteStartElement("Pumpa");
                    xmlOut.WriteElementString("IDPumpe", textBox2.Text);
                    xmlOut.WriteElementString("BrojTankova", textBox3.Text);
                    xmlOut.WriteElementString("MatičniBroj", textBox4.Text);
                    xmlOut.WriteElementString("TelBrojKontrolera", textBox5.Text);
                    xmlOut.WriteElementString("NazivPumpe", textBox6.Text);
                    xmlOut.WriteElementString("Adresa", richTextBox1.Text);
                    xmlOut.WriteStartElement("Tankovi");
                    if (textBox3.Text != "")
                    {
                        int s = int.Parse(textBox3.Text);
                        for (int i = 0; i < s; i++)
                        {
                            xmlOut.WriteStartElement("Tank");
                            xmlOut.WriteElementString("ID", dataGridView1.Rows[i].Cells[0].Value.ToString());
                            xmlOut.WriteElementString("TankSpremnik", dataGridView1.Rows[i].Cells[1].Value.ToString());
                            xmlOut.WriteElementString("IDFMT", dataGridView1.Rows[i].Cells[2].Value.ToString());
                            xmlOut.WriteElementString("PočetnoStanje", dataGridView1.Rows[i].Cells[3].Value.ToString());
                            xmlOut.WriteElementString("UlaznaKoličina", dataGridView1.Rows[i].Cells[4].Value.ToString());
                            xmlOut.WriteElementString("IzlaznaKoličina", dataGridView1.Rows[i].Cells[5].Value.ToString());
                            xmlOut.WriteElementString("ProdajnaCijena", dataGridView1.Rows[i].Cells[6].Value.ToString());
                            xmlOut.WriteEndElement();
                        }
                    }
                    xmlOut.WriteEndElement();
                    xmlOut.WriteEndElement();
                xmlOut.WriteEndElement();
                xmlOut.Close();

                testDialog.Clear_listbox();
                testDialog.Populate_listbox(shrt, type);
        }

        string GetNumber(string text)
        {
            var numericChars = "0123456789.,".ToCharArray();
            return new String(text.Where(c => numericChars.Any(n => n == c)).ToArray());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if((dateTimePicker1.Value.Day >= DateTime.Now.Day && dateTimePicker1.Value.Month == DateTime.Now.Month && dateTimePicker1.Value.Year == DateTime.Now.Year)
             ||(dateTimePicker1.Value.Month > DateTime.Now.Month && dateTimePicker1.Value.Year == DateTime.Now.Year)
             ||(dateTimePicker1.Value.Year > DateTime.Now.Year))
            {
                MessageBox.Show("Izvještaj mora biti ranijeg datuma!", "Pogrešan unos");
                dateTimePicker1.Value = DateTime.Now.AddDays(-1);
            }
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length > 3)    //Ne koristi se 
            {
                MessageBox.Show("Predugačak ID. Unesi ponovo.");
                textBox2.Text = "";
            }
            if(textBox2.Text.Length != 0)
            {
                textBox2.BackColor = Color.NavajoWhite;
                textBox3.BackColor = Color.NavajoWhite;
                textBox4.BackColor = Color.NavajoWhite;
                textBox5.BackColor = Color.NavajoWhite;
                textBox6.BackColor = Color.NavajoWhite;
                richTextBox1.BackColor = Color.NavajoWhite;
            }
            if(textBox2.Text.Length == 0)
            {
                textBox2.BackColor = Color.White;
                textBox3.BackColor = Color.White;
                textBox4.BackColor = Color.White;
                textBox5.BackColor = Color.White;
                textBox6.BackColor = Color.White;
                richTextBox1.BackColor = Color.White;
            }
            textBox2.Text = GetNumber(textBox2.Text);
            textBox7.Text = textBox2.Text;
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = GetNumber(textBox1.Text);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\Radnik\Desktop\niks\In.xml";

            XmlReaderSettings set = new XmlReaderSettings();
            set.IgnoreComments = true;
            set.IgnoreWhitespace = true;

            XmlReader xmlIn = XmlReader.Create(path, set);
            if(xmlIn.ReadToDescendant("IDPumpe"))
            {
                textBox2.Text = xmlIn.ReadElementContentAsString();
                textBox3.Text = xmlIn.ReadElementContentAsString();
                textBox4.Text = xmlIn.ReadElementContentAsString();
                textBox5.Text = xmlIn.ReadElementContentAsString();
                textBox6.Text = xmlIn.ReadElementContentAsString();
                richTextBox1.Text = xmlIn.ReadElementContentAsString();

                textBox9.Text = xmlIn.ReadElementContentAsString();
                textBox8.Text = xmlIn.ReadElementContentAsString();

                if (xmlIn.ReadToDescendant("ID"))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("ID", typeof(int));
                    table.Columns.Add("Tank/Spremnik", typeof(string));
                    table.Columns.Add("IDFMT", typeof(int)); 
                    table.Columns.Add("PočetnoStanje", typeof(double));
                    table.Columns.Add("UlaznaKoličina", typeof(double));
                    table.Columns.Add("IzlaznaKoličina", typeof(double));
                    table.Columns.Add("ProdajnaCijena", typeof(double));

                    int s = int.Parse(textBox3.Text);
                    for (int i = 0; i < s; i++)
                    {
                        table.Rows.Add(
                        xmlIn.ReadElementContentAsInt(),
                        xmlIn.ReadElementContentAsString(),
                        xmlIn.ReadElementContentAsInt(),
                        xmlIn.ReadElementContentAsString(),
                        xmlIn.ReadElementContentAsString(),
                        xmlIn.ReadElementContentAsString(),
                        xmlIn.ReadElementContentAsString());
                        xmlIn.ReadToFollowing("ID");
                    }

                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].Width = 30;
                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].Width = 240;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].Width = 45;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].Width = 110;
                    dataGridView1.Columns[4].Width = 110;
                    dataGridView1.Columns[5].Width = 110;
                    dataGridView1.Columns[6].Width = 110;

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    for (int j = 0; j < s ; j++)
                    {
                        h[j] = (double)dataGridView1.Rows[j].Cells[6].Value;
                    }

                    dataGridView1.CurrentCell = dataGridView1[3, 0];
                    dataGridView1.BeginEdit(true);
                }
            }

            xmlIn.Close();

            if (!Control.IsKeyLocked(Keys.NumLock))
            {
                MessageBox.Show("Tipka NumLock (iznad broja 7 na tipkovnici) je isključena. \n" +
                    "Pritisnite ju za unos podataka.", "Upozorenje");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                SendXmlOut();
                MessageBox.Show("Izvještaj poslan!", "Uspjeh");
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                int iRow = dataGridView1.CurrentCell.RowIndex;

                if (iColumn == dataGridView1.Columns.Count - 1 && iRow < dataGridView1.RowCount - 1)
                {
                    dataGridView1.CurrentCell = dataGridView1[3, iRow + 1];
                }
                else if (iColumn == dataGridView1.Columns.Count - 1 && iRow == dataGridView1.RowCount - 1)
                {
                    dataGridView1.CurrentCell = dataGridView1[iColumn, iRow];
                }
                else
                {
                    dataGridView1.CurrentCell = dataGridView1[iColumn + 1, iRow];
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Unijeli ste krive podatke. Ulazna i izlaz" +
                "na količina, početno stanje i prodajna cijena" +
                " moraju biti brojevi.","Pokušajte ponovno");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            testDialog.ShowDialog(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://caljkusic.com");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            testDialo2.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            testDialo3.ShowDialog(this);
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            string message = "Jeste li sigurni da želite promijeniti jezik? Izgubit ćete " +
                "učitane podatke." +
                "\r\n\r\nAre you sure you want to switch languages? You will lose the loaded data.";
            string caption = "Upozorenje/Warning";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);

            if (result == DialogResult.Yes)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
                Form1 newform1 = new Form1();
                this.Hide();
                newform1.ShowDialog();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string message = "Jeste li sigurni da želite promijeniti jezik? Izgubit ćete " +
                "učitane podatke." +
                "\r\n\r\nAre you sure you want to switch languages? You will lose the loaded data.";
            string caption = "Upozorenje/Warning";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);

            if (result == DialogResult.Yes)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("hr-HR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("hr-HR");
                Form1 newform1 = new Form1();
                this.Hide();
                newform1.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int s = int.Parse(textBox3.Text);
            for (int i = 0; i < s; i++)
            {
                if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "")
                {
                    MessageBox.Show("Unijeli ste prazan izraz. Unesite ponovno", "Pokušajte ponovno");
                    dataGridView1.Rows[i].Cells[6].Value = h[i];
                    break;
                }
                if ((double)dataGridView1.Rows[i].Cells[6].Value > 1.25 * h[i] && b == 0)
                {
                    MessageBox.Show($"Unesena cijena ({(double)dataGridView1.Rows[i].Cells[6].Value})" +
                        $" je za više od 25% veća " +
                        $"od dosadašnje ({h[i]}). Provjerite točnost podataka i " +
                        $"koristite zareze umjesto točaka.", "Upozorenje");
                    b = 1;
                    break;
                }
                if ((double)dataGridView1.Rows[i].Cells[6].Value < 0.75 * h[i] && c == 0)
                {
                    MessageBox.Show($"Unesena cijena ({(double)dataGridView1.Rows[i].Cells[6].Value})" +
                        $" je za više od 25% manja " +
                        $"od dosadašnje ({h[i]}). Provjerite točnost podataka i " +
                        $"koristite zareze umjesto točaka.", "Upozorenje");
                    c = 1;
                    break;
                }
            }

            for(int k = 0; k < s; k++)
            {
                for(int j = 3; j < 6; j++)
                {
                    dataGridView1.Rows[k].Cells[j].Value.ToString().Replace(".",",");
                    if (dataGridView1.Rows[k].Cells[j].Value.ToString() == "")
                    {
                        MessageBox.Show("Unijeli ste prazan izraz. Unesite ponovno", "Pokušajte ponovno");
                        dataGridView1.Rows[k].Cells[j].Value = 0;
                        break;
                    }

                    if ((double)dataGridView1.Rows[k].Cells[j].Value < 0.0)
                    {
                        MessageBox.Show("Unijeli ste negativnu vrijednost.", "Pokušajte ponovno");
                        dataGridView1.Rows[k].Cells[j].Value = 0;
                    }
                }
            }
        }
    }
}
