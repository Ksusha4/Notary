using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        db db = new db();
        public Form2()
        {
            InitializeComponent();

            login1.Text = "Введите Логин";
            pass1.Text = "Введите Пароль";
            pass1.PasswordChar = login1.PasswordChar;
            login1.ForeColor = Color.Gray;
            pass1.ForeColor = Color.Gray;
        }

        string admin = "";
        bool reg = false;
        string userpass = "";
        string userfio = "";
        string userlogin = "";

        public string SenderName { get; internal set; }

        private void label2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 Назад = new Form1();
            Назад.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userlogin = login1.Text;
            userpass = pass1.Text;
            registration();
        }

        private void registration()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"select Логин, Пароль from Регистрация where Логин='{userlogin}'";
            SqlCommand command = new SqlCommand(query, db.con);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            db.con.Open();
            if (table.Rows.Count == 0)
            {
                SqlCommand insertCommand = new SqlCommand($"insert into Регистрация (Логин, Пароль) values ('{userlogin}', '{userpass}')", db.con);
                if (insertCommand.ExecuteNonQuery() == 1)
                    MessageBox.Show("Регистрация прошла успешно!", "Успешно", MessageBoxButtons.OK);
                this.Hide();
                Form2 form3 = new Form2();
                form3.ShowDialog();
            }
            db.con.Close();
        }

        private void pass1_Enter(object sender, EventArgs e)
        {
            if (pass1.Text == "Введите Пароль")
            {
                pass1.Text = "";
                pass1.ForeColor = Color.Black;
                pass1.PasswordChar = '*';
            }
        }

        private void pass1_Leave(object sender, EventArgs e)
        {
            if (pass1.Text == "")
            {
                pass1.Text = "Введите Пароль";
                pass1.ForeColor = Color.Gray;
                pass1.PasswordChar = login1.PasswordChar;
            }
        }

        private void login1_Enter(object sender, EventArgs e)
        {
            if (login1.Text == "Введите Логин")
            {
                login1.Text = "";
                login1.ForeColor = Color.Black;
            }
        }

        private void login1_Leave(object sender, EventArgs e)
        {
            if (login1.Text == "")
            {
                login1.Text = "Введите Логин";
                login1.ForeColor = Color.Gray;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 Назад = new Form1();
            Назад.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
