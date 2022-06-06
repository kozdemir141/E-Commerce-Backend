using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Employee;

namespace DataAccess.Abstract
{
    public interface IEmployeeDal:IEntityRepository<Employee>
    {
        List<OperationClaim> GetClaims(Employee employee);

        void AddClaim(EmployeeOperationClaim employeeOperationClaim);
    }
}
