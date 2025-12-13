// File: LibraryManagerApp.Helpers/SessionManager.cs

using LibraryManagerApp.DTO;
using System;

namespace LibraryManagerApp.Helpers
{
    public static class SessionManager
    {
        #region Properties

        // Thuộc tính lưu trữ thông tin người dùng đã đăng nhập
        public static LoginSessionDTO CurrentUser { get; private set; }

        // Vai trò hiện tại (lấy từ CurrentUser.MaVT)
        public static string CurrentRole
        {
            get { return CurrentUser?.MaVT; }
        }

        // Kiểm tra trạng thái đăng nhập
        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        #endregion

        #region Login/Logout Methods

        /// <summary>
        /// Hàm thiết lập thông tin người dùng sau khi đăng nhập thành công
        /// </summary>
        public static void Login(LoginSessionDTO userSession)
        {
            if (userSession == null)
            {
                throw new ArgumentNullException(nameof(userSession), "Phiên người dùng không được rỗng.");
            }

            CurrentUser = userSession;

            // Log thông tin đăng nhập (tùy chọn - có thể bỏ trong production)
            System.Diagnostics.Debug.WriteLine($"[SessionManager] User logged in: {userSession.HoTenNV} - Role: {userSession.MaVT}");
        }

        /// <summary>
        /// Hàm xóa phiên làm việc khi người dùng Đăng xuất
        /// </summary>
        public static void Logout()
        {
            if (CurrentUser != null)
            {
                System.Diagnostics.Debug.WriteLine($"[SessionManager] User logged out: {CurrentUser.HoTenNV}");
            }

            CurrentUser = null;
        }

        #endregion

        #region Basic Info Methods

        /// <summary>
        /// Hàm lấy Mã Vai trò để kiểm tra phân quyền
        /// </summary>
        public static string GetMaVaiTro()
        {
            return CurrentUser?.MaVT;
        }

        /// <summary>
        /// Hàm lấy Mã Tài khoản (rất quan trọng cho các thao tác CRUD)
        /// </summary>
        public static string GetMaTaiKhoan()
        {
            return CurrentUser?.MaTK;
        }

        /// <summary>
        /// Lấy họ tên nhân viên hiện tại
        /// </summary>
        public static string GetHoTenNhanVien()
        {
            return CurrentUser?.HoTenNV;
        }

        /// <summary>
        /// Lấy mã nhân viên hiện tại
        /// </summary>
        public static string GetMaNhanVien()
        {
            return CurrentUser?.MaNV;
        }

        #endregion

        #region Permission Check Methods

        /// <summary>
        /// Kiểm tra quyền cho module hiện tại
        /// </summary>
        public static bool HasPermission(string moduleName, Permission permission)
        {
            if (!IsLoggedIn || string.IsNullOrEmpty(CurrentRole))
            {
                System.Diagnostics.Debug.WriteLine($"[SessionManager] Permission denied: Not logged in or no role");
                return false;
            }

            bool hasPermission = PermissionHelper.HasPermission(CurrentRole, moduleName, permission);

            // Log kiểm tra quyền (tùy chọn - có thể bỏ trong production)
            System.Diagnostics.Debug.WriteLine(
                $"[SessionManager] Check permission - Role: {CurrentRole}, Module: {moduleName}, Permission: {permission}, Result: {hasPermission}"
            );

            return hasPermission;
        }

        /// <summary>
        /// Kiểm tra có phải QTV không
        /// </summary>
        public static bool IsAdmin()
        {
            return IsLoggedIn && CurrentRole?.ToUpper() == "QTV";
        }

        /// <summary>
        /// Kiểm tra có quyền xem không
        /// </summary>
        public static bool CanView(string moduleName)
        {
            return HasPermission(moduleName, Permission.View);
        }

        /// <summary>
        /// Kiểm tra có quyền thêm không
        /// </summary>
        public static bool CanCreate(string moduleName)
        {
            return HasPermission(moduleName, Permission.Create);
        }

        /// <summary>
        /// Kiểm tra có quyền sửa không
        /// </summary>
        public static bool CanUpdate(string moduleName)
        {
            return HasPermission(moduleName, Permission.Update);
        }

        /// <summary>
        /// Kiểm tra có quyền xóa không
        /// </summary>
        public static bool CanDelete(string moduleName)
        {
            return HasPermission(moduleName, Permission.Delete);
        }

        /// <summary>
        /// Kiểm tra có quyền xuất Excel không
        /// </summary>
        public static bool CanExport(string moduleName)
        {
            return HasPermission(moduleName, Permission.Export);
        }

        /// <summary>
        /// Kiểm tra có quyền in ấn không
        /// </summary>
        public static bool CanPrint(string moduleName)
        {
            return HasPermission(moduleName, Permission.Print);
        }

        /// <summary>
        /// Kiểm tra có quyền tìm kiếm không
        /// </summary>
        public static bool CanSearch(string moduleName)
        {
            return HasPermission(moduleName, Permission.Search);
        }

        #endregion

        #region Role-Based Access Check

        /// <summary>
        /// Kiểm tra vai trò cụ thể
        /// </summary>
        public static bool HasRole(string roleCode)
        {
            if (!IsLoggedIn || string.IsNullOrEmpty(roleCode))
                return false;

            return CurrentRole?.ToUpper() == roleCode.ToUpper();
        }

        /// <summary>
        /// Kiểm tra có phải Quản lý Bạn đọc không
        /// </summary>
        public static bool IsQLB()
        {
            return HasRole("QLB");
        }

        /// <summary>
        /// Kiểm tra có phải Quản lý Tài liệu không
        /// </summary>
        public static bool IsQLT()
        {
            return HasRole("QLT");
        }

        /// <summary>
        /// Kiểm tra có phải Quản lý Mượn trả không
        /// </summary>
        public static bool IsQLM()
        {
            return HasRole("QLM");
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Kiểm tra và yêu cầu đăng nhập
        /// </summary>
        public static bool RequireLogin()
        {
            if (!IsLoggedIn)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Vui lòng đăng nhập để tiếp tục.",
                    "Yêu cầu đăng nhập",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra và yêu cầu quyền cụ thể
        /// </summary>
        public static bool RequirePermission(string moduleName, Permission permission, string actionName = "thực hiện thao tác này")
        {
            if (!IsLoggedIn)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Vui lòng đăng nhập để tiếp tục.",
                    "Yêu cầu đăng nhập",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
                return false;
            }

            if (!HasPermission(moduleName, permission))
            {
                PermissionHelper.ShowPermissionDeniedMessage(actionName);
                return false;
            }

            return true;
        }

        #endregion

        #region Debug/Info Methods

        /// <summary>
        /// Lấy thông tin phiên hiện tại (cho mục đích debug)
        /// </summary>
        public static string GetSessionInfo()
        {
            if (!IsLoggedIn)
                return "Chưa đăng nhập";

            return $"User: {CurrentUser.HoTenNV} ({CurrentUser.MaTK}) - Role: {CurrentRole} ({GetRoleName()})";
        }

        /// <summary>
        /// Lấy tên vai trò đầy đủ
        /// </summary>
        public static string GetRoleName()
        {
            if (!IsLoggedIn || string.IsNullOrEmpty(CurrentRole))
                return "Chưa xác định";

            switch (CurrentRole.ToUpper())
            {
                case "QTV":
                    return "Quản trị viên";
                case "QLB":
                    return "Quản lý bạn đọc";
                case "QLT":
                    return "Quản lý tài liệu";
                case "QLM":
                    return "Quản lý mượn trả";
                default:
                    return "Nhân viên";
            }
        }

        #endregion
    }
}