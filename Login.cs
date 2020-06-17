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
    public partial class Login : Form
    {
        ClientController crtl;
        public Login(ClientController crtl)
        {
            InitializeComponent();
            this.crtl = crtl;
            textBox2.PasswordChar = '*';
            textBox2.CharacterCasing = CharacterCasing.Lower;
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox1.TextAlign = HorizontalAlignment.Center;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(textBox1.Text);
            string pass = textBox2.Text;
            if (crtl.Login(id, pass) == true)
            {
                textBox2.Clear();
                var MyForm = new MainForm(crtl);
                MyForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid password/username.");
            }
        }
    }
}
