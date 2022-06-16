using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace WinwinService.Models
{
    public class UpdateSystemSettings
    {
        public FTPInfo GetFTPInfo(int Env)
        {
            FTPInfo ftpinfo = null;
            switch (Env)
            {
                case (1):
                    ftpinfo = UpdateDev;
                    break;
                case (2):
                    ftpinfo = UpdateUat;
                    break;
                case (3):
                    ftpinfo = UpdateProduction;
                    break;
                case (4):
                    ftpinfo = UpdateProductionCN;
                    break;
                case (5):
                    ftpinfo = UpdatePudoUat;
                    break;
                case (6):
                    ftpinfo = UpdatePudoProduction;
                    break;
                case (7):
                    ftpinfo = GetFastestFtpUrl(UpdateFtpUat);
                    break;
                default:
                    ftpinfo = UpdateProduction;
                    break;
            }
            return ftpinfo;


        }
        public FTPInfo GetFastestFtpUrl(List<FTPInfo> fTPInfoUat)
        {
            FTPInfo ftpinfo = null;
            Stopwatch stop_Agility = new Stopwatch();
            TimeSpan fastestTs = new TimeSpan(9, 59, 59);
            //foreach (var element in fTPInfoUat.GetType().GetProperties())
                foreach (var element in fTPInfoUat)
                {

                FtpWebRequest request;
                try
                {
                    stop_Agility.Start();
                    request = (FtpWebRequest)FtpWebRequest.Create(element.ServerUrl);
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    request.Credentials = new NetworkCredential(element.FtpAccount, element.FtpPassword);
                    request.Timeout = 1000;
                    request.GetResponse();
                    stop_Agility.Stop();                                   
                    TimeSpan ts = stop_Agility.Elapsed;
                    if (0 > ts.CompareTo(fastestTs))
                    {
                        fastestTs = ts;
                        ftpinfo = element;
                    }
                    stop_Agility.Reset();
                }
                catch //FTP連線失敗就跳下一個
                {
                    stop_Agility.Reset();
                    continue;
                }
            }
            return ftpinfo;
        }
        public FTPInfo UpdateDev { get; set; }
        public FTPInfo UpdateUat { get; set; }
        public FTPInfo UpdateProduction { get; set; }
        public FTPInfo UpdateProductionCN { get; set; }
        public FTPInfo UpdatePudoUat { get; set; }
        public FTPInfo UpdatePudoProduction { get; set; }
        //public FTPInfoUat UpdateFtpUat { get; set; }
        public List<FTPInfo> UpdateFtpUat { get; set; }
    }
    public class FTPInfoUat
    {
        public List<FTPInfo> FTPInfo { get; set; }
    }

    public class FTPInfo
    {
        public string ServerUrl { get; set; }
        public string BranchFolder { get; set; }
        public string FtpAccount { get; set; }
        public string FtpPassword { get; set; }
    }

}
