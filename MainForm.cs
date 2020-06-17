using AgentieModell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentieClient
{
    public partial class MainForm : Form
    {
        ClientController crtl;
       
        public MainForm(ClientController crtl)
        {
            InitializeComponent();
            this.crtl = crtl;
            load(crtl.findAll());
            crtl.updateEvent += userUpdate;
        }

        private void load(IEnumerable<Excursie> data)
        {
            DataTable meciuri = new DataTable("Meciuri");
            DataColumn id = new DataColumn("Id");
            DataColumn plecare = new DataColumn("Plecare");
            DataColumn pret = new DataColumn("Pret");
            DataColumn locuri = new DataColumn("Locuri disponibile");
            DataColumn transport = new DataColumn("Transport");
            DataColumn nume = new DataColumn("Nume");

            meciuri.Columns.Add(id);
            meciuri.Columns.Add(plecare);
            meciuri.Columns.Add(pret);
            meciuri.Columns.Add(locuri);
            meciuri.Columns.Add(transport);
            meciuri.Columns.Add(nume);

            foreach (Excursie meci in data)
            {
                DataRow row = meciuri.NewRow();

                row["Id"] = meci.Id.ToString();
                row["Plecare"] = meci.Plecare.ToString();
                row["Pret"] = meci.Pret.ToString();
                row["Locuri disponibile"] = meci.Locuri_disp.ToString();
                row["Transport"] = meci.Transport.ToString();
                row["Nume"] = meci.Nume.ToString();
                meciuri.Rows.Add(row);
            }

            dataGridView1.DataSource = meciuri;
        }

        public void load2(Rezervare ex)
        {
            
            foreach(DataGridViewRow t in dataGridView1.Rows)
            {
                if(Convert.ToInt32(t.Cells[0].Value) == ex.ExcId)
                {
                    int a = Convert.ToInt32(t.Cells[3].Value) - ex.NumTickets;
                    t.Cells[3].Value = a;
                    break;
                }
            }
            Console.WriteLine("gata");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Excursie> data = crtl.cauta(textBox1.Text,Int32.Parse(textBox2.Text),Int32.Parse(textBox3.Text));
            DataTable meciuri = new DataTable("Meciuri");
            DataColumn id = new DataColumn("Id");
            DataColumn plecare = new DataColumn("Plecare");
            DataColumn pret = new DataColumn("Pret");
            DataColumn locuri = new DataColumn("Locuri disponibile");
            DataColumn transport = new DataColumn("Transport");
            DataColumn nume = new DataColumn("Nume");

            meciuri.Columns.Add(id);
            meciuri.Columns.Add(plecare);
            meciuri.Columns.Add(pret);
            meciuri.Columns.Add(locuri);
            meciuri.Columns.Add(transport);
            meciuri.Columns.Add(nume);



            foreach (Excursie meci in data)
            {
                DataRow row = meciuri.NewRow();

                row["Id"] = meci.Id.ToString();
                row["Plecare"] = meci.Plecare.ToString();
                row["Pret"] = meci.Pret.ToString();
                row["Locuri disponibile"] = meci.Locuri_disp.ToString();
                row["Transport"] = meci.Transport.ToString();
                row["Nume"] = meci.Nume.ToString();
                meciuri.Rows.Add(row);
            }

            dataGridView2.DataSource = meciuri;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ( dataGridView1.SelectedRows != null)
            {
                try
                {
                    IEnumerable<Rezervare> l = crtl.getAll();
                    int i = 0;
                    foreach (Rezervare x in l)
                        i++;
                    
                    Excursie trip = new Excursie()
                    {
                        Id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value),
                        Plecare = Convert.ToString(dataGridView1.SelectedRows[0].Cells[1].Value),
                        Pret = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[2].Value),
                        Locuri_disp = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value),
                        Transport = Convert.ToString(dataGridView1.SelectedRows[0].Cells[4].Value),             
                        Nume = Convert.ToString(dataGridView1.SelectedRows[0].Cells[5].Value),
                    };
                    
                    crtl.adauga(i + 1, textBox4.Text, textBox5.Text, Int32.Parse(textBox6.Text), trip, new Agent(1, "pass"));

                }
                catch (Exception er)
                {
                    Console.WriteLine("Not good");
                }
            }
            else
            {
                MessageBox.Show("Trebuie completate textbox-urile si selectat un meci.");
            }
        }
        public delegate void upd(Rezervare t);
        private void userUpdate(object obj,RezervareEventArgs rezv)
        {
    
            Console.WriteLine("asdf");
            Rezervare t = (Rezervare)rezv.Data;
            dataGridView1.BeginInvoke(new upd(load2), new Object[] {t});
        }
    }
}
