using System;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete.Employee;
using Core.Entities.Concrete.User;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.UserDtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;//Kullanıcıyı kontrol etmemiz gerekiyor veri tabanında var mı ?

        IEmployeeService _employeeService;

        ITokenHelper _tokenHelper;//Kullanıcı Login olduğunda ona bir Token vermemiz gerekiyor.

        public AuthManager(IUserService userService, ITokenHelper usertokenHelper,IEmployeeService employeeService)
        {
            _userService = userService;
            _employeeService = employeeService;
            _tokenHelper = usertokenHelper;
        }


        //USER---------------------------------------------------------------------------------------------------------------------


        public IDataResult<AccessToken> CreateAccessTokenForUser(User user)
        {
            var claims = _userService.GetClaims(user);
            var token = _tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(token, Messages.AccessTokenCreated);
        }

        public IDataResult<User> UserLogin(LoginDto loginDto)//Öncelikle usertoCheck ile mevcut kullanıcıyı getiririz.
        {
            var userToCheck = _userService.GetByEmail(loginDto.Email);//Böyle bir kullanıcı var mı kontrolü

            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(userToCheck, Messages.UserNotFound);
            }
            //Eğer bu noktaya geliyorsa kullanıcı bulunmuş demektir.
            //"HASHELPER" yazılacak
            //Kullanıcıyı bulduk.Kullanıcının bize gönderdiği açık bir password var
            //Kullanıcının bize gönderdiği password u oradaki salt vasıtası ile hashleyip gerçekten veri tabanındaki hash ile kullanıcı datasının hash i aynı mı onu kontrol edicez
            //KISACASI KULLANICININN GÖNDERDİĞİ DATAYI(PASSWORD),HASH İLE KONTROL EDEN OPERASYONA İHTİYACIMIZ VAR
            //LOGİN E DESTEK VEREN BİR HELPER YAZICAZ.

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.passwordHash, userToCheck.passwordSalt))
            {
                return new ErrorDataResult<User>(userToCheck, Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfullLogin);

        }

        //[ValidationAspect(typeof(RegisterValidator),Priority =1)]

        public IDataResult<User> UserRegister(RegisterDto registerDto)
        {
            byte[] passwordHash, passwordSalt;//Boş Salt ve Hash değerleri gönderiyoruz

            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);//Out keyword leri sayesinde değer kazanıyor.

            var user = new User
            {
                email = registerDto.Email,
                firstName = registerDto.FirstName,
                lastName = registerDto.LastName,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                userStatus = true  //Bu değere "False" verip emailden doğrulama isteyip doğrulama gelince status u true yapan bir sistem yazılabilir.
                                   //Yada çalışan kayıt olup Yönetici onaylayınca çalışan bir sistem
            };
            _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByEmail(email) != null || _employeeService.GetByEmail(email) != null) 
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }


        //EMPLOYEE---------------------------------------------------------------------------------------------------------------------


        public IDataResult<Employee> EmployeeRegister(RegisterDto registerDto)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            var employee = new Employee
            {
                email = registerDto.Email,
                firstname = registerDto.FirstName,
                lastname = registerDto.LastName,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                employeeStatus = true,
            };
            _employeeService.Add(employee);

            var claim = new EmployeeOperationClaim
            {
                operationClaimId = 1
            };
            _employeeService.AddClaim(claim);

            return new SuccessDataResult<Employee>(employee, Messages.UserRegistered);
        }

        public IDataResult<Employee> EmployeeLogin(LoginDto loginDto)
        {
            var employeeToCheck = _employeeService.GetByEmail(loginDto.Email);

            if (employeeToCheck==null)
            {
                return new ErrorDataResult<Employee>(employeeToCheck, Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(loginDto.Password,employeeToCheck.passwordHash,employeeToCheck.passwordSalt))
            {
                return new ErrorDataResult<Employee>(employeeToCheck, Messages.PasswordError);
            }

            return new SuccessDataResult<Employee>(employeeToCheck, Messages.SuccessfullLogin);
        }

        public IDataResult<AccessToken> CreateAccessTokenForEmployee(Employee employee)
        {
            var claims=_employeeService.GetClaims(employee);
            var token=_tokenHelper.CreateTokenForEmployee(employee, claims);

            return new SuccessDataResult<AccessToken>(token, Messages.AccessTokenCreated);
        }
    }
}
