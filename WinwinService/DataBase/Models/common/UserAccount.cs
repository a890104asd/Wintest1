using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class UserAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Area { get; set; }
    }
}
