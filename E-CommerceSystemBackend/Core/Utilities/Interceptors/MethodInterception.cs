using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception:MethodInterceptionBaseAttribute
    {
        public virtual void OnBefore(IInvocation invocation) { } //Method Çalışmadan

        public virtual void OnAfter(IInvocation invocation) { } //Çalıtıktan Sonra

        public virtual void OnException(IInvocation invocation) { } //Hata Verdiğinde

        public virtual void OnSuccess(IInvocation invocation) { } //Başarılı olduğunda

        //Invocation=Bizim Çalıştırılmaya Çalışılan Operasyonumuz

        public override void Intercept(IInvocation invocation) //Methodu Nasıl Yorumlayacağımızı Anlattığımız Yer,Methodu Nasıl Ele Alıcaz
        {
            var isSuccess = true;

            OnBefore(invocation);

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                OnException(invocation);
                throw ex;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
