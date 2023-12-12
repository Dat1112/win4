using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        public Form2()
        {
            InitializeComponent();
        }
         
        private void Form2_Load(object sender, EventArgs e)
        {
            load_dvgloaisach();
        }
        private void load_dvgloaisach()
        {
            //b1:ket noi db
            if (con.State == ConnectionState.Closed)
                con.Open();
            //b2:tao doi tuong cmd 
            string sql = "Select * From Loaisach";
            SqlCommand cmd = new SqlCommand(sql, con);
            //b3: tao doi tuong Dataadapter de lay dl tu command
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            //b4:do dl tu dataadapter len datatable
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            //b5:hien thi dataadapter len datagridview
            dvgloaisach.DataSource = tb;
            dvgloaisach.Refresh();
        }


        private void btnluu_Click(object sender, EventArgs e)
        {
            //b1:lay dl tu cac dkien luu vao bien
            string p_maloai = txtma.Text.Trim();
            string p_tenloai = txtten.Text.Trim();
            string p_mota = txtmota.Text.Trim();
            //KTRA DL TRONG:
            if (p_maloai == "")
            {
                MessageBox.Show("ma k dc trong");
                txtma.Focus();
                return;
            }
            if(p_tenloai == "")
            {
                MessageBox.Show("ten k dc trong");
                txtten.Focus();
                return;
            }
            //b2:ket noi den bd
            if (con.State == ConnectionState.Closed)
                con.Open();
            //b3:tao doi tuong cmd de chen dl vao bang loai sach
            string sql = "Insert Loaisach Values('"+p_maloai+"','"+p_tenloai+"','" + p_mota + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Them moi thanh cong");
            load_dvgloaisach();


        }
    }
}
