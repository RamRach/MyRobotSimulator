using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{

    public interface ILogger
    {
        /// <summary>
        /// The correlation id this logger is currently associated with.
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// Sets the correlation id for all log messages.
        /// </summary>
        /// <param name="correlationId"></param>
        void SetCorrelationId(Guid correlationId);

        /// <summary>
        /// Logs a message at the debug log level
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        /// <summary>
        /// Logs a message at the debug log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        void Debug(string message, object data);

        /// <summary>
        /// Logs a message at the debug log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Debug(string message, Exception ex);

        /// <summary>
        /// Logs a message at the debug log level with the specified object data and exception information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        void Debug(string message, object data, Exception ex);

        /// <summary>
        /// Logs a message at the info log level
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        /// Logs a message at the info log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        void Info(string message, object data);

        /// <summary>
        /// Logs a message at the info log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Info(string message, Exception ex);

        /// <summary>
        /// Logs a message at the info log level with the specified object data and exception information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        void Info(string message, object data, Exception ex);

        /// <summary>
        /// Logs a message at the warn log level
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);

        /// <summary>
        /// Logs a message at the warn log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        void Warn(string message, object data);

        /// <summary>
        /// Logs a message at the warn log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Warn(string message, Exception ex);

        /// <summary>
        /// Logs a message at the warn log level with the specified object data and exception information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        void Warn(string message, object data, Exception ex);

        /// <summary>
        /// Logs a message at the error log level
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        /// <summary>
        /// Logs a message at the error log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        void Error(string message, object data);

        /// <summary>
        /// Logs a message at the error log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Error(string message, Exception ex);

        /// <summary>
        /// Logs a message at the error log level with the specified object data and exception information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        void Error(string message, object data, Exception ex);

        /// <summary>
        /// Logs a message at the fatal log level
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);

        /// <summary>
        /// Logs a message at the fatal log level with the specified object data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        void Fatal(string message, object data);

        /// <summary>
        /// Logs a message at the fatal log level with the specified exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Fatal(string message, Exception ex);

        /// <summary>
        /// Logs a message at the fatal log level with the specified object data and exception information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        void Fatal(string message, object data, Exception ex);

        /// <summary>
        /// Logs a message at the specified log level with the specified object data and exception information
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        void Log(NLog.LogLevel level, string message, object data = null, Exception ex = null);

    }
}