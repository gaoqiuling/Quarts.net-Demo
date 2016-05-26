using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Quarts.Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Runner.Instance.Run();
            ServiceBase.Run(new JobService());
        }
    }
}
