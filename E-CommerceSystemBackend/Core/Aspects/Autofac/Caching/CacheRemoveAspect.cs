using System;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect:MethodInterception
    {
        string _pattern;

        ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
        //CacheRemoveAspect ne zaman çalıştırılacak=> Yeni ürün eklendiğinde,Ürün güncellendiğinde Cache in düzeltilmesi gerekiyor.
        //Bunu OnSuccess ile yapıcaz Çünkü;Mevcut operasyonun(Add,Update) önce tamamlanması gerekiyor.
    }
}
