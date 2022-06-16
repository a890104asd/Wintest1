using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinwinService.Models
{
    public class DatabaseConnection
    {
        public string ServerIp
        {
            get;
            set;
        }

        public string UserId
        {
            get;set;
        }

        public string UserPwd
        {
            get;set;
        }

        public string Database
        {
            get;set;
        }

        public string DbConnectionStr
        {
            get 
            {
                string str = string.Format("Server={0};User Id={1};Password={2};Database={3}", this.ServerIp, this.UserId, this.UserPwd, this.Database);
                return str;
            }
        }
    }
}
