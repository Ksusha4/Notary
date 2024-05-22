using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Конвертик : Form
    {
        public Конвертик()
        {
            InitializeComponent();

            timer1 = new Timer();
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            timer2 = new Timer();
            timer2.Interval = 2200;
            timer2.Tick += timer2_Tick;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Start();
            label1.Text = "Заявка отправлена!";
            pictureBox1.Image = Properties.Resources.галочка;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
