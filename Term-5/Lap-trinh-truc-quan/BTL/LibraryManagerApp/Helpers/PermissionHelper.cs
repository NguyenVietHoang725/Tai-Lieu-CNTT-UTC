// File: LibraryManagerApp.Helpers/PermissionHelper.cs

using System;
using System.Collections.Generic;

namespace LibraryManagerApp.Helpers
{
    /// <summary>
    /// Enum định nghĩa các quyền hạn trong hệ thống
    /// </summary>
    [Flags]
    public enum Permission
    {
        None = 0,
        View = 1,           // Xem/Tra cứu
        Create = 2,         // Thêm mới
        Update = 4,         // Chỉnh sửa
        Delete = 8,         // Xóa
        Export = 16,        // Xuất Excel
        Print = 32,         // In ấn
        Search = 64,        // Tìm kiếm
        FullAccess = View | Create | Update | Delete | Export | Print | Search  // Toàn quyền
    }

    /// <summary>
    /// Class quản lý phân quyền theo vai trò
    /// </summary>
    public static class PermissionHelper
    {
        // Dictionary lưu trữ quyền của từng vai trò cho từng module
        private static readonly Dictionary<string, Dictionary<string, Permission>> RolePermissions;

        static PermissionHelper()
        {
            RolePermissions = new Dictionary<string, Dictionary<string, Permission>>
            {
                // QUẢN TRỊ VIÊN - Toàn quyền tất cả
                {
                    "QTV", new Dictionary<string, Permission>
                    {
                        { "BanDoc", Permission.FullAccess },
                        { "TheBanDoc", Permission.FullAccess },
                        { "TaiLieu", Permission.FullAccess },
                        { "DanhMuc", Permission.FullAccess },
                        { "BanSao", Permission.FullAccess },
                        { "MuonTra", Permission.FullAccess },
                        { "NhanVien", Permission.FullAccess },
                        { "TaiKhoan", Permission.FullAccess },
                        { "PhanQuyen", Permission.FullAccess },
                        { "ThongKe", Permission.FullAccess }
                    }
                },

                // QUẢN LÝ BẠN ĐỌC - Full quyền Bạn đọc, chỉ xem các module khác
                {
                    "QLB", new Dictionary<string, Permission>
                    {
                        { "BanDoc", Permission.FullAccess },
                        { "TheBanDoc", Permission.FullAccess },
                        { "TaiLieu", Permission.View | Permission.Search },
                        { "DanhMuc", Permission.View | Permission.Search },
                        { "BanSao", Permission.View | Permission.Search },
                        { "MuonTra", Permission.View | Permission.Search },
                        { "NhanVien", Permission.None },
                        { "TaiKhoan", Permission.None },
                        { "PhanQuyen", Permission.None },
                        { "ThongKe", Permission.View }
                    }
                },

                // QUẢN LÝ TÀI LIỆU - Full quyền Tài liệu, chỉ xem các module khác
                {
                    "QLT", new Dictionary<string, Permission>
                    {
                        { "BanDoc", Permission.View | Permission.Search },
                        { "TheBanDoc", Permission.View | Permission.Search },
                        { "TaiLieu", Permission.FullAccess },
                        { "DanhMuc", Permission.FullAccess },
                        { "BanSao", Permission.FullAccess },
                        { "MuonTra", Permission.View | Permission.Search },
                        { "NhanVien", Permission.None },
                        { "TaiKhoan", Permission.None },
                        { "PhanQuyen", Permission.None },
                        { "ThongKe", Permission.View }
                    }
                },

                // QUẢN LÝ MƯỢN TRẢ - Full quyền Mượn trả, chỉ xem các module khác
                {
                    "QLM", new Dictionary<string, Permission>
                    {
                        { "BanDoc", Permission.View | Permission.Search },
                        { "TheBanDoc", Permission.View | Permission.Search },
                        { "TaiLieu", Permission.View | Permission.Search },
                        { "DanhMuc", Permission.View | Permission.Search },
                        { "BanSao", Permission.View | Permission.Search },
                        { "MuonTra", Permission.FullAccess },
                        { "NhanVien", Permission.None },
                        { "TaiKhoan", Permission.None },
                        { "PhanQuyen", Permission.None },
                        { "ThongKe", Permission.View }
                    }
                }
            };
        }

