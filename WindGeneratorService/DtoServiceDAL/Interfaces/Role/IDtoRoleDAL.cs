using DtoModel.DtoModels.Implementations.Role;
using DtoModel.DtoResponseObjectModels.Role;
using DtoServiceDAL.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoServiceDAL.Interfaces.Role
{
    public interface IDtoRoleDAL : IDtoObjectBaseDAL<long, DtoRole, DtoRoleResponse, DtoRoleListResponse>
    {
    }
}
