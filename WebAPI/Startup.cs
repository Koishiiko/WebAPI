using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebAPI.autofac;
using WebAPI.formatter;
using WebAPI.middleware;
using WebAPI.utils;
using Microsoft.EntityFrameworkCore;

namespace WebAPI {
    public class Startup {

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddOptions();

            services.Configure<WebAPISettings>(Configuration.GetSection(nameof(WebAPISettings)));

            services.AddCors(option => {
                option.AddPolicy("allowAny", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            services.AddControllers(options => {
                //options.Filters.Add<LogFilter>();
                //options.Filters.Add<ResultFilter>();
                options.InputFormatters.Insert(0, new RequestFormatter());
                options.OutputFormatters.Insert(0, new ResultFormatter());
            }).AddJsonOptions(options => {
                // FIXME: ���ò���Ч
                /*
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = true;
                */
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

            //services.AddDbContext<WebAPIContext>(options => {
            //    options.UseSqlServer(AppSettings.MSSQLString);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IOptions<WebAPISettings> webAPISettings) {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            AppSettings.Initial(webAPISettings.Value);

            loggerFactory.AddLog4Net(AppSettings.LogConfigPath);

            app.UseCors("allowAny");

            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(AppSettings.FolderPath),
                RequestPath = "/files"
            });

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder) {
            builder.RegisterModule<AutofacRegisterModule>();
        }
    }
}
