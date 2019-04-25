using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Configuration
{
    public class Config
    {
        public static string Browser => ConfigurationManager.AppSettings["Browser"];
        public static string Url => ConfigurationManager.AppSettings["SiteUrl"];
        public static string UserName => ConfigurationManager.AppSettings["Email"];
        public static string Password => ConfigurationManager.AppSettings["Password"];
    }
}
