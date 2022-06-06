using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public interface ICoreModule //AYNI MANTIK == AutofacBusinessModule:Module => Module:IModule 
    {
        void Load(IServiceCollection services); //AutofacBusinessModule da Load Methodu altında Configurationlar yaptık.
    }                                           //Aynı mantık ile Core katmanında merkezleştirerek Cache,Log gibi işlemler için Configuration lar yapıcaz
}                                               //.net core IServiceColleciton un ne olduğunu biliyor ona göre Enjection yapacaktır.
