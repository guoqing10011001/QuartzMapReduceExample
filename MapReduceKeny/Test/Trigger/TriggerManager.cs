using MapReduceKeny;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Job;
using Test.Log;

namespace Test.Trigger
{
    public class TriggerManager
    {
        public static void RegisterJob()
        {
            try
            {

                LogHelper.info("读取同步策略");

                int defaultSyncIntervalBasalData = Int32.Parse(Tool.GetAppSettings("defaultSyncIntervalBasalData"));

                LogHelper.info("读取同步策略完成");


                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler
                IScheduler sched = schedFact.GetScheduler();

                sched.Start();
                //为每个表定义一个调度器，加入调度器分组


                //基础数据
                LogHelper.info("构建作业");
                IJobDetail basalJob = JobBuilder.Create<BasalJob>()
                    .WithIdentity("BasalJob", "groupBaseData")
                    .Build();
                LogHelper.info("构建作业完成");

                LogHelper.info("构建触发器");
                // Trigger the job to run now, and then every 40 seconds
                ITrigger triggerBase = TriggerBuilder.Create()
                  .WithIdentity("JobSyncBasalTrigger", "groupBaseData")
                  .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(defaultSyncIntervalBasalData)
                      .RepeatForever())
                  .Build();
                LogHelper.info("构建触发器完毕");

                LogHelper.info("准备注册job");
                ///注册启动同步job
                sched.ScheduleJob(basalJob, triggerBase);

                LogHelper.info("注册job完毕");

            }
            catch (Exception err)
            {
                LogHelper.info("发生了错误");
                LogHelper.info(err.Message);
            }
        }
    }

}
