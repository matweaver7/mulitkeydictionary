using System;
using ConsoleApp1.Contracts.Interfaces;
using ConsoleApp1.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ConsoleApp1.Data;
using ConsoleApp1.Data.Interfaces;

namespace ConsoleApp1.Terminal
{
    class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.AddScoped<ICustomDictionary<string, string>, MemoryDictionary<string, string>>();
                    services.AddScoped<IDictionaryService<string, string>, DictionaryService>();
                    services.AddScoped<ITerminal, TerminalController>();
                });
            IHost host = builder.Build();
            IServiceProvider serviceProvider = host.Services;
            try
            {
                serviceProvider.GetService<ITerminal>().Run();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
          
        }
    }
}