        /// <summary>
        /// Kiểm tra vai trò có quyền cụ thể cho module hay không
        /// </summary>
        public static bool HasPermission(string maVaiTro, string moduleName, Permission requiredPermission)
        {
            if (string.IsNullOrEmpty(maVaiTro) || string.IsNullOrEmpty(moduleName))
                return false;

            // QTV có toàn quyền
            if (maVaiTro.ToUpper() == "QTV")
                return true;

            maVaiTro = maVaiTro.ToUpper();

            if (!RolePermissions.ContainsKey(maVaiTro))
                return false;

            var modulePermissions = RolePermissions[maVaiTro];

            if (!modulePermissions.ContainsKey(moduleName))
                return false;

            var userPermission = modulePermissions[moduleName];

            // Kiểm tra có quyền yêu cầu không (sử dụng bitwise AND)
            return (userPermission & requiredPermission) == requiredPermission;
        }

        /// <summary>
        /// Lấy tất cả quyền của vai trò cho module
        /// </summary>
        public static Permission GetPermissions(string maVaiTro, string moduleName)
        {
            if (string.IsNullOrEmpty(maVaiTro) || string.IsNullOrEmpty(moduleName))
                return Permission.None;

            // QTV có toàn quyền
            if (maVaiTro.ToUpper() == "QTV")
                return Permission.FullAccess;

            maVaiTro = maVaiTro.ToUpper();

            if (!RolePermissions.ContainsKey(maVaiTro))
                return Permission.None;

            var modulePermissions = RolePermissions[maVaiTro];

            return modulePermissions.ContainsKey(moduleName)
                ? modulePermissions[moduleName]
                : Permission.None;
        }

        /// <summary>
        /// Kiểm tra có quyền View (Xem)
        /// </summary>
        public static bool CanView(string maVaiTro, string moduleName)
        {
            return HasPermission(maVaiTro, moduleName, Permission.View);
        }

        /// <summary>
        /// Kiểm tra có quyền Create (Thêm)
        /// </summary>
        public static bool CanCreate(string maVaiTro, string moduleName)
        {
            return HasPermission(maVaiTro, moduleName, Permission.Create);
        }

        /// <summary>
        /// Kiểm tra có quyền Update (Sửa)
        /// </summary>
        public static bool CanUpdate(string maVaiTro, string moduleName)
        {
            return HasPermission(maVaiTro, moduleName, Permission.Update);
        }

        /// <summary>
        /// Kiểm tra có quyền Delete (Xóa)
        /// </summary>
        public static bool CanDelete(string maVaiTro, string moduleName)
        {
            return HasPermission(maVaiTro, moduleName, Permission.Delete);
        }

        /// <summary>
        /// Kiểm tra có quyền Export (Xuất Excel)
        /// </summary>
        public static bool CanExport(string maVaiTro, string moduleName)
        {
            return HasPermission(maVaiTro, moduleName, Permission.Export);
        }

        /// <summary>
        /// Kiểm tra có quyền Print (In)
        /// </summary>
        public static bool CanPrint(string maVaiTro, string moduleName)
        {
            return HasPermission(maVaiTro, moduleName, Permission.Print);
        }

        /// <summary>
        /// Kiểm tra có quyền Search (Tìm kiếm)
        /// </summary>
        public static bool CanSearch(string maVaiTro, string moduleName)
        {
            return HasPermission(maVaiTro, moduleName, Permission.Search);
        }

        /// <summary>
        /// Hiển thị thông báo không có quyền
        /// </summary>
        public static void ShowPermissionDeniedMessage(string action = "thực hiện thao tác này")
        {
            System.Windows.Forms.MessageBox.Show(
                $"Bạn không có quyền {action}.",
                "Không có quyền truy cập",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Warning
            );
        }
    }
}