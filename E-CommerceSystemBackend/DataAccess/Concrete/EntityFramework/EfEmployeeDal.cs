using System;
using System.Collections.Generic;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Employee;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeDal : EfEntityRepositoryBase<Employee, NorthwindContext>, IEmployeeDal
    {
        public void AddClaim(EmployeeOperationClaim employeeOperationClaim)
        {
            using (var context=new NorthwindContext())
            {
                var addedClaim = context.Entry(employeeOperationClaim);
                addedClaim.State= EntityState.Added;

                context.SaveChanges();
            }
        }

        public List<OperationClaim> GetClaims(Employee employee)
        {
            using (var context=new NorthwindContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join employeeOperationClaim in context.EmployeeOperationClaims

                             on operationClaim.id equals employeeOperationClaim.operationClaimId
                             //where employeeOperationClaim.employeeId == employee.employeeId

                             select new OperationClaim { id = operationClaim.id, name = operationClaim.name };

                return result.ToList();
            }
        }
    }
}
