using System;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete.User
{
    public class UserOperationClaim:IEntity
    {
        public int userOperationClaimId { get; set; }

        public int userId { get; set; }

        public int operationClaimId { get; set; }
    }
}
