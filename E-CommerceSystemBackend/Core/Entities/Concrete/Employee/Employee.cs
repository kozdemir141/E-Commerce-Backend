using System;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Employee
{
    public class Employee:IEntity
    {
        public int employeeId { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string email { get; set; }

        public bool employeeStatus { get; set; }

        public byte[] passwordSalt { get; set; }

        public byte[] passwordHash { get; set; }
    }
}
