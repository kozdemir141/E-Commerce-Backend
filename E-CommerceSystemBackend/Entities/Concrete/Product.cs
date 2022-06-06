using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Product:IEntity 
    {                   //İmzalama tekniğidir Product'ın veri tabanı nesnesi olduğunu belirtir.
        public int productId { get; set; }

        public string productName { get; set; }

        public int categoryId { get; set; }

        public string quantityPerUnit { get; set; }

        public decimal unitPrice { get; set; }

        public short unitsInStock { get; set; }

        public bool IsDeleted { get; set; }
    }
}
