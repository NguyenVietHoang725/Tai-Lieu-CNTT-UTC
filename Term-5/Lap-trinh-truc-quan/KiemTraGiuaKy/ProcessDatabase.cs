using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemTraGiuaKy
{
    internal class ProcessDatabase
    {
        private string strConnect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\00_UNIVERSITY\TERM_05\LapTrinhTrucQuan\KiemTraGiuaKy\Database1.mdf;Integrated Security=True";

        private SqlConnection sqlConnect = null;

        private void KetNoiCSDL()
        {
            try
            {
                sqlConnect = new SqlConnection(strConnect);
                if (sqlConnect.State != ConnectionState.Open)
                    sqlConnect.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi mở kết nối CSDL: " + ex.Message);
            }
        }

        private void DongKetNoiCSDL()
        {
            if (sqlConnect != null && sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }

        // Hàm thực thi câu lệnh dạng Select trả về một DataTable
        public DataTable DocBang(string sql)
        {
            DataTable dtBang = new DataTable();

            try
            {
                KetNoiCSDL();
                SqlDataAdapter sqldataAdapter = new SqlDataAdapter(sql, sqlConnect);
                sqldataAdapter.Fill(dtBang);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đọc bảng dữ liệu: " + ex.Message);
            }
            finally
            {
                DongKetNoiCSDL();
            }

            return dtBang;
        }

        // Hàm thực lệnh insert hoặc update hoặc delete
        public void CapNhatDuLieu(string sql)
        {
            try
            {
                KetNoiCSDL();

                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand.Connection = sqlConnect;
                sqlcommand.CommandText = sql;

                sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
            finally
            {
                DongKetNoiCSDL();
            }
        }
    }
}
