using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phoneshop.Business;
using Phoneshop.Business.Interfaces;
using Phoneshop.Business.Repositories;
using Phoneshop.Domain.Interfaces;
using System;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(builder.Services.GetRequiredService<PhoneOverview>());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                ConfigureServices(hostContext.Configuration, services);
            });


        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped(typeof(IRepository<>), typeof(AdoRepository<>));
            services.AddScoped<IXmlService, XmlService>();

            services.AddScoped<PhoneOverview>();
        }
    }
}
