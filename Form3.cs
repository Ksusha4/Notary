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
    public partial class Form3 : Form
    {
        db db = new db();
        public Form3(string username)
        {
            InitializeComponent();
            LoadsBox();

            textBox6.Text = username;
            textBox7.Text = username;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "нотариусDataSet2.Услуги". При необходимости она может быть перемещена или удалена.
            this.услугиTableAdapter.Fill(this.нотариусDataSet2.Услуги);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "нотариусDataSet.Услуги". При необходимости она может быть перемещена или удалена.
           

            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void CreateColumns()
        {

            dataGridView1.Columns.Add("id", "Номер заявки");
            dataGridView1.Columns.Add("Нотариус", "Нотариус");
            dataGridView1.Columns.Add("Услуги", "Услуги");
            dataGridView1.Columns.Add("Цена", "Цена");
            dataGridView1.Columns.Add("Вопрос", "Вопрос");
            dataGridView1.Columns.Add("Файл", "Файл");
            dataGridView1.Columns.Add("СтатусЗаявки", "Статус заявки");
        }

        private void CreateRows(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6));
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
             dgw.Rows.Clear();

            string query = $"select id, Нотариус, Услуги, Цена, Вопрос, Файл, СтатусЗаявки from Нотариусы where Аккаунт = '{textBox7.Text}'";

            SqlCommand cmd = new SqlCommand(query, db.con);

            db.con.Open();

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CreateRows(dgw, sqlDataReader);
            }
            sqlDataReader.Close();
            db.con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadsBox();
        }
        private void LoadsBox()
        {

            string sqlQuery = $"SELECT Стоимость, ФИО FROM Услуги where Наименование='{comboBox1.Text}'";

            try
            {
                     db.con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, db.con);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox3.Text = reader["Стоимость"].ToString();
                            textBox5.Text = reader["ФИО"].ToString();
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

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Hide();
            Form1 Назад = new Form1();
            Назад.ShowDialog();
        }

        string Names = "";
        string Email = "";
        string Notary = "";
        string Service = "";
        string Price = "";
        public string Question;
        string Files;
        string Status;
        //Значения для заполнения
        private void button1_Click(object sender, EventArgs e)
        {
            Names = textBox1.Text;
            Email = textBox2.Text;
            Notary = textBox5.Text;
            Service = comboBox1.Text;
            Price = textBox3.Text;
            Question = textBox4.Text;
            Files = linkLabel1.Text;
            Status = "На рассмотрении";
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" && comboBox1.Text == "")
            {
                MessageBox.Show("Заполните пустые значения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                NotaryOffice();
                Конвертик заявка = new Конвертик();
                заявка.ShowDialog();
               
            }
        }

        //Метод для заполнения базы данных для задания вопроса Юристу/Администратору
        private void NotaryOffice()
        {
            {

                string query = $"insert into Нотариусы(Аккаунт, ФИО,Email,Нотариус,Услуги,Цена, Вопрос, Файл, СтатусЗаявки) values ('{textBox6.Text}','{Names}', '{Email}','{Notary}','{Service}', '{Price}', '{Question}', '{Files}', '{Status}')";
                SqlCommand command = new SqlCommand(query, db.con);
                command.Parameters.AddWithValue("Аккаунт", textBox6.Text);
                command.Parameters.AddWithValue("ФИО", Names);
                command.Parameters.AddWithValue("Email", Email);
                command.Parameters.AddWithValue("Нотариус", Notary);
                command.Parameters.AddWithValue("Услуги", Service);
                command.Parameters.AddWithValue("Цена", Price);
                command.Parameters.AddWithValue("Вопрос", Question);
                command.Parameters.AddWithValue("Файл", Files);
                command.Parameters.AddWithValue("СтатусЗаявки", Files);

                try
                {
                    db.con.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    db.con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                linkLabel1.Text = openFileDialog.FileName;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
