using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Test.Log
{
    public class LogHelper
    {
        public static void info(String message)
        {
            DateTime dt = DateTime.Now;


            String prefix = dt.Year + "-" + dt.Month + "-" + dt.Day;

            String attrfix = dt.Hour + ":" + dt.Minute + ":" + dt.Second;

            using (StreamWriter sw = new StreamWriter(prefix + ".log", true))
            {
                String log = message + "日志时间是：" + prefix + " " + attrfix;
                sw.WriteLine(log);
                Console.WriteLine(log);
                sw.Close();
                sw.Dispose();
            }
        }

    }
}
