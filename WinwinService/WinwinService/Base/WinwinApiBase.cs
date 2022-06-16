using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WinwinService.Models;

namespace WinwinService.Base
{
    public class WinwinApiBase : ControllerBase
    {
        public const string MimeJson = "application/json";
        protected ApiLanguage apilanguage;
        IConfiguration Config { get; set; }
        protected WinwinApiConfiguration ApiConfiguration { get; set; }
        protected ILogger logger { get; set; }
        protected WinwinApiBase()
        {
            ApiConfiguration = new WinwinApiConfiguration();
        }

        #region ConnectToApi
        public async Task<ApiResponse<T>> ConnectToApiAsync<T>(CallApiRequest callapirequest) where T : class
        {
            ApiResponse<T> response = new ApiResponse<T>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage();
                    // Content-Type 用於宣告遞送給對方的文件型態
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    // 新增需求Header
                    foreach (KeyValuePair<string, string> header in callapirequest.Headers)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                    //Decide what method the api use
                    switch (callapirequest.ApiMethod)
                    {
                        case EnumHttpMethod.POST:
                            httpResponse = await client.PostAsJsonAsync(callapirequest.Url, new { data = callapirequest.Request });
                            break;
                        case EnumHttpMethod.GET:
                            httpResponse = await client.GetAsync(callapirequest.Url);
                            break;
                        case EnumHttpMethod.PUT:
                            httpResponse = await client.PutAsJsonAsync(callapirequest.Url, new { data = callapirequest.Request });
                            break;
                        case EnumHttpMethod.PATCH:
                            httpResponse = await client.PatchAsync(callapirequest.Url, new StringContent(JsonConvert.SerializeObject(callapirequest.Request), Encoding.UTF8));
                            break;
                    }

                    response = await httpResponse.Content.ReadAsAsync<ApiResponse<T>>();
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        #endregion
        #region Error Message Management
        //Errorcode for stock ApiService
        public Errors GetErrors(string key, EnumMasterErrorCode mastererrorcode, EnumSeqMessage seqmessage)
        {
            if (mastererrorcode == EnumMasterErrorCode.Internal_System_Error)
            {
                logger.LogError(Request.Path.Value + "\n" + key);
                key = "";
            }

            Errors errors = new Errors()
            {
                Key = key ?? "",
                Code = ((int)mastererrorcode).ToString() + ((int)EnumApiServiceTypeCode.STK_Service).ToString().PadLeft(2, '0') + ((int)seqmessage).ToString(),
                Message = ReturnMessage(apilanguage, (int)seqmessage).Replace("{Key}", key)
            };
            Response.StatusCode = (int)mastererrorcode;
            return errors;
        }
        public Errors GetErrors(string key, EnumMasterErrorCode mastererrorcode, EnumApiCode apicode, EnumSeqMessage seqmessage)
        {
            if (mastererrorcode == EnumMasterErrorCode.Internal_System_Error)
            {
                logger.LogError(Request.Path.Value + "\n" + key);
                key = "";
            }

            Errors errors = new Errors()
            {
                Key = key ?? "",
                Code = ((int)mastererrorcode).ToString() + ((int)EnumApiServiceTypeCode.STK_Service).ToString().PadLeft(2, '0') + ((int)apicode).ToString().PadLeft(3, '0') + ((int)seqmessage).ToString(),
                Message = ReturnMessage(apilanguage, (int)seqmessage).Replace("{Key}", key)
            };
            Response.StatusCode = (int)mastererrorcode;
            return errors;
        }
        public Errors GetErrors(string key, EnumMasterErrorCode mastererrorcode, EnumApiCode apicode, EnumSeqMessage seqmessage, string Message)
        {
            if (mastererrorcode == EnumMasterErrorCode.Internal_System_Error)
            {
                logger.LogError(Request.Path.Value + "\n" + key);
                key = "";
            }

            Errors errors = new Errors()
            {
                Key = key ?? "",
                Code = ((int)mastererrorcode).ToString() + ((int)EnumApiServiceTypeCode.STK_Service).ToString().PadLeft(2, '0') + ((int)apicode).ToString().PadLeft(3, '0') + ((int)seqmessage).ToString(),
                Message = Message
            };
            Response.StatusCode = (int)mastererrorcode;
            return errors;
        }
        //For especial error response for New Weight
        public Errors GetErrorsForNewWeight(string key, EnumMasterErrorCode mastererrorcode, string errorcode, string Message)
        {
            if (mastererrorcode == EnumMasterErrorCode.Internal_System_Error)
            {
                logger.LogError(Request.Path.Value + "\n" + key);
                key = "";
            }

            Errors errors = new Errors()
            {
                Key = key ?? "",
                Code = ((int)mastererrorcode).ToString() + errorcode,
                Message = Message
            };
            Response.StatusCode = (int)mastererrorcode;
            return errors;
        }

        public Errors GetErrors(string key, EnumMasterErrorCode mastererrorcode, EnumApiServiceTypeCode apiservicetypecode, EnumSeqMessage seqmessage)
        {
            if (mastererrorcode == EnumMasterErrorCode.Internal_System_Error)
            {
                logger.LogError(Request.Path.Value + "\n" + key);
                key = "";
            }

            Errors errors = new Errors()
            {
                Key = key,
                Code = ((int)mastererrorcode).ToString() + ((int)apiservicetypecode).ToString() + ((int)seqmessage).ToString(),
                Message = ""
            };

            return errors;
        }

        public bool RequestIsValid<T>(ApiResponse<T> response, EnumApiCode apiCode) where T : class
        {
            if(!ModelState.IsValid)
            {
                response.Status = (int)EnumMasterErrorCode.DataFailed;
                response.Error = new List<Errors>();
                foreach (string propertyName in ModelState.Keys.ToList<string>())
                {
                    response.Error.Add(GetErrors(propertyName, EnumMasterErrorCode.DataFailed, apiCode, EnumSeqMessage.Cant_Be_Null));
                }
            }
            return true;
        }

        protected void SetApiLanguage(string requestlanguage)
        {
            if (string.IsNullOrWhiteSpace(requestlanguage))
            {
                apilanguage = ApiLanguage.en;
                return;
            }
            if (Enum.TryParse(requestlanguage, out apilanguage))
            {
                switch (apilanguage)
                {
                    case ApiLanguage.zh_TW:
                        apilanguage = ApiLanguage.zh_TW;
                        break;
                    default:
                        apilanguage = ApiLanguage.en;
                        break;
                }
            }

        }

        private Dictionary<int, string> SetMessageInEn()
        {
            return new Dictionary<int, string>()
            {
                {9000,"Request Format is null!"},
                {9001,"Token is Invalid!"},
                {9002,"Token is Required"},
                {9003,"{Key} System Service Exception"},
                {9004,"{Key} Database updated failed"},
                {9005,"{Key} Data not found"},
                {9006,"Update Erp data failed"},
                {9007,"Track Update Failed:{Key}"},
                {9008,"Data limited!"},
                {9009,"Service is closed" },
                {9010, "Get live label failed." },
                {9200,"{Key} is overweight"},
                {9201,"{Key} is scanned"},
                {9202,"No Read!"},
                {9203,"UnKnow"},
                {9204,"The Process: {Key} has been responsed"},
                {9205,"The Process: {Key} is Processing"},
                {9220,"{Key} No packing records"},
                {9221,"{Key} The item number has been scanned before"},
                {9222,"Hold Item !!!"},
                {9300,"Storage Download failed:{Key}"},
                {9301,"connecting Storage failed"},
                {9500,"Hold request already existed!"},
                //Public
                {9994,"{Key} format is wrong."},
                {9995,"{Key} is invalid."},
                {9996,"{Key} is duplicated."},
                {9997,"{Key} length is wrong."},
                {9998,"{Key} type is wrong."},
                {9999,"{Key} can't be null."},
            };
        }
        private string ReturnMessage(ApiLanguage language, int seq)
        {
            Dictionary<int, string> messages = new Dictionary<int, string>();
            switch (language)
            {
                case ApiLanguage.en:
                    messages = SetMessageInEn();
                    break;
                case ApiLanguage.zh_TW:
                    //messages = SetMessageInCn();
                    break;

                default:
                    messages = SetMessageInEn();
                    break;
            }

            return messages[seq];
        }
        #endregion
        #region Other Applications
        protected string FilterBarcode(string barcode)
        {
            switch (barcode.Length)
            {
                case 13:
                    string str = barcode.Substring(0, 2);

                    if ("QB" == str ||
                        "QC" == str ||
                        "QD" == str ||
                        "RZ" == str ||
                        "VZ" == str ||
                        "XZ" == str ||
                        "RS" == str)
                    {
                        return barcode.Substring(0, 12);
                    }
                    break;

                case 18:
                    if ("9979" == barcode.Substring(0, 4))
                    {
                        return barcode.Substring(4, 14);
                    }
                    break;

                case 28:
                    if ("%00" == barcode.Substring(0, 3) && ("801250" == barcode.Substring(22, 6) || "802250" == barcode.Substring(22, 6)) && ("6A" == barcode.Substring(10, 2) || "6C" == barcode.Substring(10, 2)))
                    {
                        int Ncp = 0;
                        int Nci = 0;
                        int BCle = 0;
                        int NCle = 0;
                        string FilterBarcode = barcode.Substring(10, 12);
                        char[] filter_number = FilterBarcode.ToArray();
                        for (int i = 2; i < filter_number.Length; i++)
                        {
                            int x = (int)char.GetNumericValue(filter_number[i]);
                            if (x == -1)
                                return FilterBarcode;
                            Ncp = Ncp + x;
                            i++;
                        }
                        for (int i = 3; i < filter_number.Length; i++)
                        {
                            int x = (int)char.GetNumericValue(filter_number[i]);
                            if (x == -1)
                                return FilterBarcode;
                            Nci = Nci + x;
                            i++;
                        }
                        BCle = Nci * 3 + Ncp;
                        NCle = Convert.ToInt16(Math.Floor((double)(BCle / 10))) * 10;
                        if (BCle > NCle)
                            NCle = NCle + 10;

                        NCle = NCle - BCle;

                        return FilterBarcode + NCle.ToString();
                    }


                    if ("%" == barcode.Substring(0, 1) && "1550" == barcode.Substring(8, 4))
                    {
                        return barcode.Substring(8, 14);
                    }
                    break;

                case 30:
                    if ("94" == barcode.Substring(8, 2))
                    {
                        return barcode.Substring(8, 22);
                    }

                    if ("92" == barcode.Substring(8, 2))
                    {
                        return barcode.Substring(8, 22);
                    }
                    break;

                case 34:
                    if ("92" == barcode.Substring(8, 2))
                    {
                        return barcode.Substring(8, 26);
                    }

                    if ("96" == barcode.Substring(0, 2))
                    {
                        return barcode.Substring(22, 12);
                    }
                    break;
  
                case 41:
                    if ("019931265099999891" == barcode.Substring(0, 18))
                    {
                        return barcode.Substring(18, 23);
                    }
                    break;
            }

            return barcode;
        }
        protected int VolumetricWeightCalculation(int height, int width, int length)
        {
            return (int)Math.Round(ConvertToCM(height) * ConvertToCM(width) * ConvertToCM(length) / 5.0, MidpointRounding.AwayFromZero);
        }

        protected int ConvertToCM(int? num)
        {
            return Convert.ToInt16(Math.Ceiling((decimal)num / 10));
        }
        public bool SubwaybillValidation(ref string boxid)
        {
            if (boxid.Substring(0, 3) == "JJD"
                || boxid.Substring(0, 2) == "1Z"
                || boxid.Substring(0, 2) == "JD")
            {
                return true;
            }

            if (boxid.Length == 34)
            {
                boxid = boxid.Substring(boxid.Length - 12);
                return true;
            }

            return false;
        }

        //The Function for copying model to new model
        public static TTarget CopyModelTo<TSource, TTarget>(TSource source) where TTarget : new()
        {
            var target = new TTarget();
            var sourceProps = typeof(TSource).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TTarget).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(target, sourceProp.GetValue(source, null), null);
                    }
                }
            }

            return target;
        }

        public string BoxIdFilter(string box_id)
        {
            string result = "";// Fail Case Result will be empty.
            int box_char_num = 0;
            int box_zero_length = 0;

            foreach (char c in box_id)
            {
                box_char_num = (int)char.GetNumericValue(c);
                if (box_char_num == 0)
                    box_zero_length++;
                else
                    break;
            }
            result = box_id.Remove(0, box_zero_length);
            return result;
        }

        #endregion
    }
}
