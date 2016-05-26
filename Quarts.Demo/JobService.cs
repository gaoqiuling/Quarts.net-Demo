using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Quarts.Demo
{
    public partial class JobService : ServiceBase
    {
        public JobService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Runner.Instance.Run();
            Log.Info("-------Start Service-------");
        }

        protected override void OnStop()
        {
            Runner.Instance.Stop();
            Log.Info("-------Stop Service-------");
        }
    }
}
