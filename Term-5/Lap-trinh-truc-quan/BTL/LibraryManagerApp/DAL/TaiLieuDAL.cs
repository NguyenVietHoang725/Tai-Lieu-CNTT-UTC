using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class TaiLieuDAL
    {
        // 1. READ chính: Lấy danh sách Tài liệu
        public List<TaiLieuDTO> GetAllTaiLieuDTO()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from tl in db.tTaiLieus
                            join nxb in db.tNhaXuatBans on tl.MaNXB equals nxb.MaNXB
                            join nn in db.tNgonNgus on tl.MaNN equals nn.MaNN
                            join thl in db.tTheLoais on tl.MaThL equals thl.MaThL
                            join dd in db.tDinhDangs on tl.MaDD equals dd.MaDD
                            select new TaiLieuDTO
                            {
                                MaTL = tl.MaTL,
                                TenTL = tl.TenTL,
                                MaNXB = tl.MaNXB,
                                MaNN = tl.MaNN,
                                MaThL = tl.MaThL,
                                MaDD = tl.MaDD,
                                MaTK = tl.MaTK,

                                // Thông tin JOIN
                                TenNXB = nxb.TenNXB,
                                TenNN = nn.TenNN,
                                TenThL = thl.TenThL,
                                TenDD = dd.TenDD,

                                LanXuatBan = tl.LanXuatBan,
                                NamXuatBan = tl.NamXuatBan,
                                SoTrang = tl.SoTrang,
                                KhoCo = tl.KhoCo.Trim(), // Nhớ Trim() cho khổ cỡ
                                Anh = tl.Anh
                            };

                return query.ToList();
            }
        }

        // 2. READ Chi tiết: Lấy thông tin Tài liệu chính và Tác giả đính kèm
        public TaiLieuDTO GetTaiLieuByMaTL(string maTL)
        {
            using (var db = new QLThuVienDataContext())
            {
                // a) Lấy thông tin Tài liệu chính
                TaiLieuDTO taiLieu = (from tl in db.tTaiLieus
                                      join nxb in db.tNhaXuatBans on tl.MaNXB equals nxb.MaNXB
                                      join nn in db.tNgonNgus on tl.MaNN equals nn.MaNN
                                      join thl in db.tTheLoais on tl.MaThL equals thl.MaThL
                                      join dd in db.tDinhDangs on tl.MaDD equals dd.MaDD
                                      where tl.MaTL == maTL
                                      select new TaiLieuDTO
                                      {
                                          MaTL = tl.MaTL,
                                          TenTL = tl.TenTL,
                                          MaNXB = tl.MaNXB,
                                          TenNXB = nxb.TenNXB,
                                          MaNN = tl.MaNN,
                                          TenNN = nn.TenNN,
                                          MaThL = tl.MaThL,
                                          TenThL = thl.TenThL,
                                          MaDD = tl.MaDD,
                                          TenDD = dd.TenDD,
                                          MaTK = tl.MaTK,

                                          LanXuatBan = tl.LanXuatBan,
                                          NamXuatBan = tl.NamXuatBan,
                                          SoTrang = tl.SoTrang,
                                          KhoCo = tl.KhoCo.Trim(),
                                          Anh = tl.Anh
                                      }).SingleOrDefault();

                if (taiLieu == null) return null;

                // b) Lấy danh sách Tác giả đính kèm
                taiLieu.DanhSachTacGia = (from tltg in db.tTaiLieu_TacGias
                                          join tg in db.tTacGias on tltg.MaTG equals tg.MaTG
                                          where tltg.MaTL == maTL
                                          select new TL_TGDTO
                                          {
                                              MaTL = tltg.MaTL,
                                              MaTG = tltg.MaTG,
                                              VaiTro = tltg.VaiTro,
                                              HoTenTG = tg.HoDem + " " + tg.Ten
                                          }).ToList();

                return taiLieu;
            }
        }

        // 3. Hàm hỗ trợ tải danh sách Tác giả (cho ComboBox)
        // Dùng lại hàm GetTacGiaByMaTG đã có từ TacGiaDAL (cần truy cập qua BLL)
        // Hoặc tạo một DTO/DAL mới để lấy danh sách MaTG - HoTen
        public List<TacGiaDTO> GetAllTacGiaForCombo()
        {
            // Dùng TacGiaDAL đã có để lấy toàn bộ tác giả
            TacGiaDAL tgDal = new TacGiaDAL();
            return tgDal.GetAllTacGiaDTO();
        }

        // 1. Hàm gọi Stored Procedure SP_GenerateNewMaTl
        public string GenerateNewMaTL(string maNXB)
        {
            // MaTL (TL[MaQG][YY]-[###]) phụ thuộc vào MaQG của NXB
            if (string.IsNullOrEmpty(maNXB) || maNXB.Length < 5)
            {
                throw new ArgumentException("Mã NXB không hợp lệ để sinh Mã Tài liệu.");
            }

            // Format MaNXB: NXB[QG]-[###] (VD: NXBVN-001)
            // Lấy MaQG (2 ký tự) từ MaNXB
            string maQG = maNXB.Substring(3, 2);

            using (var db = new QLThuVienDataContext())
            {
                string newMaTL = string.Empty;

                try
                {
                    // Giả định LINQ to SQL đã ánh xạ SP: SP_GenerateNewMaTL
                    db.SP_GenerateNewMaTl(maQG, ref newMaTL);

                    return newMaTL;
                }
                catch (Exception ex)
                {
                    // Bắt lỗi RAISERROR từ SP (ví dụ: Vượt quá giới hạn mã)
                    throw new Exception("Lỗi SP khi sinh mã Tài liệu: " + ex.Message);
                }
            }
        }

        // 2. Hàm Insert (CREATE)
        public bool InsertTaiLieu(TaiLieuDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tTaiLieu newTL = new tTaiLieu
                {
                    MaTL = model.MaTL, // Mã đã được sinh
                    MaNXB = model.MaNXB,
                    MaNN = model.MaNN,
                    MaThL = model.MaThL,
                    MaDD = model.MaDD,
                    MaTK = model.MaTK, // Lấy từ Session
                    TenTL = model.TenTL,
                    LanXuatBan = model.LanXuatBan,
                    NamXuatBan = model.NamXuatBan,
                    SoTrang = model.SoTrang,
                    KhoCo = model.KhoCo,
                    Anh = model.Anh
                };

                db.tTaiLieus.InsertOnSubmit(newTL);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thêm Tài liệu: " + ex.Message);
                    return false;
                }
            }
        }

        // 3. Hàm Insert Tác giả đính kèm (cho Bảng phụ)
        public bool InsertTL_TG(TL_TGDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tTaiLieu_TacGia newEntry = new tTaiLieu_TacGia
                {
                    MaTL = model.MaTL,
                    MaTG = model.MaTG,
                    VaiTro = model.VaiTro
                };

                db.tTaiLieu_TacGias.InsertOnSubmit(newEntry);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thêm Tác giả vào Tài liệu: " + ex.Message);
                    return false;
                }
            }
        }

        // 4. Hàm Delete (DELETE Tài liệu chính)
        public bool DeleteTaiLieu(string maTL)
        {
            using (var db = new QLThuVienDataContext())
            {
                tTaiLieu tlToDelete = db.tTaiLieus.SingleOrDefault(tl => tl.MaTL == maTL);

                if (tlToDelete != null)
                {
                    db.tTaiLieus.DeleteOnSubmit(tlToDelete);

                    // Lưu ý: tTaiLieu_TacGia sẽ tự động bị xóa theo (ON DELETE CASCADE)
                    // (Nếu bạn không cài CASCADE, bạn phải xóa tTaiLieu_TacGia trước)

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Lỗi ràng buộc (ví dụ: Tài liệu đang có Bản sao/Phiếu mượn)
                        Console.WriteLine("Lỗi khi xóa Tài liệu: " + ex.Message);
                        return false;
                    }
                }
                return false; // Không tìm thấy Mã TL
            }
        }

        // 5. Hàm Update (UPDATE Tài liệu chính)
        public bool UpdateTaiLieu(TaiLieuDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tTaiLieu existingTL = db.tTaiLieus.SingleOrDefault(tl => tl.MaTL == model.MaTL);

                if (existingTL != null)
                {
                    // Cập nhật các trường
                    existingTL.TenTL = model.TenTL;
                    existingTL.MaNXB = model.MaNXB;
                    existingTL.MaNN = model.MaNN;
                    existingTL.MaThL = model.MaThL;
                    existingTL.MaDD = model.MaDD;
                    existingTL.MaTK = model.MaTK; // Cập nhật người sửa (nếu cần)
                    existingTL.LanXuatBan = model.LanXuatBan;
                    existingTL.NamXuatBan = model.NamXuatBan;
                    existingTL.SoTrang = model.SoTrang;
                    existingTL.KhoCo = model.KhoCo;
                    existingTL.Anh = model.Anh;

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi cập nhật Tài liệu: " + ex.Message);
                        return false;
                    }
                }
                return false;
            }
        }

        // 6. Hàm Xóa Tác giả đính kèm (Để đồng bộ)
        public bool DeleteAllTacGiaByMaTL(string maTL)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Tìm tất cả các bản ghi tTaiLieu_TacGia của tài liệu này
                var tacGiaList = db.tTaiLieu_TacGias.Where(tltg => tltg.MaTL == maTL);

                if (tacGiaList.Any())
                {
                    db.tTaiLieu_TacGias.DeleteAllOnSubmit(tacGiaList);
                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi xóa Tác giả đính kèm cũ: " + ex.Message);
                        return false;
                    }
                }
                return true; // Không có gì để xóa, vẫn tính là thành công
            }
        }

        // 7. Hàm Cập nhật Đường dẫn Ảnh
        // (Sử dụng cho Thêm/Thay đổi/Xóa ảnh)
        public bool UpdateImagePath(string maTL, string imagePath)
        {
            using (var db = new QLThuVienDataContext())
            {
                tTaiLieu existingTL = db.tTaiLieus.SingleOrDefault(tl => tl.MaTL == maTL);
                if (existingTL != null)
                {
                    // Gán đường dẫn mới (hoặc null nếu Xóa)
                    existingTL.Anh = imagePath;

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi cập nhật đường dẫn ảnh: " + ex.Message);
                        return false;
                    }
                }
                return false;
            }
        }
        // 8. Hàm Tìm kiếm Tài Liệu
        public List<TaiLieuDTO> SearchTaiLieu(List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext())
            {
                IQueryable<tTaiLieu> query = db.tTaiLieus.AsQueryable();

                foreach (var filter in filters)
                {
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;
                    string valueTo = filter.ValueTo;

                    // 1. Xử lý các trường kiểu chuỗi
                    if (fieldName == "MaTL" || fieldName == "MaNXB" || fieldName == "MaNN" ||
                        fieldName == "MaThL" || fieldName == "MaDD")
                    {
                        if (op == "=") query = query.Where(tl =>
                            (fieldName == "MaTL" ? tl.MaTL : fieldName == "MaNXB" ? tl.MaNXB :
                            fieldName == "MaNN" ? tl.MaNN : fieldName == "MaThL" ? tl.MaThL : tl.MaDD) == value);
                        else if (op == "LIKE" && fieldName != "MaNN" && fieldName != "MaThL" && fieldName != "MaDD")
                            query = query.Where(tl =>
                            (fieldName == "MaTL" ? tl.MaTL : tl.MaNXB).Contains(value));
                        else if (op == "Bắt đầu bằng" && fieldName != "MaNN" && fieldName != "MaThL" && fieldName != "MaDD")
                            query = query.Where(tl =>
                            (fieldName == "MaTL" ? tl.MaTL : tl.MaNXB).StartsWith(value));
                    }
                    else if (fieldName == "TenTL")
                    {
                        if (op == "LIKE") query = query.Where(tl => tl.TenTL.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(tl => tl.TenTL.StartsWith(value));
                    }

                    // 2. Xử lý các trường số nguyên (Năm Xuất Bản, Số Trang)
                    else if (fieldName == "NamXuatBan" || fieldName == "SoTrang")
                    {
                        if (int.TryParse(value, out int intValue))
                        {
                            var propValue = fieldName == "NamXuatBan" ? (int?)intValue : (int?)intValue;

                            if (op == "=")
                                query = query.Where(tl => fieldName == "NamXuatBan" ? tl.NamXuatBan == intValue : tl.SoTrang == intValue);
                            else if (op == ">")
                                query = query.Where(tl => fieldName == "NamXuatBan" ? tl.NamXuatBan > intValue : tl.SoTrang > intValue);
                            else if (op == "<")
                                query = query.Where(tl => fieldName == "NamXuatBan" ? tl.NamXuatBan < intValue : tl.SoTrang < intValue);
                            else if (op == ">=")
                                query = query.Where(tl => fieldName == "NamXuatBan" ? tl.NamXuatBan >= intValue : tl.SoTrang >= intValue);
                            else if (op == "<=")
                                query = query.Where(tl => fieldName == "NamXuatBan" ? tl.NamXuatBan <= intValue : tl.SoTrang <= intValue);
                            else if (int.TryParse(valueTo, out int intEnd))
                            {
                                if (op == "Khoảng")
                                    query = query.Where(tl => fieldName == "NamXuatBan" ? (tl.NamXuatBan > intValue && tl.NamXuatBan < intEnd) : (tl.SoTrang > intValue && tl.SoTrang < intEnd));
                                else if (op == "Đoạn")
                                    query = query.Where(tl => fieldName == "NamXuatBan" ? (tl.NamXuatBan >= intValue && tl.NamXuatBan <= intEnd) : (tl.SoTrang >= intValue && tl.SoTrang <= intEnd));
                            }
                        }
                    }
                }

                // 3. Thực hiện JOIN và MAP ra DTO đầy đủ (Giả sử bạn cần JOIN)
                var finalQuery = from tl in query
                                 join nxb in db.tNhaXuatBans on tl.MaNXB equals nxb.MaNXB
                                 join nn in db.tNgonNgus on tl.MaNN equals nn.MaNN
                                 join thl in db.tTheLoais on tl.MaThL equals thl.MaThL
                                 join dd in db.tDinhDangs on tl.MaDD equals dd.MaDD
                                 select new TaiLieuDTO
                                 {
                                     MaTL = tl.MaTL,
                                     TenTL = tl.TenTL,
                                     LanXuatBan = tl.LanXuatBan,
                                     NamXuatBan = tl.NamXuatBan,
                                     SoTrang = tl.SoTrang,
                                     KhoCo = tl.KhoCo,
                                     Anh = tl.Anh,

                                     // Trường khóa ngoại
                                     MaNXB = tl.MaNXB,
                                     MaNN = tl.MaNN,
                                     MaThL = tl.MaThL,
                                     MaDD = tl.MaDD,

                                     // Thông tin JOIN
                                     TenNXB = nxb.TenNXB,
                                     TenNN = nn.TenNN,
                                     TenThL = thl.TenThL,
                                     TenDD = dd.TenDD
                                     // ... và các trường khác (MaTK)
                                 };

                return finalQuery.ToList();
            }
        }

        // [MỚI] Hàm lấy tất cả liên kết Tài liệu - Tác giả để phục vụ xuất Excel
        // Trả về danh sách phẳng: MaTL, HoTenTG, VaiTro
        public List<TL_TGDTO> GetAllAuthorLinks()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from tltg in db.tTaiLieu_TacGias
                            join tg in db.tTacGias on tltg.MaTG equals tg.MaTG
                            select new TL_TGDTO
                            {
                                MaTL = tltg.MaTL,
                                MaTG = tltg.MaTG,
                                VaiTro = tltg.VaiTro,
                                HoTenTG = tg.HoDem + " " + tg.Ten
                            };
                return query.ToList();
            }
        }

        // Lấy tổng số tài liệu
        public int GetTotalTaiLieu()
        {
            using (var db = new QLThuVienDataContext())
            {
                return db.tTaiLieus.Count();
            }
        }
    }
}
