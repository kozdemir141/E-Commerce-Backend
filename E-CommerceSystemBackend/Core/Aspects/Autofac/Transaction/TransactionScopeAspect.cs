using System;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect:MethodInterception
    {
        public override void Intercept(IInvocation invocation)//Tekrar Hatırlatma Invocation Çalıştırılmaya çalışılan Method.
        {
            using (TransactionScope transactionScope = new TransactionScope()) //Transasction Yapısının Yaşam Döngüsünün Yönetilmesi Gerekiyor.
            {
                try
                {
                    invocation.Proceed();         //Aspect e konu olan Methodu çalıştırmaya çalış.
                    transactionScope.Complete();  //Hata vermiyorsa Complete et
                }
                catch (Exception)
                {
                    transactionScope.Dispose();   //Hata veriyorsa yapılan tüm işlemleri geri al.
                    throw;
                }
            }
        }
    }
}
