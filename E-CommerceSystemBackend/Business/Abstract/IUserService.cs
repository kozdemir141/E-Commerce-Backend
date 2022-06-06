using System;
using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Entities.Concrete.User;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);

        User GetByEmail(string email);

        void Add(User user);

        void Delete(User user);

        void Update(User user);
    }
}
