﻿using System;
using System.Collections.Generic;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.User;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u => u.email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
    }
}
