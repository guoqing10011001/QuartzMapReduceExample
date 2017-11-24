using MapReduceKeny;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Test.Log;
using Test.Trigger;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LogHelper.info("程序启动");

                LogHelper.info("准备启动JOB注册器");
                TriggerManager.RegisterJob();
                LogHelper.info("启动JOB注册器完成");
                //阻塞守护调度任务
                Console.ReadLine();
            }catch(Exception err)
            {
                LogHelper.info("程序启动发生了错误");
                LogHelper.info(err.Message);
            }

            LogHelper.info("程序结束了");
        }
    }
}
