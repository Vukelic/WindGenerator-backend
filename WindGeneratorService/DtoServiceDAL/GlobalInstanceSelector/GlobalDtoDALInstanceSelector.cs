using DtoServiceDAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoServiceDAL.GlobalInstanceSelector
{
    public class GlobalDtoDALInstanceSelector
    {
        public static Func<ADtoDAL> GetDtoDALImplementation;
    }
}
