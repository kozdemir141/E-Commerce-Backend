using System;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete.User
{
    public class User:IEntity
    {
        public int userId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public bool userStatus { get; set; }

        public byte[] passwordSalt { get; set; }

        public byte[] passwordHash { get; set; }
    }
}
