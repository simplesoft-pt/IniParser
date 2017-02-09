using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SimpleSoft.IniParser.Examples
{
    public class Program
    {
        private readonly ILogger<Program> _logger;
        private readonly List<IExample> _examples;

        public Program(ILogger<Program> logger, IEnumerable<IExample> examples)
        {
            _logger = logger;
            _examples = examples.ToList();
        }

        public void Run(string[] args)
        {
            _logger.LogDebug("Running a total of {totalExamplesToRun} examples", _examples.Count);

            foreach (var example in _examples)
            {
                example.Run();
            }

            _logger.LogDebug("All examples have been run");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Example application for SimpleSoft.IniParser started...");
            try
            {
                var services = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<Program>();

                var container = services.BuildServiceProvider(true);

                container.GetRequiredService<ILoggerFactory>()
                    .AddConsole(LogLevel.Trace, true);

                container.GetRequiredService<Program>().Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("A critical exception has been thrown during startup");
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Example application for SimpleSoft.IniParser terminated. Press <enter> to exit...");
                Console.ReadLine();
            }
        }
    }
}
