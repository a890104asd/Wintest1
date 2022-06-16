using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace WinwinService.Base
{
    public class WarehouseApiRequestLogModel
    {
        public string Method { get; set; }

        public string RequestContext { get; set; }

        public string ResponseContext { get; set; }

        public string RequestUri { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string ActionArguments { get; set; }

        public WarehouseApiRequestLogModel(ActionExecutedContext actionExecutedContext)
        {
            this.Method = actionExecutedContext.HttpContext.Request.Method.ToString();
            this.RequestUri = actionExecutedContext.HttpContext.Request.Path;
            this.ControllerName = actionExecutedContext.Controller.ToString();
            this.ActionName = actionExecutedContext.ActionDescriptor.DisplayName;
            this.ActionArguments = JsonConvert.SerializeObject(actionExecutedContext.HttpContext.Items);
            if (null != actionExecutedContext.Result)
            {
                //this.ResponseContext = JsonConvert.SerializeObject(((ObjectResult)actionExecutedContext.Result).Value);
                switch(actionExecutedContext.Result)
                {
                    case ObjectResult objectResult:
                        this.ResponseContext = JsonConvert.SerializeObject(((ObjectResult)actionExecutedContext.Result).Value);
                        break;

                    case JsonResult jsonResult:
                        this.ResponseContext = JsonConvert.SerializeObject(((JsonResult)actionExecutedContext.Result).Value);
                        break;

                    default:
                        this.ResponseContext = JsonConvert.SerializeObject(actionExecutedContext.Result);
                        break;
                }
            }

            switch(this.Method.ToLower())
            {
                case "get":
                    IDictionary<object, object> query = actionExecutedContext.HttpContext.Items;
                    if (query != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (KeyValuePair<object, object> keyValuePair in query)
                        {
                            sb.Append($"{keyValuePair.Key}={keyValuePair.Value},");
                        }
                        this.RequestContext = sb.ToString();
                    }
                    break;

                case "post":
                    try
                    {
                        if (actionExecutedContext.HttpContext.Request.Body.CanSeek)
                            actionExecutedContext.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                        using (StreamReader reader = new StreamReader(actionExecutedContext.HttpContext.Request.Body))
                        {
                            this.RequestContext = reader.ReadToEnd();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        this.RequestContext = ex.Message;
                    }
                    break;
            }
        }
    }
}
