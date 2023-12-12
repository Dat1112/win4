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
    public partial class Form3 : Form
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        public Form3()
        {
            InitializeComponent();
        }
        private void Load_dvg1()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "Select * From Sanpham";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            dvg1.DataSource = tb;
            dvg1.Refresh();
        }

        private void Form3_Load(object sender, EventArgs e)
        {   
            
            Load_dvg1();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string p_ma = txtma.Text.Trim();
            string p_ten = txtten.Text.Trim();
            string p_gia = txtgia.Text.Trim();
            string p_soluong = txtsoluong.Text.Trim();
            string p_mancc = comboBox1.SelectedItem.ToString();
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sql = "Insert into Sanpham (masp,tensp,gia,soluong,mancc) Values ('" + p_ma + "','" + p_ten + "','" + p_gia + "','" + p_soluong + "','"+p_mancc+"')";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Them moi thanh cong");
            Load_dvg1();



        }

        private void dvg1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtma.Text = dvg1.Rows[i].Cells[0].Value.ToString();
            txtten.Text = dvg1.Rows[i].Cells[1].Value.ToString();
            txtgia.Text = dvg1.Rows[i].Cells[2].Value.ToString();
            txtsoluong.Text = dvg1.Rows[i].Cells[3].Value.ToString();
            comboBox1.SelectedItem = dvg1.Rows[i].Cells[4].Value.ToString();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string p_ma = txtma.Text.Trim();
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sql = "Delete From Sanpham Where masp = '"+p_ma+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Xoa thanh cong");
            Load_dvg1();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
