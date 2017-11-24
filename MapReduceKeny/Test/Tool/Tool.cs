using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MapReduceKeny
{
    public class Tool
    {
        public static string conn = GetAppSettings("conn");

        public static String GetAppSettings(String key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
