using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Quarts.Demo
{
    public class JobConfigUtility
    {
        /// <summary>
        /// 加载job
        /// </summary>
        public static Dictionary<string, JobData> LoadJobs()
        {
            Dictionary<string, JobData> dicJobData = new Dictionary<string, JobData>();
            string xmlFileName = string.Format("{0}/JobConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
            XmlNodeList nodes = XmlHelper.GetXmlNodeListByXpath(xmlFileName, "/JobList/Job");
            if (nodes == null)
                return dicJobData;
            foreach (XmlNode node in nodes)
            {
                if (node == null)
                    continue;
                JobData job = new JobData() { Job = new Job() };
                if (node.Attributes["name"] != null)
                    job.JobName = node.Attributes["name"].Value;
                if (node.Attributes["type"] != null)
                    job.JobType = node.Attributes["type"].Value;
                if (!node.HasChildNodes)
                    continue;
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n == null)
                        continue;
                    switch (n.Name)
                    {
                        case "RequestUrl":
                            job.RequestUrl = n.InnerText;
                            break;
                        case "CronExpression":
                            job.CronExpression = n.InnerText;
                            break;
                        case "WSUrl":
                            job.WSUrl = n.InnerText;
                            break;
                        case "UserAgent":
                            job.UserAgent = n.InnerText;
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(job.JobName) && !string.IsNullOrEmpty(job.JobType) && !string.IsNullOrEmpty(job.CronExpression))
                    dicJobData.Add(job.JobName, job);
            }
            return dicJobData;
        }
    }
}
