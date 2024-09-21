using Hangfire.Dashboard;

namespace Infrastructure.Jobs
{
    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext.Request.Query.ContainsKey("token"))
            {
                var token = httpContext.Request.Query["token"].ToString();
                if (!string.IsNullOrEmpty(token))
                {
                    return true;
                }
            }
            return true;
        }
    }
}
