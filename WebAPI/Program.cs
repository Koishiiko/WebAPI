using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebAPI {
    public class Program {

        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(builder => {
                    builder.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder => {
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
