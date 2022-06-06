using System;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Employee
{
    public class EmployeeOperationClaim:IEntity
    {
        public int employeeOperationClaimId { get; set; }

        public int employeeId { get; set; }

        public int operationClaimId { get; set; }
    }
}
