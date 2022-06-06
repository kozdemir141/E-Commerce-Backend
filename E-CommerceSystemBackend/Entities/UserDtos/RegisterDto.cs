using System;
using Core.Entities.Abstract;

namespace Entities.UserDtos
{
    public class RegisterDto:IDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
