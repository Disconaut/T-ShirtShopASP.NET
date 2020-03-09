using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_T_Shirt_Shop.Areas.Identity.Services.EmailSender
{
    public class AuthManagerOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
