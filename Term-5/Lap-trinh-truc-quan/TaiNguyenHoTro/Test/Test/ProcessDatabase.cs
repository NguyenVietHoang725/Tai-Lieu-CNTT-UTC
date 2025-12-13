using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class ProcessDataBase
    {
        // 🔑 KHAI BÁO CONNECTION STRING
        private string strConnect = @"Data Source=HOANGNGUYEN\SQLEXPRESS;Initial Catalog=QLThuVien;Integrated Security=True";

        private SqlConnection sqlConnect = null;

        // --- HÀM QUẢN LÝ KẾT NỐI ---
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

        // --- HÀM THỰC THI TRUY VẤN SELECT ---

        // Hàm thực thi câu lệnh dạng Select trả về một DataTable
        public DataTable DocBang(string sql)
        {
            DataTable dtBang = new DataTable();

            try
            {
                KetNoiCSDL();
                // SqlDataAdapter tự động mở/đóng kết nối nếu nó tự tạo, nhưng với cách viết này
                // ta cần mở kết nối thủ công trước
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

        // --- HÀM THỰC THI TRUY VẤN INSERT/UPDATE/DELETE ---

        // Hàm thực lệnh insert hoặc update hoặc delete
        public void CapNhatDuLieu(string sql)
        {
            try
            {
                KetNoiCSDL();

                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand.Connection = sqlConnect;
                sqlcommand.CommandText = sql;

                // ExecuteNonQuery trả về số dòng bị ảnh hưởng, nên bạn có thể hứng giá trị này nếu cần
                sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Ném ngoại lệ để lớp gọi có thể xử lý và thông báo cho người dùng
                throw new Exception("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
            finally
            {
                DongKetNoiCSDL();
            }
        }
    }
}