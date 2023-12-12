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
using System.Data.Common;
using System.Configuration;
using System.Diagnostics.SymbolStore;
using e_excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Load_dgv1();
        }
        private bool Checktrungma(string p_manxb)
        {
            //B1:ket noi db
            if (con.State == ConnectionState.Closed)
                con.Open();
            //B2:tao doi tuong command de thuc hien kiem tra
            string sql = "Select count(*)From Nhaxuatban Where manxb='"+p_manxb+"'";
            SqlCommand cmd=new SqlCommand(sql, con);
            int kq=(int)cmd.ExecuteScalar();
            cmd.Dispose();
            con.Close();
            if (kq > 0) return true;
            else return false;

        }
        private void Load_dgv1()
        {
            //B1: Ket noi den DB
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //b2:tao doi tuong command de lay dl tu bang nxb
            string sql = "Select * From Nhaxuatban";
            SqlCommand cmd = new SqlCommand(sql, con);
            //b3: tao doi tuong Dataadapter de lay dl tu command
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            //b4:do dl tu dataadapter len datatable
            DataTable tb =new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            //b5:hien thi dataadapter len datagridview
            dvg1.DataSource= tb;
            dvg1.Refresh();
        }
        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                //l.ay dl tu cac control dua vao bien
                //b2:ket noi den db
                //b3:tao doi tuong command de chen dl vao bang nxb va thuc thi no
                //B1:
                string p_Manxb = txtma.Text.Trim();
                string P_Tennxb = txtten.Text.Trim();
                string P_Dienthoai = txtdienthoai.Text.Trim();
                string P_Email = txtemail.Text.Trim();
                string P_Diachi = txtdiachi.Text.Trim();
                string P_Ghichu = txtghichu.Text.Trim();
                //ktra du lieu rỗng(mã,tên)
                if (p_Manxb == "")
                {
                    MessageBox.Show("Mã nxb k đc trống");
                    txtma.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(P_Tennxb))
                {
                    MessageBox.Show("Tên nxb k đc trống");
                    txtten.Focus();
                    return;
                }

                //Kiem tra trung khoa chinh
                if (Checktrungma(p_Manxb))
                {
                    MessageBox.Show("Mã nxb đã tồn tại");
                    txtma.Focus();
                    return;
                }
                //B2:Ket noi den DB
                if (con.State == ConnectionState.Closed)
                    con.Open();
                //B3:tao doi tuong command de chen dl vao va thuc thi no
                // string sql = "Insert Nhaxuatban Values('" + p_Manxb + "',N'" + P_Tennxb + "','" + P_Dienthoai + "','" + P_Email + "',N'" + P_Diachi + "',N'" + P_Ghichu + "')";
                string sql = "Insert Nhaxuatban Values(@manxb,@tennxb,@dienthoai,@email,@diachi,@ghichu)";
                SqlCommand cmd = new SqlCommand(sql, con);
                //
                cmd.Parameters.Add("@manxb", SqlDbType.NVarChar, 50).Value = p_Manxb;
                cmd.Parameters.Add("@tennxb", SqlDbType.NVarChar, 100).Value = P_Tennxb;
                cmd.Parameters.Add("@dienthoai", SqlDbType.NVarChar, 50).Value = P_Dienthoai;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = P_Email;
                cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 200).Value = P_Diachi;
                cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar, 200).Value = P_Ghichu;
                //
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                MessageBox.Show("Them moi thanh cong");
                Load_dgv1();
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống.Liên hệ với quản trị viên");
            }
        }

        private void dvg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtma.Text = dvg1.Rows[i].Cells[0].Value.ToString();
            txtten.Text = dvg1.Rows[i].Cells[1].Value.ToString();
            txtdienthoai.Text = dvg1.Rows[i].Cells[2].Value.ToString();
            txtemail.Text = dvg1.Rows[i].Cells[3].Value.ToString();
            txtdiachi.Text = dvg1.Rows[i].Cells[4].Value.ToString();
            txtghichu.Text = dvg1.Rows[i].Cells[5].Value.ToString();
            txtma.Enabled = false;
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            //b1: lay dl tu control dua vao bien

            string p_Manxb = txtma.Text.Trim();
            string p_Tennxb = txtten.Text.Trim();
            string p_Dienthoai = txtdienthoai.Text.Trim();
            string p_Email = txtemail.Text.Trim();
            string p_Diachi = txtdiachi.Text.Trim();
            string p_Ghichu = txtghichu.Text.Trim();
            //b2: ket noi den db
            if (con.State == ConnectionState.Closed)
                con.Open();
            //b3: tao doi tuong command de thuc hien sua dl
            String sql = "Update Nhaxuatban set tennxb = N'" + p_Tennxb + "',dienthoai='" + p_Dienthoai + "',Email='" + p_Email + "',diachi=N'" + p_Diachi + "',ghichu=N'" + p_Ghichu + "' Where manxb = '" + p_Manxb + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("sua thanh cong");
            Load_dgv1();
            txtma.Enabled = true;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            //b1:lay dl control dua vao bien
            string p_manxb=txtma.Text.Trim();
            //b2:ket noi db
            if(con.State == ConnectionState.Closed) con.Open();
            //b3:tao doi tuong command de thuc hien xoa dl
            string sql = "Delete Nhaxuatban Where manxb='"+p_manxb+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("xoa thanh cong");
            Load_dgv1();
            txtma.Enabled = true;

        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            //b1:lay dl tu cac control dua vao bien:
            string p_ma = txtma.Text.Trim();
            string p_ten = txtten.Text.Trim();
            string p_dienthoai = txtdienthoai.Text.Trim();
            string p_email = txtemail.Text.Trim();
            string p_diachi = txtdiachi.Text.Trim();
            //b2:ket noi den db
            if(con.State == ConnectionState.Closed) 
                con.Open();
            //b3:tao doi tuong commad de lay dl tu bang
            string sql = "Select * From Nhaxuatban Where manxb like '%" + p_ma + "%' and tennxb like '%" + p_ten + "%' and dienthoai like '%" + p_dienthoai + "%' and email like '%" + p_email + "%' and diachi like '%" + p_diachi + "%'";
            SqlCommand cmd = new SqlCommand (sql, con);
            //b4:tao doi tuong data adapter de lay kq tu command
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            //b5:do dl tu datraadapter vao datatable
            DataTable tb = new DataTable();
            //b6:hien thi datatable len dvg
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            dvg1.DataSource = tb;
            dvg1.Refresh();
            

        }
        private void ExportExcel(DataTable tb, string sheetname)
        {
            //Tạo các đối tượng Excel

            e_excel.Application oExcel = new e_excel.Application();
            e_excel.Workbooks oBooks;
            e_excel.Sheets oSheets;
            e_excel.Workbook oBook;
            e_excel.Worksheet oSheet;
            //Tạo mới một Excel WorkBook 
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (e_excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (e_excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetname;
            // Tạo phần đầu nếu muốn

            e_excel.Range head = oSheet.get_Range("A1", "E1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH LỚP HỌC";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "16";
            head.HorizontalAlignment = e_excel.XlHAlign.xlHAlignCenter;
            // Tạo tiêu đề cột 
            e_excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MÃ LỚP";
            cl1.ColumnWidth = 15;
            e_excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "TÊN LỚP";

            cl2.ColumnWidth = 25.0;
            e_excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "MÃ NGÀNH";
            cl3.ColumnWidth = 20.0;
            e_excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "SĨ SỐ";
            cl4.ColumnWidth = 15.0;
            e_excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "TÊN NGÀNH";
            cl5.ColumnWidth = 40.0;
            //Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            //cl6.Value2 = "NGÀY THI";
            //cl6.ColumnWidth = 15.0;
            //Microsoft.Office.Interop.Excel.Range cl6_1 = oSheet.get_Range("F4", "F1000");
            //cl6_1.Columns.NumberFormat = "dd/mm/yyyy";

            e_excel.Range rowHead = oSheet.get_Range("A3", "E3");
            rowHead.Font.Bold = true;
            // Kẻ viền
            rowHead.Borders.LineStyle = e_excel.Constants.xlSolid;
            // Thiết lập màu nền
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = e_excel.XlHAlign.xlHAlignCenter;
            // Tạo mảng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
            // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];
            //Chuyển dữ liệu từ DataTable vào mảng đối tượng
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                DataRow dr = tb.Rows[r];
                for (int c = 0; c < tb.Columns.Count; c++)

                {
                    arr[r, c] = dr[c];
                }
            }
            //Thiết lập vùng điền dữ liệu
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count;
            // Ô bắt đầu điền dữ liệu
            e_excel.Range c1 = (e_excel.Range)oSheet.Cells[rowStart, columnStart];
            // Ô kết thúc điền dữ liệu
            e_excel.Range c2 = (e_excel.Range)oSheet.Cells[rowEnd, columnEnd];
            // Lấy về vùng điền dữ liệu
            e_excel.Range range = oSheet.get_Range(c1, c2);
            //Điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;
            // Kẻ viền
            range.Borders.LineStyle = e_excel.Constants.xlSolid;
            // Căn giữa cột STT
            e_excel.Range c3 = (e_excel.Range)oSheet.Cells[rowEnd, columnStart];
            e_excel.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = e_excel.XlHAlign.xlHAlignCenter;
        }



        private void btnxuat_Click(object sender, EventArgs e)
        {
            //b1:lay dl tu cac control dua vao bien:
            string p_ma = txtma.Text.Trim();
            string p_ten = txtten.Text.Trim();
            string p_dienthoai = txtdienthoai.Text.Trim();
            string p_email = txtemail.Text.Trim();
            string p_diachi = txtdiachi.Text.Trim();
            //b2:ket noi den db
            if (con.State == ConnectionState.Closed)
                con.Open();
            //b3:tao doi tuong commad de lay dl tu bang
            string sql = "Select * From Nhaxuatban Where manxb like '%" + p_ma + "%' and tennxb like '%" + p_ten + "%' and dienthoai like '%" + p_dienthoai + "%' and email like '%" + p_email + "%' and diachi like '%" + p_diachi + "%'";
            SqlCommand cmd = new SqlCommand(sql, con);
            //b4:tao doi tuong data adapter de lay kq tu command
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            //b5:do dl tu datraadapter vao datatable
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            //B6:XUAT DL TU TB RA FILE EXCEL
            ExportExcel(tb, "DSNXB");
        }
    }
}
