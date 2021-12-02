using CommonModels.Exceptions.BaseException;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels.Exceptions
{
    public class UserAccountLockException : WSBaseException
    {
        public UserAccountLockException()
        {

        }


        public UserAccountLockException(string message)
            : base(String.Format("User account is locked. {0}", message))
        {

        }
    }
}
