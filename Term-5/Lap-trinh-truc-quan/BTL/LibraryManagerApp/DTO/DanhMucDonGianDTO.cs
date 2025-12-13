using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class DanhMucDonGianDTO
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string HienThi { get { return $"{Ma} - {Ten}"; } }
    }
}
