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
namespace Otobusbileti_satisotomasyonu
{
    public partial class Kayitol : Form
    {
        public Kayitol()
        {
            InitializeComponent();
        }

        static string constring = "Data Source=DESKTOP-4MS0SVU\\;Initial Catalog=Otobusbiletsatis;Integrated Security=True";

        SqlConnection baglanti =new SqlConnection(constring);    

        private void Kayitol_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            // Alanlara boş degsifre ve sifretekrar birbirine eşit ise gerekli işlemler
            if (string.IsNullOrEmpty(txtad.Text) || string.IsNullOrEmpty(txtsoyad.Text) || string.IsNullOrEmpty(msktelefon.Text) || string.IsNullOrEmpty(txtkullaniciadi.Text) || string.IsNullOrEmpty(txtsifre.Text) || string.IsNullOrEmpty(txtsifretekrar.Text))
            {
                MessageBox.Show("Lütfen gereki alanları doldurun!");

            }

            else if (txtsifre.Text == txtsifretekrar.Text)
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    string kayitlar = "insert into kullanici_bilgileri (ad,soyad,telefon,kullaniciadi,sifre,cinsiyet) values(@ad,@soyad,@telefon,@kullaniciadi,@sifre,@cinsiyet)";
                    SqlCommand komut = new SqlCommand(kayitlar, baglanti);  //komutu çalıştırmak için    kayıt ve bağlantıları gönderir 

                    komut.Parameters.AddWithValue("@ad", txtad.Text);  //kayıtları veritabanına ekledik
                    komut.Parameters.AddWithValue("@soyad", txtsoyad.Text);
                    komut.Parameters.AddWithValue("@telefon", msktelefon.Text);
                    komut.Parameters.AddWithValue("@kullaniciadi", txtkullaniciadi.Text);
                    komut.Parameters.AddWithValue("@sifre", txtsifre.Text);
                    if (radioButton1.Checked)
                    {
                        komut.Parameters.AddWithValue("@cinsiyet", radioButton1.Text);
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@cinsiyet", radioButton2.Text);
                    }


                    komut.ExecuteNonQuery();    //sonuçları çalıştırır

                    baglanti.Close();

                    MessageBox.Show("kayıtlar veritabanına eklendi");

                }
                catch (Exception hata)
                {
                    MessageBox.Show("hata meydana geldi" + hata.Message);
                    throw;
                }


                this.Hide(); 


                Form2 s = new Form2();
                s.ad = txtad.Text;  //set get edilmiş değişkenlerden nesne oluşturup onlara txtad , txtsoyad ,msktelefon
                s.soyad = txtsoyad.Text;//bileşenlerindeki bilgileri atadık
                s.telefon = msktelefon.Text;
                if (radioButton1.Checked)
                {
                    s.radiobutoncinsiyet = "Bay".ToString();

                }
                else if (radioButton2.Checked)
                {
                    s.radiobutoncinsiyet = "Bayan".ToString();

                }
                s.ShowDialog();
            
            }
            else if(txtsifre.Text != txtsifretekrar.Text)
            {
                MessageBox.Show("Girilen sifreler birbiri ile eşleşmiyor lütfen şifre ve şifre tekrar alanlarını  aynı olup olmadığını kontrol edin!");
            }



            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Girisyap z = new Girisyap();
            z.ShowDialog();
        }
    }
}
