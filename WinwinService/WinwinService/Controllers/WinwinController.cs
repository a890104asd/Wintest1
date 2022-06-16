using DataBase;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WinwinService.Base;
using WinwinService.Models;

namespace WinwinService.Controllers
{                                                                                                                                                                                                                            
    [Route("[controller]")]
    [ApiController]
    public class WinwinController : WinwinApiBase
    {
        private string ErpUrl { get; set; }
        private string TrackUrl { get; set; }
        private List<Errors> Errors { get; set; }
        private DatabaseConnection WarehouseDbConntction { get; set; }
        private DatabaseConnection CommonDbConntction { get; set; }
        private Dictionary<string, string> WarehouseName { get; set; }


        public WinwinController(IConfiguration config, ILogger<WinwinController> _logger)
        {
            logger = _logger;
            CommonDbConntction = ApiConfiguration.CommonDbConnection;
            Errors = new List<Errors>();
        }

        [Route("creeate_account")]
        [HttpPost]
        [MiddlewareFilter(typeof(FromToolAuthenFilterMiddleware))]
        [ApiRequestLogAttribute]
        public ApiResponse<ApiResponseData> GetWarehouseList([FromBody]ApiRequest<CreateAccountRequest> Request)
        {

            #region Property
            List<string> DestinationList = new List<string>();
            #endregion
            ApiResponse<ApiResponseData> response = new ApiResponse<ApiResponseData>()
            {
                Status = (int)EnumMasterErrorCode.Successful,
            };
            try
            {
                CreateAccountRequest request = Request?.Data;
                //verify Request Format
                if (request == null)
                {
                    response.Status = (int)EnumMasterErrorCode.DataFailed;
                    Errors.Add(GetErrors("Request Format", EnumMasterErrorCode.DataFailed, EnumApiCode.StockOut_GetWareHouseList, EnumSeqMessage.Request_Is_Null));
                    response.Error = Errors;
                    return response;
                }

                if (!IsValidEmail(request.Email))
                {
                    response.Status = (int)EnumMasterErrorCode.DataFailed;
                    Errors.Add(GetErrors("信箱不符合規定", EnumMasterErrorCode.DataFailed, EnumApiCode.StockOut_GetWareHouseList, EnumSeqMessage.Cant_Be_Null));
                    response.Error = Errors;
                    return response;
                }
                if (!IsValidPassword(request.Password))
                {
                    response.Status = (int)EnumMasterErrorCode.DataFailed;
                    Errors.Add(GetErrors("密碼不符合規定", EnumMasterErrorCode.DataFailed, EnumApiCode.StockOut_GetWareHouseList, EnumSeqMessage.Cant_Be_Null));
                    response.Error = Errors;
                    return response;
                }
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    response.Status = (int)EnumMasterErrorCode.DataFailed;
                    Errors.Add(GetErrors("姓名不可為空", EnumMasterErrorCode.DataFailed, EnumApiCode.StockOut_GetWareHouseList, EnumSeqMessage.Cant_Be_Null));
                    response.Error = Errors;
                    return response;
                }

                using (CommonContext context = new CommonContext(CommonDbConntction.DbConnectionStr))
                {
                    var result = context.UserAccount
                                             .Where(x => x.Email == request.Email)
                                             .Select(x => x.Email).Distinct().ToString();

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        UserAccount NewItem = new UserAccount()
                        {
                            Email = request.Email,
                            Password = request.Password,
                            Name = request.Name,
                            Age = request.Age,
                            Gender = request.Gender,
                            Area = request.Email
                        };

                        context.UserAccount.Add(NewItem);
                        context.SaveChanges();
                    }
                    else
                    {
                        response.Status = (int)EnumMasterErrorCode.Internal_System_Error;
                        Errors.Add(GetErrors("此信箱已註冊", EnumMasterErrorCode.Internal_System_Error, EnumApiCode.StockOut_GetWareHouseList, EnumSeqMessage.Exception_Problem));
                        response.Error = Errors;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = (int)EnumMasterErrorCode.Internal_System_Error;
                Errors.Add(GetErrors(ex.ToString(), EnumMasterErrorCode.Internal_System_Error, EnumApiCode.StockOut_GetWareHouseList, EnumSeqMessage.Exception_Problem));
                response.Error = Errors;
            }

            return response;
        }
 

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidPassword(string plainText)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6}$");
            Match match = regex.Match(plainText);
            return match.Success;
        }
    }
}