using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Category:IEntity
    {
        public int categoryId { get; set; }

        public string categoryName { get; set; }
    }
}
