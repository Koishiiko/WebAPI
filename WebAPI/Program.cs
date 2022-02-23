using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Spire.Xls;

namespace WebAPI {
    public class Program {

        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(builder => {
                    builder.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.CaptureStartupErrors(true);
                    webBuilder.ConfigureKestrel(options => {
                        options.ConfigureHttpsDefaults(co => {
                            co.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
