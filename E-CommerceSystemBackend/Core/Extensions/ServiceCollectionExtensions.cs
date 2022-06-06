using System;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,ICoreModule[]modules)
        //AddDependencyResolvers vasıtası ile .netCore API tarafına bütün merkezi Configuration larımızı yüklüyor olucaz.
        {
            foreach (var module in modules)
            {
                module.Load(services); 
            }
            return ServiceTool.Create(services); //Tüm Modullerimi (Cache,Log) .netCore a eklemiş olucam
        }
    }
}
