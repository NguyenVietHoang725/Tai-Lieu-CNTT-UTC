using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bai4.Data
{
    internal class BaiTapDienTu
    {
        public string DeBai { get; set; }
        public List<string> DapAnDung { get; set; }
        public List<string> DapAnNguoiDung { get; set; }

        public BaiTapDienTu()
        {
            DapAnDung = new List<string>();
            DapAnNguoiDung = new List<string>();
        }

        public BaiTapDienTu(string deBai, List<string> dapAnDung)
        {
            DeBai = deBai;
            DapAnDung = dapAnDung;
            DapAnNguoiDung = new List<string>(new string[dapAnDung.Count]);
        }

        public int TinhDiem()
        {
            int diem = 0;
            for (int i = 0; i < DapAnDung.Count; i++)
            {
                if (i < DapAnNguoiDung.Count &&
                    string.Equals(DapAnDung[i], DapAnNguoiDung[i], StringComparison.OrdinalIgnoreCase))
                {
                    diem++;
                }
            }
            return diem;
        }
    }
}
