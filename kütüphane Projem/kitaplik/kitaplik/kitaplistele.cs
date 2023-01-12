﻿using System;
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
    public partial class kitaplistele : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
        DataSet ds;
        SqlDataAdapter da;

        public kitaplistele(string ad_, string soyad_)
        {
            InitializeComponent();
            ad = ad_;
            soyad = soyad_;
        }
        string ad, soyad;
        void griddoldur()
        {
            conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
            da = new SqlDataAdapter("Select kitapadi,yazari,kitapturu,baskiyili,ISBN,sayfasayisi,kitapdili,yayinevi,adet,aciklama From book", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "book");
            dataGridView1.DataSource = ds.Tables["book"];
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user user = new user(ad.ToString(), soyad.ToString());
            user.Show();
            this.Hide();
        }

        private void kitaplistele_Load(object sender, EventArgs e)
        {
            label1.Text = ad + " " + soyad;
            button1.Text = "Geri Dön";
            griddoldur();
        }
    }
}
