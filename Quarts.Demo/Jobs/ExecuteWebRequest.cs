using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Quarts.Demo
{
    public class ExecuteWebRequest : IExecute
    {
        private string Url { set; get; }

        public ExecuteWebRequest(string url)
        {
            this.Url = url;
        }

        public object ExecuteJob()
        {
            WebClient client = new WebClient();
            client.Headers["User-Agent"] = "JobService";
            client.Encoding = System.Text.Encoding.UTF8;
            string content = client.DownloadString(Url);
            return content;
        }
    }
}
