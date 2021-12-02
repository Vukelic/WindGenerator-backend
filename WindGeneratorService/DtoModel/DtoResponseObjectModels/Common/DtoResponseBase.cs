using DtoModel.DtoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModel.DtoResponseObjectModels.Common
{
    public class DtoResponseBase<Tdto>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string MessageDescription { get; set; }
        public DtoPaging PagingObject { get; set; }
        public Tdto Value { get; set; }
        public object FailedDetails { get; set; }

    }
}
