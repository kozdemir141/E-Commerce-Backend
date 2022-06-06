using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //Öncelikle classAttribute bulmamız gerekiyor.
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            //Bizim Aspect imize konu olan Sınıf(ProductManager gibi).Bizim Aspect olayına konu olan nesnelerimizi Listeye çevirir.

            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            //Class ın içindeki hangi Method onu verdik.(Add,GetList.vs)

            classAttributes.AddRange(methodAttributes);//Product Manager.Add gibi=>Aspect hangi classda hangi Operasyonu çalıştıracağını artık biliyor

            return classAttributes.OrderBy(x => x.Priority).ToArray();//ethodInterceptionBaseAttribute de tanımlı Priorty niteliğini verdik artık sıralı çalıştırabiliriz belirttiğimiz koşulda.
        }
    }
}
