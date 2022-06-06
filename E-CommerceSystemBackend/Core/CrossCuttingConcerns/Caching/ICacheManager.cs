using System;
namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        //Standart Cache Operasyonları

        //T Get<T>(string key); //Key değerini verip Cache değerini hangi tipte olmasını istiyorusam okucam.

        object Get(string key); //Key değerini verip Cache değerini object(nesne) olarak kullanabilirim.

        void Add(string key, object data, int duration);

        bool IsAdd(string key); //Eklenmiş mi =>Cache den mi getireyim yoksa veri tabanından getirip Cache mi ekliyeyim.

        void Remove(string key);

        void RemoveByPattern(string pattern); //Pattern verip string pattern e uyanları silinmesi
    }
}
