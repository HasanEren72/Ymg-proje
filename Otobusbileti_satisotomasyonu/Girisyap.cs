using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Otobusbileti_satisotomasyonu
{
    public partial class Girisyap : Form
    {
        public Girisyap()
        {
            InitializeComponent();
        }
       

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-4MS0SVU\;Initial Catalog=Otobusbiletsatis;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {

            baglanti.Open();

            SqlCommand komut = new SqlCommand("select * from kullanici_bilgileri where kullaniciadi='"+textBox1.Text+"' and sifre='"+textBox2.Text+"' ", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {

                MessageBox.Show("tebrikler giriş başarılı");
                Form2 a = new Form2();
                DialogResult x = a.ShowDialog();
            }
            else
            {
                MessageBox.Show("kullaniciadı veya şifre hatalı!");
            }
            baglanti.Close();
       
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            this.Hide();  

            Form1 z = new Form1();
            z.ShowDialog();
        }

        private void Girisyap_Load(object sender, EventArgs e)
        {

        }
    }
}
