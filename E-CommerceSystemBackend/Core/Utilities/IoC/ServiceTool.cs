using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static ServiceProvider ServiceProvider { get; set; } //Biz uygulamamız da Service Collection nesnesine onunda arkasında ServiceProvider nesnesine erişmeye çalışıyor olucaz.
        
        public static IServiceCollection Create(IServiceCollection services) //Service Tool vasıtası ile .net core un kendi servislerine ulaşabiliyorum
        {                                                                    //Startup daki yapıyı(Sistem servislerini) Tool vasıtası ile merkezi bir noktadan kontrol ediyoruz.
            ServiceProvider = services.BuildServiceProvider();               //Bize IOS Container görücek bir yapıdır.
                                     //.net core un kendi ServiceCollection yapısını injekte ediyor.
            return services;
        }
    }
}
