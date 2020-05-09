using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Online_T_Shirt_Shop.Areas.Identity.Services.EmailSender
{
    public class MessageTemplateService
    {
        private readonly string _emailTemplate;

        public MessageTemplateService(IConfiguration configuration)
        {
            _emailTemplate = File.ReadAllText(configuration.GetSection("Email")["TemplatePath"]);
        }

        public string GetMessageHtml(params object[] formatArgs)
        {
            return string.Format(_emailTemplate, formatArgs);
        }
    }
}
