using DtoModel.DtoModels.Implementations.WindGeneratorType;
using DtoModel.DtoResponseObjectModels.WindGeneratorType;
using DtoServiceDAL.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoServiceDAL.Interfaces.WindGeneratorType
{
    public interface IDtoWindGeneratorTypeDAL : IDtoObjectBaseDAL<long, DtoWindGeneratorType, DtoWindGeneratorTypeResponse, DtoWindGeneratorTypeListResponse>
    {
    }
}
