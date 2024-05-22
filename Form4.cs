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
    public partial class Form4 : Form
    {
        int Ids;
        string Files;
        db db = new db();
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bid();
        }

        private string stringCon()
        {
            return @"Data Source=435А-005;Initial Catalog=Нотариус;Integrated Security=True";
        }

        private void Bid()
        {

            string sqlQuery = $"select * from Нотариусы where id='{comboBox1.Text}'";

            try
            {

                    db.con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, db.con);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox1.Text = reader["ФИО"].ToString();
                            textBox2.Text = reader["Email"].ToString();
                            textBox3.Text = reader["Нотариус"].ToString();
                            textBox5.Text = reader["Услуги"].ToString();
                            text.Text = reader["Вопрос"].ToString();
                            linkLabel1.Text = reader["Файл"].ToString();
                            label9.Text ="Статус: " + reader["СтатусЗаявки"].ToString();
                        }
                    }
                    reader.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            db.con.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "нотариусDataSet3.Нотариусы". При необходимости она может быть перемещена или удалена.
            this.нотариусыTableAdapter1.Fill(this.нотариусDataSet3.Нотариусы);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ids = Convert.ToInt32(comboBox1.Text);
            Files = "Принята";
            if (textBox1.Text == "")
            {
                MessageBox.Show("Выберите заявку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Status();
            }
        }

        private void Status()
        {
            {
                string query = $"update Нотариусы set СтатусЗаявки = '{Files}' where id = {Ids} ";
                SqlCommand command = new SqlCommand(query, db.con);
                command.Parameters.AddWithValue("idЗаявки", Ids);
                command.Parameters.AddWithValue("СтатусЗаявки", Files);

                try
                {
                    db.con.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show("Удачно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db.con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ids = Convert.ToInt32(comboBox1.Text);
            Files = "Отказана";
            if (textBox1.Text == "")
            {
                MessageBox.Show("Выберите заявку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Status();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 Назад = new Form1();
            Назад.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Bid();
        }
    }
}
