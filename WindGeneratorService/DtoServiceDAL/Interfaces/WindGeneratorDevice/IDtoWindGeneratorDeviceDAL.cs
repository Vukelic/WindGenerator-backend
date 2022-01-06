using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice;
using DtoServiceDAL.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoServiceDAL.Interfaces.WindGeneratorDevice
{
    public interface IDtoWindGeneratorDeviceDAL : IDtoObjectBaseDAL<long, DtoWindGeneratorDevice, DtoWindGeneratorDeviceResponse, DtoWindGeneratorDeviceListResponse>
    {
    }
}
