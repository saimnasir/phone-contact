using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneContact.Extensions;
using PhoneContact.Repositories;
using Polly;
using Repositories;
using System;

namespace PhoneContact
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});
            services.AddPhoneContactContext(Configuration.GetSection("ConnectionStrings:ContactDBConnectionString").Value);

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //services.AddHangfire(configuration => configuration
            //  .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //  .UseSimpleAssemblyNameTypeSerializer()
            //  .UseRecommendedSerializerSettings()
            //  .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnectionString"), 
            //      new SqlServerStorageOptions
            //      {
            //          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //          SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //          QueuePollInterval = TimeSpan.Zero,
            //          UseRecommendedIsolationLevel = true,
            //          DisableGlobalLocks = true
            //      }
            //  ));

            // Add the processing server as IHostedService
            //services.AddHangfireServer();
            services.AddControllers();
            services.AddSwaggerGen();

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddControllersWithViews();

            serviceCollection.AddPhoneContactContext(Configuration.GetSection("DataSource:ConnectionString").Value);

            serviceCollection.AddAutoMapper(typeof(Startup));
            serviceCollection.AddScoped<IRepositoryFactory, RepositoryFactory>();
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            serviceCollection.AddControllers();
            serviceCollection.AddSwaggerGen();

            builder.Populate(serviceCollection);
            // var serviceProvider = new AutofacServiceProvider(builder.Build());

            // Register your own things directly with Autofac, like:
            //   builder.RegisterModule(new MyApplicationModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ExecuteMigrations(app, env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                // endpoints.MapHangfireDashboard();
            });

           // app.UseHangfireDashboard("/PhoneContactContactJobs");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Phone Contact API V1");
            });
            //StartJobs(recurringJobManager, phoneContactOperations);
        }

        private void ExecuteMigrations(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Testing") return;

            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(new TimeSpan[]
                {
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(6),
                    TimeSpan.FromSeconds(12)
                });

            retry.Execute(() =>
                app.ApplicationServices.GetService<PhoneContactContext>().Database.Migrate());
        }
        //private void StartJobs(IRecurringJobManager recurringJobManager, IPhoneContactOperations phoneContactOperations)
        //{
        //    recurringJobManager.AddOrUpdate("ProcessReport", Job.FromExpression(() => phoneContactOperations.ProcessPendingReports()), "*/1 * * * *");
        //}
    }
}
