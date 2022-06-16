using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace WinwinService.Base
{
    public class DashboardAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
