using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Quarts.Demo
{
    public class Runner : IRunner
    {
        #region
        private Runner() { }
        private static Runner instance;
        private static object _lock = new object();

        public static Runner Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                            instance = new Runner();
                    }
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 启动
        /// </summary>
        public void Run()
        {
            try
            {
                CacheData.DicJobs = JobConfigUtility.LoadJobs();
                foreach (string key in CacheData.DicJobs.Keys)
                {
                    if (!CacheData.DicJobs.ContainsKey(key))
                        continue;

                    //内存调度
                    ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                    IScheduler _scheduler = schedulerFactory.GetScheduler();
                    CacheData.DicJobs[key]._Scheduler = _scheduler;

                    try
                    {
                        //创建一个Job来执行特定的任务
                        IJobDetail synchronousData = new JobDetailImpl(key, CacheData.DicJobs[key].Job.GetType());

                        //创建并定义触发器的规则
                        ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(CacheData.DicJobs[key].CronExpression).Build();

                        //将创建好的任务和触发规则加入到Quartz中
                        _scheduler.ScheduleJob(synchronousData, trigger);

                        //开始
                        _scheduler.Start();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("Job: [{0}] 在 [{1:yyy-MM-dd HH:mm:ss}] 启动异常：", key, DateTime.Now), ex);
                        if (_scheduler != null)
                            _scheduler.Shutdown();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            foreach (string key in CacheData.DicJobs.Keys)
            {
                if (CacheData.DicJobs.ContainsKey(key))
                    CacheData.DicJobs[key]._Scheduler.Shutdown();
            }
        }

    }
}
