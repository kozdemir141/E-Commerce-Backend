using System;
using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module //Autofac bize IOC Container altyapısı sunuyor.
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>(); //IProductService istenirse ProductManager ı ver.
            builder.RegisterType<EfProductDal>().As<IProductDal>();       //IProductDal istenirse EfProductDal ı ver.

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<EmployeeManager>().As<IEmployeeService>();
            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            //Aspect in devreye girmesi için ne yapılacak?
            //Autofac kullanıyoruz,Autofac bir IOS Container,IOS Container üzerinden çalıştırıcaz.
            //Bizim bütün operasyonlarımız Autofac üzerinden çalışıyor.Burdaki Dependsy çözülüyor ve ona göre Operasyon çağırılıyor.
            //Interception Aspectlerimin OnBefore,After,Exception,Success şeklinde çalışmasını istiyorum.Bunun için Konfigurasyon yapmam gerekiyor.

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); //Mevcut Assembly'e ulaştım

            //Gönderilen assembly deki tüm tipleri kaydet
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions() //ProxyGenerationsOptions bir nesnedir özelliklerini vermen gerekiyor
            //Araya Girecek olan nesneyi söyle demek            
            {
                Selector = new AspectInterceptorSelector() //Kendi oluşturduğumuz bir nesne yazdık,Hangi interceptor çalışacak onu yazıyor olucaz.

            }).SingleInstance();//Tek bir instance oluştursun her seferinde bir sürü Aspect oluşturmasın
        }

    }
}
