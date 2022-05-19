
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otobusbileti_satisotomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Girisyap a = new Girisyap(); //girisyap  formuna gider

            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kayitol b = new Kayitol();    //kayıt ol fformuna gider
            b.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
