using System;
using System.Collections.Generic;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Employee;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class EmployeeManager:IEmployeeService
    {
        IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public void Add(Employee employee)
        {
            _employeeDal.Add(employee);
        }

        public void AddClaim(EmployeeOperationClaim employeeOperationClaim)
        {
            _employeeDal.AddClaim(employeeOperationClaim);
        }

        public void Delete(Employee employee)
        {
            _employeeDal.Delete(employee);
        }

        public Employee GetByEmail(string email)
        {
            return _employeeDal.Get(e => e.email == email);
        }

        public List<OperationClaim> GetClaims(Employee employee)
        {
            return _employeeDal.GetClaims(employee);
        }

        public void Update(Employee employee)
        {
            _employeeDal.Update(employee);
        }
    }
}
