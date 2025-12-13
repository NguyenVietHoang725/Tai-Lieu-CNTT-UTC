using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class NxbDTO
    {
        public string MaNXB { get; set; }
        public string MaQG { get; set; }
        public string TenNXB { get; set; }
        public string TenQG { get; set; } // JOIN
    }
}
