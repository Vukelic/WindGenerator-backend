using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.RepoRequestObjectModels.Filters
{
    public enum EFilterTypes
    {
        Equal = 0,
        StartWith,
        Contain,
        EndWith,
        NotEqual,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual
    }
}
