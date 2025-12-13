using LibraryManagerApp.DAL;
using System;
using System.Collections.Generic;
using static LibraryManagerApp.DAL.TrangChuDAL;

namespace LibraryManagerApp.BLL
{
    /// <summary>
    /// Business Logic Layer cho Trang chủ
    /// Xử lý validation và business rules
    /// </summary>
    internal class TrangChuBLL
    {
        #region Fields

        private readonly TrangChuDAL _dal;

        #endregion

        #region Constants

        private const int MIN_COUNT = 1;
        private const int MAX_ACTIVITIES = 100;
        private const int MAX_TOP_BOOKS = 50;

        #endregion

        #region Constructor

        public TrangChuBLL()
        {
            _dal = new TrangChuDAL();
        }

        #endregion

        #region Thống kê

        /// <summary>
        /// Lấy thống kê tổng quan của thư viện
        /// </summary>
        public Dictionary<string, int> GetStatistics()
        {
            try
            {
                return _dal.GetStatistics();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thống kê: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Kiểm tra có phiếu quá hạn hay không
        /// </summary>
        public bool HasOverdueTransactions()
        {
            try
            {
                return _dal.HasOverdueBooks();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy thống kê hoạt động hôm nay
        /// </summary>
        public TodayStatisticsDTO GetTodayStatistics()
        {
            try
            {
                return new TodayStatisticsDTO
                {
                    BorrowCount = _dal.GetTodayBorrowCount(),
                    ReturnCount = _dal.GetTodayReturnCount(),
                    TotalBorrowedCopies = _dal.GetTotalBorrowedCopies()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thống kê hôm nay: " + ex.Message, ex);
            }
        }

        #endregion

        #region Hoạt động gần đây

        /// <summary>
        /// Lấy danh sách hoạt động gần đây
        /// </summary>
        /// <param name="topCount">Số lượng giao dịch (1-100)</param>
        public List<ActivityDTO> GetRecentActivities(int topCount = 10)
        {
            ValidateCount(topCount, MAX_ACTIVITIES, "hoạt động");

            try
            {
                return _dal.GetRecentActivities(topCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy hoạt động gần đây: " + ex.Message, ex);
            }
        }

        #endregion

        #region Top sách

        /// <summary>
        /// Lấy danh sách Top sách được mượn nhiều nhất
        /// </summary>
        /// <param name="topCount">Số lượng sách (1-50)</param>
        public List<TopBookDTO> GetTopBorrowedBooks(int topCount = 10)
        {
            ValidateCount(topCount, MAX_TOP_BOOKS, "sách");

            try
            {
                return _dal.GetTopBorrowedBooks(topCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy top sách: " + ex.Message, ex);
            }
        }

        #endregion

        #region Dashboard Summary

        /// <summary>
        /// Lấy thông tin tổng hợp cho Dashboard
        /// </summary>
        public DashboardSummaryDTO GetDashboardSummary()
        {
            try
            {
                var stats = _dal.GetStatistics();
                var recentActivities = _dal.GetRecentActivities(5);
                var topBooks = _dal.GetTopBorrowedBooks(5);

                return new DashboardSummaryDTO
                {
                    TotalBooks = stats["TongTaiLieu"],
                    TotalReaders = stats["TongBanDoc"],
                    CurrentBorrowing = stats["DangMuon"],
                    OverdueCount = stats["QuaHan"],
                    RecentActivityCount = recentActivities.Count,
                    TopBookCount = topBooks.Count,
                    HasOverdue = stats["QuaHan"] > 0,
                    TodayBorrowCount = _dal.GetTodayBorrowCount(),
                    TodayReturnCount = _dal.GetTodayReturnCount()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tổng hợp dashboard: " + ex.Message, ex);
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate số lượng yêu cầu
        /// </summary>
        private void ValidateCount(int count, int maxCount, string itemName)
        {
            if (count < MIN_COUNT || count > maxCount)
            {
                throw new ArgumentException(
                    $"Số lượng {itemName} phải từ {MIN_COUNT} đến {maxCount}");
            }
        }

        #endregion
    }

    #region DTO Classes

    /// <summary>
    /// DTO tổng hợp thông tin Dashboard
    /// </summary>
    public class DashboardSummaryDTO
    {
        public int TotalBooks { get; set; }
        public int TotalReaders { get; set; }
        public int CurrentBorrowing { get; set; }
        public int OverdueCount { get; set; }
        public int RecentActivityCount { get; set; }
        public int TopBookCount { get; set; }
        public bool HasOverdue { get; set; }
        public int TodayBorrowCount { get; set; }
        public int TodayReturnCount { get; set; }
    }

    /// <summary>
    /// DTO thống kê hoạt động hôm nay
    /// </summary>
    public class TodayStatisticsDTO
    {
        public int BorrowCount { get; set; }
        public int ReturnCount { get; set; }
        public int TotalBorrowedCopies { get; set; }
    }

    #endregion
}