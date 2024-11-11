using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.DashBoard
{
    public class DashBoardResponse
    {
        public decimal Revenue { get; set; }
        public decimal MonthRevenue { get; set; }
        public decimal Profit { get; set; }
        public decimal MonthProfit { get; set; }
        public int TotalProduct { get; set; }
        public int TotalTheme { get; set; }
        public WeeklyRevenueDataResponse WeeklyRevenueData { get; set; }

    }
}
