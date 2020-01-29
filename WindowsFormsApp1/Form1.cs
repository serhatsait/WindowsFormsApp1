using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Server= .;Database= Northwind; trusted_connection=true");

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("... ile başlayan ");
            comboBox1.Items.Add("... ile biten ");
            comboBox1.Items.Add("içinde ... geçen ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("KargoEkle1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kargoadi", textBox1.Text);
            cmd.Parameters.AddWithValue("@kargotelno", textBox2.Text);
            //SqlCommand cmd = new SqlCommand("exec KargoEkle1 @kargoadi,@kargotelno", con);
            //cmd.Parameters.AddWithValue("@kargoadi", textBox1.Text);
            //cmd.Parameters.AddWithValue("@kargotelno", textBox2.Text);
            int etk = cmd.ExecuteNonQuery();
            MessageBox.Show("Etkilenen satir = "+etk);
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("UrunAram", con);
            cmd.CommandType = CommandType.StoredProcedure;
            switch (comboBox1.Text)
            {
                case "... ile başlayan ":
                    cmd.Parameters.AddWithValue("@deger",textBox3.Text + "%");
                    break;
                case "... ile biten ":
                    cmd.Parameters.AddWithValue("@deger", "%" + textBox3.Text);
                    break;
                case "içinde ... geçen":
                    cmd.Parameters.AddWithValue("@deger", "%"+textBox3.Text+"%");
                    break;
            }
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader.GetString(0));
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"create proc UrunAram (@deger nvarchar(40)) as select ProductName from Products where ProductName like @deger", con);
            con.Open();
            int etki = cmd.ExecuteNonQuery();
            MessageBox.Show("Etkilenen satir = " + etki);
            con.Close();

        }
    }
}
