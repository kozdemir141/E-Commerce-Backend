using System;
using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Employee;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        List<OperationClaim> GetClaims(Employee employee);

        Employee GetByEmail(string email);

        void Add(Employee employee);

        void AddClaim(EmployeeOperationClaim employeeOperationClaim);

        void Delete(Employee employee);

        void Update(Employee employee);
    }
}
