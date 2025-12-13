using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.BLL
{
    internal class NgonNguBLL
    {
        private NgonNguDAL _dal = new NgonNguDAL();

        public List<NgonNguDTO> LayTatCaNgonNgu()
        {
            return _dal.GetAllNgonNguDTO();
        }
    }
}
