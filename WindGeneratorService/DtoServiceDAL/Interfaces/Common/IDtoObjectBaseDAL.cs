using DtoModel.DtoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoServiceDAL.Interfaces.Common
{
    public interface IDtoObjectBaseDAL<TIdType, TDto, TDtoResponse, TDtoListResponse>
    {
        TDtoResponse DeleteWholeTableContent();

        TDtoResponse Get(TIdType inId);
        TDtoListResponse GetList(DtoPaging inPaging = null);
        TDtoResponse Update(TDto inObject);
        TDtoResponse Create(TDto inObject);
        TDtoResponse Delete(TDto inObject);
        TDtoResponse Delete(TIdType inObjectId);

        TDtoResponse SoftDelete(TDto inObject);
        TDtoResponse SoftDelete(TIdType inObjectId);
    }
}
