using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinwinService.Models;


namespace WinwinService.Base
{
    public class FromToolAuthentication : WinwinApiBase
    {
        private readonly RequestDelegate _next;
        private readonly string ToolToken = "XzWUht3RZPWG8HK4dxrPNmv9EFxNF6Qf";
        public FromToolAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
                if (await TokenAuthentication(context))
                {
                    await _next(context);
                }
        }

        private async Task<bool> TokenAuthentication(HttpContext context)
        {
            bool IsTokenSuccess = true;
            ApiResponse<object> apiResponse = new ApiResponse<object>() { };

            string DestinationApi = context.Request.Path.Value;

            try
            {
                string token = context.Request.Headers["tool-token"];
                if (!string.IsNullOrWhiteSpace(token))
                {
                    if (!token.Equals(ToolToken))
                    {
                        apiResponse.Status = (int)EnumMasterErrorCode.Invalid_Token;
                        apiResponse.Error = new List<Errors>(){ GetErrors("tool-token", EnumMasterErrorCode.Invalid_Token, EnumSeqMessage.Is_Invalid) };
                        byte[] result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(apiResponse));
                        context.Response.StatusCode = 401;
                        await context.Response.Body.WriteAsync(result, 0, result.Length);
                        return false;
                    }
                }
                else
                {
                    apiResponse.Status = (int)EnumMasterErrorCode.Invalid_Token;
                    apiResponse.Error = new List<Errors>(){ GetErrors("Token", EnumMasterErrorCode.Invalid_Token, EnumSeqMessage.Tool_Token_Is_Required) };
                    byte[] result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(apiResponse));
                    context.Response.StatusCode = 401;
                    await context.Response.Body.WriteAsync(result, 0, result.Length);
                    return false;
                }
            }
            catch
            {
                context.Response.StatusCode = (int)EnumMasterErrorCode.Invalid_Token;
                apiResponse.Status = (int)EnumMasterErrorCode.Invalid_Token;
                apiResponse.Error = new List<Errors>(){ GetErrors("Token", EnumMasterErrorCode.Invalid_Token, EnumSeqMessage.Exception_Problem) };
                byte[] result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(apiResponse));
                await context.Response.Body.WriteAsync(result, 0, result.Length);
                return false;
            }
            return IsTokenSuccess;
        }
    }
}
