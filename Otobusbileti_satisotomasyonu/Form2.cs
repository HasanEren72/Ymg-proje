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
    public partial class Form2 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        public Form2()
        {
            InitializeComponent();
        }
        public string ad { get; set; } //form2 formuna veri göndermek için
        public string soyad { get; set; }
        public String telefon { get; set; }
        public String radiobutoncinsiyet { get; set; }
        
        
        private void Form2_Load(object sender, EventArgs e)
        {


            cb1otobusSec.SelectedIndex = 0;
            label2.Text = ad;
            label3.Text = soyad;


        }

        private void cb1otobusSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb1otobusSec.Text == "KamilKoc(2+1)")
            {
                koltukdoldur(10, 4, false);

            }
            else if (cb1otobusSec.Text == "Maltego(2+1)")
            {
                koltukdoldur(12, 4, true);
            }
            else if (cb1otobusSec.Text == "HatayhasTurizm(2+2)")
            {
                koltukdoldur(10, 5, false);
            }
            else
            {

            }


            void koltukdoldur(int satir, int stun, bool arkabeslimi)
            {



                int koltukno = 1;

            yavasgetir:
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button)
                    {
                        Button btn = ctrl as Button;
                        if (btn.Text == "Bileti_Al"||btn.Text== "Çıkış")
                        {
                            continue;
                        }
                        else
                        {
                            this.Controls.Remove(ctrl);
                            goto yavasgetir;
                        }
                    }
                }


                for (int i = 0; i < satir; i++)
                {
                    for (int j = 0; j < stun; j++)
                    {
                        if (cb1otobusSec.Text == "HatayhasTurizm(2+2)")
                        {
                            if (j == 2)
                                continue;

                        }
                        else
                        {
                            if (j == 1)
                                continue;
                        }


                        Button koltuk = new Button();
                        koltuk.Height = 40;
                        koltuk.Width = 40;
                        koltuk.Top = 30 + (i * 45);
                        koltuk.Left = 5 + (j * 45);


                        koltuk.Text = koltukno.ToString();
                        koltukno++;
                        koltuk.ContextMenuStrip = contextMenuStrip1;
                        koltuk.MouseDown += Koltuk_MouseDown;
                        this.Controls.Add(koltuk);
                    }

                }



            }

        }
        Button tıklanan;
        private void Koltuk_MouseDown(object sender, MouseEventArgs e)
        {
            tıklanan = sender as Button;
        }
        private void bayKoltukAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tıklanan.BackColor = Color.Blue;
        }

        private void bayanKoltukAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tıklanan.BackColor = Color.Red;
        }

        private void secilenBiletiİptalEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

       
     
        private void cb2nereden_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb2nereden.AutoCompleteCustomSource.Add(cb2nereden.Text);
        }

        private void cb3nereye_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb3nereye.AutoCompleteCustomSource.Add(cb3nereye.Text);



             if (cb2nereden.SelectedIndex >= 75 && cb3nereye.SelectedIndex >=75)
            {
                txtfiyat.Text = "350";
            }
            else if (cb2nereden.SelectedIndex >= 70 && cb3nereye.SelectedIndex >=60)
            {
                txtfiyat.Text = "200";
            }
            else if (cb2nereden.SelectedIndex >=60 && cb3nereye.SelectedIndex >=50)
            {
                txtfiyat.Text = "100";
            }
            else if (cb2nereden.SelectedIndex >= 50 && cb3nereye.SelectedIndex >=30)
            {
                txtfiyat.Text = "80";
            }
            else if (cb2nereden.SelectedIndex >= 40 &&cb3nereye.SelectedIndex >= 30)
            {
                txtfiyat.Text = "75";
            }
            else if (cb2nereden.SelectedIndex >= 30&& cb3nereye.SelectedIndex >= 20)
            {
                txtfiyat.Text = "50";
            }
            else if (cb2nereden.SelectedIndex >= 20&& cb3nereye.SelectedIndex >= 10)
            {
                txtfiyat.Text = "45";
            }
            else if (cb2nereden.SelectedIndex >= 10&& cb3nereye.SelectedIndex >= 10)
            {
                txtfiyat.Text = "45";
            }
             else
            {
                txtfiyat.Text = "30";

            }


        }

        void Biletsatiskayitlarini_Veritabanina_Gonder()
        {
            try
            {
                baglanti = new SqlConnection("Data Source=DESKTOP-4MS0SVU;Initial Catalog=Otobusbiletsatis;Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Alinan_BiletlerT (ad,soyad,telefon,cinsiyet,nereden,nereye,tarih,fiyat,koltukno,Firma) values(@ad,@soyad,@telefon,@cinsiyet,@nereden,@nereye,@tarih,@fiyat,@koltukno,@Firma)", baglanti);


                

                if (ad == "" || soyad == "" || telefon == "")
                {
                   
                    SqlCommand sqlCommand = new SqlCommand();
                    baglanti.Open();
                    da = new SqlDataAdapter("select * from kullanici_bilgileri (ad,soyad,telefon,kullaniciadi,sifre,cinsiyet) ", baglanti);
                    dataGridView1.DataSource=da;    



                    baglanti.Close();

                }
                else
                {
                    komut.Parameters.AddWithValue("@ad", ad);  //kayıtları veritabanına ekledik
                    komut.Parameters.AddWithValue("@soyad", soyad);
                    komut.Parameters.AddWithValue("@telefon", telefon);
                    komut.Parameters.AddWithValue("@cinsiyet", radiobutoncinsiyet);
                }


               
               
                komut.Parameters.AddWithValue("@nereden", cb2nereden.Text);
                komut.Parameters.AddWithValue("@nereye", cb3nereye.Text);
                komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Text);
                komut.Parameters.AddWithValue("@fiyat", txtfiyat.Text);
                komut.Parameters.AddWithValue("@koltukno", tıklanan.Text);
                komut.Parameters.AddWithValue("@Firma", cb1otobusSec.SelectedItem.ToString());


                komut.ExecuteNonQuery();

                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("hata meydana geldi"+hata.Message);
               
            }
           

        }
        void BiletsatiskayitlariGetir()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-4MS0SVU;Initial Catalog=Otobusbiletsatis;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from Alinan_BiletlerT", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Biletsatiskayitlarini_Veritabanina_Gonder();
            BiletsatiskayitlariGetir();


            if (cb1otobusSec.SelectedIndex == -1 || cb2nereden.SelectedIndex == -1 || cb3nereye.SelectedIndex == -1)
            {
                MessageBox.Show("lütfen gerekli alanları doldurunuz!");
                return; 
            }
            else if(cb2nereden.Text==cb3nereye.Text)
            {
                MessageBox.Show("lütfen farklı şehirler seçiniz");      
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void seçilenBiletiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu=" Delete From  Alinan_BiletlerT Where  bilet_id=@bilet_id";

                komut = new SqlCommand(sorgu, baglanti);





                //listView1.Items.RemoveAt(listView1.SelectedItems[0].Index);
                tıklanan.BackColor = Color.White;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
