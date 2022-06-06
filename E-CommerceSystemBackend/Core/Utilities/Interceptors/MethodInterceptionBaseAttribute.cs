using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    //Autofac Interceptor ile çalışıcaz, tüm aspectlerde kullanacağımız bir altyapı kurucaz.

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]//Attribute lerimize kısıt koyduk.

    public abstract class MethodInterceptionBaseAttribute:Attribute, IInterceptor//Attribute:Nitelik içeren abstract classlardır.
    {                                                                           //Uygulandıkları tiplerin çalışma zamanındaki davranışlarını değiştirir
        public int Priority { get; set; } //Çalışma sırası önceliği verdik.

        public virtual void Intercept(IInvocation invocation)
        {
        }
        //"Interceptors": Metot çağrımları sırasında araya girerek çakışan ilgilerimizi işletmemizi ve yönetmemizi sağlamakta.
        //Buda metotların çalışmasından önce veya sonra bir takım işlemleri gerçekleştirebilmemeizi sağlar.
    }
}
