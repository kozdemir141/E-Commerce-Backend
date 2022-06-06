using System;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation:MethodInterception //Authorization Projeye Dayalı bir CrossCuttingConcern dür
    {                                                //Veri tabanına bağlı çalışacağımız için Business Katmanına yazdık
        string[] _roles;                             //Veri tabanına bağlı kalınmayacak durumlarda Core tarafına yazılabilir.
                                                     //Authorization un yerini İş İhtiyaçları belirler.
        IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)//[]roles =>string roles e çevirdik Aspect in Const. Array değil string geliyor
        {
            _roles = roles.Split(','); //Bunları virgülle ayır ve []_roles Array ına at.
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();//Rollere sahibim
            //Şimdi bu roleClaimlerimin içinde kullanıcının talep ettiği roller var mı foreach döngüsi ile bakıcam.
            foreach (var role in _roles)
            {
                if (!roleClaims.Contains(role))
                {
                    throw new Exception(Messages.AuthorizationDenied);
                }
            }
            return;
        }
    }
}
