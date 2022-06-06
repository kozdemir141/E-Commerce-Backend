using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMemoryCache();

            services.AddControllers();
            //Cors=Cross Origin Reques Sharing
            //Bizim apimize izin verdiğimiz yerin dışında bir yerden istek geldiğinde,hem buradan hem tarayıcıdan,bununla ilgili bir güvenlik tehdidi algılandığından dolayı izin verilmez

            //Servisimize Bir Cors Ekleyip Konfigurasyon yapıcaz.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:3000"));//localhost:3000 den talep gelirse izin ver demek.
                                                                                                          //localhost:3000 React uygulamasının yayın adresidir.
                //Şuan localhost test ortamındayız,normal koşullarda domain neyse onu atarız.
                //WithOrigins=İstek yapılan yer demek
            });

            var tokenoptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //app.Settings deki TokenOptions bilgilerini Core daki TokenOptions a bağladım.

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => //Konfigurasyon bilgilerini veriyorum
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//Isser bilgisini validate ediyim mi;Kullanıcıya Token verdiğimiz zaman Isser olarak
                                          //app.Settings deki Issuer bilgisini veriyor olucaz,O bilginin geri geliyor olması gerekiyor,Bunun olması gerekiyor Token da

                    ValidateAudience = true,
                    ValidateLifetime = true, //Token olması yeterli,Token ın ömrü dolmuş olsa bile
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = tokenoptions.Issuer,
                    ValidAudience = tokenoptions.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenoptions.SecurityKey)
                    
                };
            });
            services.AddDependencyResolvers(new ICoreModule[] //.netCore tarafındaki framework seviyesinde uygulayabileceğimiz bütün merkezi servis injection,
                {                                             //injection Configuration larını buraya yazmış olduk.
                    new CoreModule()
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //"MIDDLEWAIRE" LERİN SIRASI ÇOK ÖNEMLİDİR
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader()); //Buradan gelen her türlü talebe cevap ver demek(Header,get,post,put,delete gibi http istekleridir.) 
                                                                                                   //AllowAnyHeader ile tüm bu isteklere cevap vermiş olduk.
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //JWT için bir Authentication servisininde sisteme eklenmesi gerekiyor.

            //Aynı zamanda hem Authorization hem Authentication middle waire lerinin sisteme eklenmesi lazım ki talep geldiğinde talebi yoğuralım.
            //Authentication:Bir yere girmek için anahtar
            //Authorization:Yetki,Anahtar ile girdiğimiz yerde ne yapılabilir durumu

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
