using MapReduceKeny;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Test.Log;

namespace Test.Job
{
    public class BasalJob : IJob
    {


        public BasalJob()
        {


        }


        public virtual void Execute(IJobExecutionContext context)
        {
            try
            {

                LogHelper.info(MessageMap.MessageMap.Start.ToString());

                ///三板斧 输入 转换 输出 
                ///
                LogHelper.info("实例化投递归约对象");
                ItemCheckMapReduce mapreduce = new ItemCheckMapReduce();
                LogHelper.info("实例化投递归约对象完成");

                LogHelper.info("读取数据库连接");
                mapreduce.conn = Tool.GetAppSettings("conn");
                LogHelper.info("读取数据库连接完成");
                LogHelper.info("读取数据库连接是：\r" + mapreduce.conn);

                LogHelper.info("获得投递源");
                mapreduce.mapper();
                LogHelper.info("获得投递源完毕");

                LogHelper.info("经过转换体");
                mapreduce.reducer();
                LogHelper.info("经过转换体完成");

                LogHelper.info("输出结果");
                DataTable dt = mapreduce.result;
                ///在这里加入你的处理逻辑

                LogHelper.info("输出结果完成");

            }
            catch (Exception err)
            {
                LogHelper.info("发生错误了");
                LogHelper.info(err.Message);
            }
        }
    }
}
