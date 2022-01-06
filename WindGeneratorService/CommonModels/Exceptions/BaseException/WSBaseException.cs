using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels.Exceptions.BaseException
{
    public class WSBaseException : Exception
    {
        public WSBaseException()
        {

        }


        public WSBaseException(string message)
            : base(String.Format("Total safety error. {0}", message))
        {

        }
    }
}
