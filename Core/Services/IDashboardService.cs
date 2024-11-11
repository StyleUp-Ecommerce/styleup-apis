
using Core.ViewModels.Responses.DashBoard;

namespace Core.Services
{
    public interface IDashboardService
    {
        Task<DashBoardResponse> GetHomeDashBoardData();
    }
}
