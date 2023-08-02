using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Common;
using System.Net;

namespace Application.Specifications
{
    public class UserSpecification
    {
        public class UserListSpecification : BaseSpecification<User>
        {
            public UserListSpecification(int? page, int? size, string? orderBy, string? orderByDes) : base(take: size, pageNumber: page, orderBy: orderBy, orderByDesc: orderByDes)
            {


            }
        }

        public class UserSearchSpecification : BaseSpecification<User>
        {
            public UserSearchSpecification(string? name, string? userName) : base(x =>
                x.Name == name || x.UserName == userName)
            {

            }
        }

        public class UserCredentialsSpecification : BaseSpecification<UserCredentialModel>
        {
            public UserCredentialsSpecification(string userName, string password) : base(x =>
                x.User!.UserName == userName && x.Password == password)
            {

            }
        }

    }
}
