using System;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {                                              //Startup da yapacağım işlemleri merkezleştirdim. 
            services.AddMemoryCache();                 //Startupda yapacağım merkezi services işlemlerini (Cache,Log) Buraya ekleyebilirim

            services.AddSingleton<ICacheManager, MemoryCacheManager>();//Autofac de yaptığımız işlem Birisi ICacheManager isterse MemoryCacheManager ı ver.

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
