using RepositoryModel.RepoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.RepoResponseObjectModels.Common
{
    public class RepositoryResponseBase<Trepository>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string MessageDescription { get; set; }
        public RepositoryPaging PaggingObject { get; set; }
        public Trepository Value { get; set; }

        public object FailedDetails { get; set; }
    }
}
