using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModel.DtoModels.Implementations.DtoChangeUserPassword
{
    public class DtoChangeUserPassword
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public long UserId { get; set; }
    }
}
