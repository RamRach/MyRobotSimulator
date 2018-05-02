using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;
using Formatting = System.Xml.Formatting;

namespace Logging
{
    public class Logger : ILogger
    {
        private const string LoggerBaseDirectoryKey = "LoggerBaseDirectory";
        private const string LoggingLevelKey = "LoggingLevel";

        private static readonly ThreadLocal<Guid> ThreadStaticGuid = new ThreadLocal<Guid>(Guid.NewGuid);
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        private readonly NLog.Logger _logger;
        private Guid? _correlationId;
        private static string _applicationName;
        private static bool _enableCorrealtionGuidLogging = true;
        /// <summary>
        /// Creates an instance of the logger. This shouldn't be used directly; instead, ILogger should be injected.
        /// </summary>
        public Logger()
        {
            if (LogManager.Configuration == null)
                throw new InvalidOperationException("Logger has not been initialised. You must call Logger.InitialiseLogging() once.");
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// The correlation id this logger is currently associated with.
        /// </summary>
        public Guid CorrelationId
        {
            get { return _correlationId.HasValue ? _correlationId.Value : ThreadStaticGuid.Value; }
        }

        /// <summary>
        /// Sets the correlation id for all log messages.
        /// </summary>
        /// <param name="correlationId"></param>
        public void SetCorrelationId(Guid correlationId)
        {
            _correlationId = correlationId;
        }

        /// <summary>
        /// Logs a message at the debug log level
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            Log(NLog.LogLevel.Debug, message);
        }

        /// <summary>
        /// Logs a message at the debug log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Debug(string message, object data)
        {
            Log(NLog.LogLevel.Debug, message, data);
        }

        /// <summary>
        /// Logs a message at the debug log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Debug(string message, Exception ex)
        {
            Log(NLog.LogLevel.Debug, message, null, ex);
        }

        /// <summary>
        /// Logs a message at the debug log level with the specified object data and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        public void Debug(string message, object data, Exception ex)
        {
            Log(NLog.LogLevel.Debug, message, data, ex);
        }

        /// <summary>
        /// Logs a message at the info log level
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            Log(NLog.LogLevel.Info, message);
        }

        /// <summary>
        /// Logs a message at the info log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Info(string message, object data)
        {
            Log(NLog.LogLevel.Info, message, data);
        }

        /// <summary>
        /// Logs a message at the info log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Info(string message, Exception ex)
        {
            Log(NLog.LogLevel.Info, message, null, ex);
        }

        /// <summary>
        /// Logs a message at the info log level with the specified object data and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        public void Info(string message, object data, Exception ex)
        {
            Log(NLog.LogLevel.Info, message, data, ex);
        }

