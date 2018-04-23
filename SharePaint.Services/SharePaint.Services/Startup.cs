using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShareLine.Data.Services;
using ShareLine.Services.ServiceProviders;
using SharePaint.Data.Services;
using SharePaint.Services.Filters;
using SharePaint.Services.Middlewares;
using SharePaint.Services.ServiceProviders;
using ShareStroke.Data.Services;
using ShareStroke.Services.ServiceProviders;
using ShareUser.Data.Services;

namespace SharePaint.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts =>
            {
                var userDataService = new UserDataServiesProvider();
                IAuthonticationService authorizationService = new AuthonticationServiceProvider(userDataService);
                opts.Filters.Add(new KeyAuthorizeFilter(new AuthorizationPolicyBuilder().RequireRole("Key").Build(), authorizationService));

            });
            services.AddCors(options => options.AddPolicy("AllowCors",
          builder =>
          {

              builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
          }));

            services.AddTransient<IUserServies, UserServiceProvider>();
            services.AddSingleton<IUserDataService, UserDataServiesProvider>();

            services.AddTransient<IPaintServies, PaintServiceProvider>();
            services.AddSingleton<IPaintDataService, PaintDataServiesProvider>();

            services.AddTransient<IUserServies, UserServiceProvider>();
            services.AddSingleton<IUserDataService, UserDataServiesProvider>();

            services.AddTransient<IStrokeService, StrokeServiceProvider>();
            services.AddSingleton<IStrokeDataService, StrokeDataServiesProvider>();

            services.AddTransient<ILineService, LineServiceProvider>();
            services.AddSingleton<ILineDataService, LineDataServiesProvider>();

            services.AddTransient<IReceiveDataHandler, ReceiveDataHandler>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseWebSockets();
            // Add global exception handler
            app.UseMiddleware<UnhandledExceptionFilter>();
            app.UseMiddleware<PaintWebSocketMiddleware>();
            app.UseMvc();
            app.UseCors("AllowCors");
        }
    }
}
