using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LearnLogging
{
    class Program
    {
        private static readonly int CHOICE_ARGS_INDEX = 0;

        private static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            LoadConfiguration();

            try
            {
                var choice = Convert.ToInt32(args[CHOICE_ARGS_INDEX]);
                switch (choice)
                {
                    case 1:
                        TraceListenerDemo();
                        break;
                    case 2:
                        TraceSwitchDemo();
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // =============================================
        // =                   DEMO                    =
        // =============================================

        private static void TraceListenerDemo()
        {
            AddTextFileTraceListener();
            Debug.WriteLine("Debug says, I am watching!");
            Trace.WriteLine("Trace says, I am watching!");
        }

        /// <summary>
        /// Switching trace levels
        /// </summary>
        private static void TraceSwitchDemo()
        {
            var traceSwitch = new TraceSwitch(displayName: "CustomSwitch", description: "");
            configuration.GetSection("CustomSwitch").Bind(traceSwitch);

            AddTextFileTraceListener();
            Trace.WriteLineIf(traceSwitch.TraceError, "Available to trace at error level.");
            Trace.WriteLineIf(traceSwitch.TraceWarning, "Available to trace at warning level.");
            Trace.WriteLineIf(traceSwitch.TraceInfo, "Available to trace at info level.");
            Trace.WriteLineIf(traceSwitch.TraceVerbose, "Available to trace at verbose level.");
        }

        // =============================================
        // =                  HELPER                   =
        // =============================================

        private static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        private static void AddTextFileTraceListener()
        {
            // write to a text file in the project folder
            Trace.Listeners.Add(new TextWriterTraceListener(
            File.CreateText("log.txt")));
            // text writer is buffered, so this option calls
            // Flush() on all listeners after writing
            Trace.AutoFlush = true;
        }
    }
}