        /// <summary>
        /// Logs a message at the warn log level
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            Log(NLog.LogLevel.Warn, message);
        }

        /// <summary>
        /// Logs a message at the warn log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Warn(string message, object data)
        {
            Log(NLog.LogLevel.Warn, message, data);
        }

        /// <summary>
        /// Logs a message at the warn log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Warn(string message, Exception ex)
        {
            Log(NLog.LogLevel.Warn, message, null, ex);
        }

        /// <summary>
        /// Logs a message at the warn log level with the specified object data and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        public void Warn(string message, object data, Exception ex)
        {
            Log(NLog.LogLevel.Warn, message, data, ex);
        }

        /// <summary>
        /// Logs a message at the error log level
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Log(NLog.LogLevel.Error, message);
        }

        /// <summary>
        /// Logs a message at the error log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Error(string message, object data)
        {
            Log(NLog.LogLevel.Error, message, data);
        }

        /// <summary>
        /// Logs a message at the error log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Error(string message, Exception ex)
        {
            Log(NLog.LogLevel.Error, message, null, ex);
        }

        /// <summary>
        /// Logs a message at the error log level with the specified object data and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        public void Error(string message, object data, Exception ex)
        {
            Log(NLog.LogLevel.Error, message, data, ex);
        }

        /// <summary>
        /// Logs a message at the fatal log level
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            Log(NLog.LogLevel.Fatal, message);
        }

        /// <summary>
        /// Logs a message at the fatal log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Fatal(string message, object data)
        {
            Log(NLog.LogLevel.Fatal, message, data);
        }

        /// <summary>
        /// Logs a message at the fatal log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Fatal(string message, Exception ex)
        {
            Log(NLog.LogLevel.Fatal, message, null, ex);
        }

        /// <summary>
        /// Logs a message at the fatal log level with the specified object data and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        public void Fatal(string message, object data, Exception ex)
        {
            Log(NLog.LogLevel.Fatal, message, data, ex);
        }

        /// <summary>
        /// Logs a message at the specified log level with the specified object data and exception information
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        public void Log(NLog.LogLevel level, string message, object data = null, Exception ex = null)
        {
            var eventInfo = new LogEventInfo(level, "", message)
            {
                Exception = ex
            };

            if (data == null)
                eventInfo.Properties["relatedData"] = null;
            else
                eventInfo.Properties["relatedData"] = JsonConvert.SerializeObject(data, (Newtonsoft.Json.Formatting) Formatting.None, JsonSettings);
            if (_enableCorrealtionGuidLogging == true)
                eventInfo.Properties["correlationId"] = CorrelationId;
            else
                eventInfo.Properties["correlationId"] = null;
            eventInfo.Properties["applicationName"] =  _applicationName;
            _logger.Log(eventInfo);
        }

        /// <summary>
        /// Initialise the logging system, loading config values from the app/web config.
        /// You must have two app settings configured:
        /// LoggerBaseDirectory: the base directory to put the logging folder in. NLog variables can be used such as ${basedir}
        /// LoggingLevel: the minimum log level. Can be one of: Debug, Info, Warn, Error, Fatal, Off,
        /// </summary>
        /// <param name="config"></param>
        internal static void InitialiseLogging(string applicationName = "", bool enableCorrelationId = true)
        {
            var loggingBaseDirectory = ConfigurationManager.AppSettings[LoggerBaseDirectoryKey];
            var loggingLevel = ConfigurationManager.AppSettings[LoggingLevelKey];
            _applicationName = applicationName;
            _enableCorrealtionGuidLogging = enableCorrelationId;
            if (string.IsNullOrEmpty(loggingBaseDirectory))
                throw new ConfigurationErrorsException(string.Format("AppSettings should have '{0}' key.", LoggerBaseDirectoryKey));
            if (string.IsNullOrEmpty(loggingLevel))
                throw new ConfigurationErrorsException(string.Format("AppSettings should have '{0}' key.", LoggingLevelKey));

            const string layout = "{\"date\":\"${date:universalTime=True:format=yyyy-MM-ddTHH\\:mm\\:ss.fff}\", " +
                                  "\"level\":\"${level}\", " +
                                  "\"applicationName\":\"${event-context:item=applicationName}\", " +
                                  "\"message\":\"${message:jsonEncode=true}\", " +
                                  "\"relatedData\": \"${event-context:item=relatedData:format=type:jsonEncode=true}\", " +
                                  "\"correlationId\":\"${event-context:item=correlationId}\"" +
                                  "${onexception:, \"exception\"\\: { \"type\"\\: \"${exception:format=type:jsonEncode=true}\", \"message\"\\:\"${exception:jsonEncode=true:format=message,method}\", \"stacktrace\"\\:\"${exception:jsonEncode=true:StackTrace:maxInnerExceptionLevel=5}\" } } ${onexception:\\} }";

            var consoleTarget = new ColoredConsoleTarget { Layout = layout };

            var fileTarget = new FileTarget
            {
                Layout = layout,
                FileName = string.Format("{0}/log.txt", loggingBaseDirectory),
                //FileName="Logfile.txt",
                CreateDirs = true,
                ArchiveFileName = string.Format("{0}/log", loggingBaseDirectory) + ".{##}.txt",
                //ArchiveFileName = "log.{####}.txt",
                KeepFileOpen = false,
                ConcurrentWrites = true,
                //ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                ArchiveAboveSize = 9000000,//9 MB
                MaxArchiveFiles = 45
            };

            var loggingConfig = new LoggingConfiguration();

            loggingConfig.AddTarget("console", consoleTarget);
            loggingConfig.AddTarget("file", fileTarget);

            var logLevel = NLog.LogLevel.FromString(loggingLevel);
            loggingConfig.LoggingRules.Add(new LoggingRule("*", logLevel, consoleTarget));
            loggingConfig.LoggingRules.Add(new LoggingRule("*", logLevel, fileTarget));

            LogManager.Configuration = loggingConfig;
        }

    }
}
