using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quarts.Demo
{
    public class Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Log.InfoFormat("Job: [{0}] 在 [{1:yyy-MM-dd HH:mm:ss}] 执行了一次", ((JobDetailImpl)context.JobDetail).Name, DateTime.Now);

                if (!CacheData.DicJobs.ContainsKey(((JobDetailImpl)context.JobDetail).Name))
                    return;
                IExecute jobExecute = null;
                JobData data = CacheData.DicJobs[((JobDetailImpl)context.JobDetail).Name];
                if (data.JobType == "WebRequest")
                {
                    jobExecute = new ExecuteWebRequest(data.RequestUrl);
                }
                if (jobExecute != null)
                {
                    object value = jobExecute.ExecuteJob();
                    Log.InfoFormat("Job: [{0}] 在 [{1:yyy-MM-dd HH:mm:ss}] 返回结果: {2}", ((JobDetailImpl)context.JobDetail).Name, DateTime.Now, value);

                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Job: [{0}] 在 [{1:yyy-MM-dd HH:mm:ss}] 出现了异常：", ((JobDetailImpl)context.JobDetail).Name, DateTime.Now), ex);
            }
        }
    }
}
