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
using System.Data.Sql;

namespace kitaplik
{
    public partial class user : Form
    {
        public user(string ad_, string soyad_)
        {
            InitializeComponent();
            ad = ad_;
            soyad = soyad_;
        }
        string ad, soyad;

        private void button1_Click(object sender, EventArgs e)
        {
            kitapekle kitap = new kitapekle(ad.ToString(), soyad.ToString());
            kitap.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kitaplistele kitaplistele = new kitaplistele(ad.ToString(), soyad.ToString());
            kitaplistele.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            emanet emanet = new emanet(ad.ToString(), soyad.ToString());
            emanet.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            uyesayfasi uyesayfasi = new uyesayfasi(ad.ToString(), soyad.ToString());
            uyesayfasi.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            emanetlistele emanetlistele = new emanetlistele(ad.ToString(), soyad.ToString());
            emanetlistele.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            uyelistele uyelistele = new uyelistele(ad.ToString(), soyad.ToString());
            uyelistele.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void user_Load(object sender, EventArgs e)
        {
            label1.Text = ad + " " + soyad;
            button1.Text = "kitap ekle";
            button2.Text = "kitap listele";
            button3.Text = "emanet ekle";
            button4.Text = "emanet listele";
            button5.Text = "üye ekle - güncelle";
            button6.Text = "üye listele";
            button7.Text = "Girişe Dön";
            button8.Text = "Çıkış";
        }
    }
}
