using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phoneshop.Business;
using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Interfaces;
using PhoneShop.Business;
using PhoneShop.Business.Repositories;
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
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IXmlService, XmlService>();

            services.AddScoped<PhoneOverview>();
        }
        //static void Main()
        //{
        //    Application.SetHighDpiMode(HighDpiMode.SystemAware);
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);

        //    var services = new ServiceCollection();

        //    ConfigureServices(services);

        //    using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        //        Application.Run(serviceProvider.GetRequiredService<PhoneOverview>());
        //}


        //private static void ConfigureServices(ServiceCollection services)
        //{
        //    services.AddDbContext<DataContext>();
        //    services.AddScoped<IPhoneService, PhoneService>();
        //    services.AddScoped<IBrandService, BrandService>();
        //    services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

        //    services.AddScoped<PhoneOverview>();
        //}
    }
}
