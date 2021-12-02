using DtoServiceDAL.Interfaces.Role;
using DtoServiceDAL.Interfaces.User;
using DtoServiceDAL.Interfaces.WindGeneratorDevice;
using DtoServiceDAL.Interfaces.WindGeneratorDevice_History;
using System;

namespace DtoServiceDAL
{
    public interface IDtoDAL
    {
        IDtoRoleDAL GetRoleDAL();
        IDtoUserDAL GetUserDAL();
        IDtoWindGeneratorDeviceDAL GetWindGeneratorDeviceDAL();
        IDtoWindGeneratorDevice_HistoryDAL GetWindGeneratorDevice_HistoryDAL();
    }
}
