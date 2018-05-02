using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class LogHandler
    {

        private const string LoggerBaseDirectoryKey = "LoggerBaseDirectory";
        private const string LoggingLevelKey = "LoggingLevel";

        /// <summary>
        /// Microservices - Initialize logger with correlation id using Ninject
        /// </summary>  
        /// <returns>logger</returns>     
        public static ILogger InitializeLogger()
        {
            return Initialize(new Guid().ToString());
        }
        public static ILogger InitializeLoggerWithCorrelationId(string correlationId)
        {
            return Initialize(correlationId);
        }
        private static ILogger Initialize(string correlationId)
        {
            var logger = new Logger();
            Guid correlationGuid;
            if (correlationId != null)
            {
                if (Guid.TryParse(correlationId, out correlationGuid))
                    logger.SetCorrelationId(correlationGuid);
            }
            return logger;
        }
        /// <summary>
        /// Reads the base directory and logging level from the config and initialize logging Config
        /// </summary>
        public static void InitialiseLoggingConfig(string applicationName = "", bool enableCorrelationId = true)
        {
            string loggerBaseDirectory = ConfigurationManager.AppSettings[LoggerBaseDirectoryKey];
            string loggingLevel = ConfigurationManager.AppSettings[LoggingLevelKey];
            if (loggerBaseDirectory != null)
                ConfigurationManager.AppSettings[LoggerBaseDirectoryKey] = loggerBaseDirectory;
            if (loggingLevel != null)
                ConfigurationManager.AppSettings[LoggingLevelKey] = loggingLevel;
            Logger.InitialiseLogging( applicationName, enableCorrelationId);

        }
    }
}
