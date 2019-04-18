using System.Configuration;

namespace Companion.Connect.Automation
{
    public class Config
    {
        public static string Browser => ConfigurationManager.AppSettings["Browser"];
        public static string Url => ConfigurationManager.AppSettings["SiteUrl"];
        public static string UserName => ConfigurationManager.AppSettings["Email"];
        public static string Password => ConfigurationManager.AppSettings["Password"];
    }
}
