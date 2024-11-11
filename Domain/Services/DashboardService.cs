using CleanBase.Core.Data.UnitOfWorks;
using Core.Constants;
using Core.Data.Repositories;
using Core.Services;
using Core.ViewModels.Responses.DashBoard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardService(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }

        public async Task<DashBoardResponse> GetHomeDashBoardData()
        {
            List<string> monthAbbreviations = new List<string>();

            DateTime currentMonth = DateTime.Now;

            for (int i = 0; i < 6; i++)
            {
                DateTime month = currentMonth.AddMonths(i);
                string monthAbbreviation = month.ToString("MMM");
                monthAbbreviations.Add(monthAbbreviation);
            }

            decimal totalRevenue = CalculatorTotalOfRevenue();
            decimal totalMonthRevenue = CalculatorTotalOfMonthRevenue(DateTime.Now.Month);
            var totalProductTheme = CalculateTotalProductCustomCanvas();

            return new DashBoardResponse
            {
                Revenue = totalRevenue,
                MonthRevenue = totalMonthRevenue,
                Profit = totalRevenue * (35 / 100),
                MonthProfit = totalMonthRevenue * (25 / 100),
                TotalProduct = totalProductTheme.totalProduct,
                TotalTheme = totalProductTheme.totalTheme,
                WeeklyRevenueData = null
            };
        }
        public decimal CalculatorTotalOfMonthRevenue(int month)
        {
            var orders = this._unitOfWork.Repository<IOrderRepository>()
                .Where(p => p.CreatedDate.Month == month && p.StatusString == nameof(StatusEnum.Delivered))
                .Include(p => p.OrderItems)
                .ToList(); 

            return orders.Sum(order => order.OrderItems.Sum(oi => (decimal)(oi.Price) * oi.Quantity)); 
        }

        public decimal CalculatorTotalOfRevenue()
        {
            var orders = this._unitOfWork.Repository<IOrderRepository>()
                .Where(p => p.StatusString == nameof(StatusEnum.Delivered))
                .Include(p => p.OrderItems)
                .ToList(); 

            return orders.Sum(order => order.OrderItems.Sum(oi => (decimal)(oi.Price) * oi.Quantity));
        }


        public (int totalProduct, int totalTheme) CalculateTotalProductCustomCanvas()
        {
            var data = this._unitOfWork.Repository<ITemplateCanvasRepository>()
            .Where(p => true)  
            .Select(p => new
            {
                eachTotalTheme = p.CustomCanvas.Count(c => c.IsPublic), 
                eachTotalProduct = p.CustomCanvas.Count()               
            }).ToList();

            var totalData = new
            {
                totalTheme = data.Sum(p => p.eachTotalTheme),
                totalProduct = data.Sum(p => p.eachTotalProduct)
            }; 

            return (totalData.totalProduct, totalData.totalTheme);
        }
    }
}
