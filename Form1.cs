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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public string username;
        db db = new db();
        public Form1()
        {
            InitializeComponent();

            pass2.PasswordChar = login1.PasswordChar;
        }
        string userpass = "";
        string userlogin = "";
        private void vhod_Click(object sender, EventArgs e)
        {
            userlogin = login1.Text;
            userpass = pass2.Text;
            login();
        }

        private void login()
        {
            username = login1.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"select Логин, Пароль from Регистрация where Логин='{userlogin}' and Пароль='{userpass}'";
            SqlCommand command = new SqlCommand(query, db.con);
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)

            {
                MessageBox.Show("Вы успешно вошли!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                if (userlogin == "admin")
                {
                    Form4 form4 = new Form4();
                    form4.ShowDialog();
                    Hide();
                }
                else 
                {
                    Form3 t = new Form3(username);
                    t.Show();
                }
                db.con.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();

                db.con.Close();


            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 Рег = new Form2();
            Рег.ShowDialog();
        }

        private void pass2_Enter(object sender, EventArgs e)
        {
            if (pass2.Text == "")
            {
                pass2.Text = "";
                pass2.ForeColor = Color.Black;
                pass2.PasswordChar = '*';
            }
        }

        private void pass2_Leave(object sender, EventArgs e)
        {
            if (pass2.Text == "")
            {
                pass2.PasswordChar = login1.PasswordChar;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
