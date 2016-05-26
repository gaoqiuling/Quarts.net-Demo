using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quarts.Demo
{
    public class Log
    {
        /// <summary>
        ///  log4net加载配置,
        ///  Application_Start 中加载
        /// </summary>
        static Log()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        static ILog Logger { get { return LogManager.GetLogger("All"); } }


        public static void Error(string msg, Exception ex)
        {
            var exception = ex.InnerException ?? ex;
            StringBuilder sb = new StringBuilder();

            //追加消息
            if (msg != null)
            {
                sb.AppendLine(msg);
            }

            Logger.Error(sb.ToString(), ex);
        }

        public static void Error(Exception ex)
        {
            Error(null, ex);
        }

        public static void Info(string info)
        {
            Logger.Info(info);
        }
        public static void InfoFormat(string format, params object[] args)
        {
            Logger.InfoFormat(format, args);
        }

        public static void Debug(string info)
        {
            Logger.Debug(info);
        }
        public static void Debug(string format, params object[] args)
        {
            Logger.DebugFormat(format, args);
        }

        public enum Level
        {
            ALL, ERROR, FATAL, INFO, DEBUG
        }
    }
}
