using log4net.Config;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net.Http;

namespace WinwinService.Base
{
    public class ApiRequestLogAttribute : ActionFilterAttribute
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static ApiRequestLogAttribute()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("./log4net.config"));
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            WarehouseApiRequestLogModel logModel = new WarehouseApiRequestLogModel(actionExecutedContext);
            log.Info(JsonConvert.SerializeObject(logModel));
        }

    }
}
