using System;
using Core.Entities.Concrete.Employee;
using Core.Entities.Concrete.User;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.UserDtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        //USER

        IDataResult<User> UserRegister(RegisterDto registerDto);

        IDataResult<User> UserLogin(LoginDto loginDto);

        IResult UserExists(string email);

        IDataResult<AccessToken> CreateAccessTokenForUser(User user);

        //EMPLOYEE

        IDataResult<Employee> EmployeeRegister(RegisterDto registerDto);

        IDataResult<Employee> EmployeeLogin(LoginDto loginDto);

        IDataResult<AccessToken> CreateAccessTokenForEmployee(Employee employee);


    }
}
