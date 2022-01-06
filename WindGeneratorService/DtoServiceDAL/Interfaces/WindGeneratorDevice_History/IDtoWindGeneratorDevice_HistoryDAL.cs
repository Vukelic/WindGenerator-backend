using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice_History;
using DtoServiceDAL.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoServiceDAL.Interfaces.WindGeneratorDevice_History
{
    public interface IDtoWindGeneratorDevice_HistoryDAL : IDtoObjectBaseDAL<long, DtoWindGeneratorDevice_History, DtoWindGeneratorDevice_HistoryResponse, DtoWindGeneratorDevice_HistoryListResponse>
    {
    }
}
