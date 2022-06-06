using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.UserDtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]

        public ActionResult UserLogin(LoginDto loginDto)
        {
            if (loginDto.Email.Contains("@trendyol.com"))
            {
                var employeeToLogin = _authService.EmployeeLogin(loginDto);

                if (!employeeToLogin.Success)
                {
                    return BadRequest(employeeToLogin.Message);
                }

                var employeeResult = _authService.CreateAccessTokenForEmployee(employeeToLogin.Data);

                if (employeeResult.Success)
                {
                    return Ok(employeeResult.Data);
                }
                return BadRequest(employeeResult.Message);

            }

            var userToLogin=_authService.UserLogin(loginDto);

            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessTokenForUser(userToLogin.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("register")]

        public ActionResult UserRegister(RegisterDto registerDto)
        {
            var userExists = _authService.UserExists(registerDto.Email);

            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            if (registerDto.Email.Contains("@trendyol.com"))
            {
                var employeeRegisterResult = _authService.EmployeeRegister(registerDto);

                var employeeResult = _authService.CreateAccessTokenForEmployee(employeeRegisterResult.Data);

                if (employeeResult.Success)
                {
                    return Ok(employeeResult.Data);
                }
                return BadRequest(employeeResult.Message);
            }

            var registerResult = _authService.UserRegister(registerDto);

            var result = _authService.CreateAccessTokenForUser(registerResult.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
