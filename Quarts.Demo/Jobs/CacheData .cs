using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quarts.Demo
{
    public class CacheData
    {
        public static Dictionary<string, JobData> DicJobs = new Dictionary<string, JobData>();
    }

    public class JobData
    {
        //必需
        public string JobName { set; get; }
        public string JobType { set; get; }
        public string CronExpression { set; get; }

        //非必需
        public string RequestUrl { set; get; }
        public string WSUrl { set; get; }
        public string UserAgent { set; get; }

        //调度器
        public IScheduler _Scheduler { set; get; }
        public IJob Job { set; get; }
    }
}
