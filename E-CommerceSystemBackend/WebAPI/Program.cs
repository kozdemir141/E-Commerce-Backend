using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            //Autofac kayıtlı bir IOC Container değil bunu .netCore'a eklememiz,tanıtmamız gerekiyor.
            //Dependency Injection implementasyonu yapmamız gerekiyor.

            //HangiServiceProviderFactory kullanılacak=Autofac =>Fabrika sisteme eklendi.
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            
            //Hangi Modulleri kullanacağımızı register ettik=Autofac Business Module
            .ConfigureContainer<ContainerBuilder>(builder=>
            {
                builder.RegisterModule(new AutofacBusinessModule());
            })


                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
