using System;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        int _duration;

        ICacheManager _cacheManager;

        public CacheAspect(int duration=60) //Default değer girdik,değer girilmezse default olarak çalışır
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
                                                              //Burada şunu gerçekleştirmemiz gerekiyor
        public override void Intercept(IInvocation invocation)//Önce kontrol et,Cache de var mı? Yoksa Cache ekle,Varsa Cache'den getir
                                                              //Burada Parametrede belirttiğimiz (string key) değeri Method bilgimiz olucak
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //ProductManager.GetListByCategory (Key değer başlangıcı)
                           //String formatında        //Class,newlenebilir= ReflectedType

            var arguments = invocation.Arguments.ToList(); //(int categoryId); 

            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";//Her bir parametre için eğer parametre varsa onu string e çevir
                                                                                                            //Dinamik yapıda                                                     //Aksi takdirde parametre yokmuş gibi Null döndür


            if (_cacheManager.IsAdd(key))//Bu key değeri daha önce eklemişse
            {
                invocation.ReturnValue = _cacheManager.Get(key); //Bu methodun ReturnValuse si Cache key değeridir.
                return;
            }

            invocation.Proceed();                                      //invocation(method) u çalıştır
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //Cache e key değerini ekle
                                 //Methodun Return Valuesi(Son değeri)

        }
    }
}
