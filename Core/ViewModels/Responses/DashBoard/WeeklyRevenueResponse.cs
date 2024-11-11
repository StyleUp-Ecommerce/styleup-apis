using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.DashBoard
{
    public class WeeklyRevenueDataResponse
    {
        public ICollection<WeeklyRevenueDayDataResponse> DaysData { get; set; } 
        public ICollection<int> DayInt { get; set; }
    }

    public class WeeklyRevenueDayDataResponse
    {
        public string Name { get; set; }
        public ICollection<int> Data { get; set; }
        public string Color { get; set; }
    }
}
