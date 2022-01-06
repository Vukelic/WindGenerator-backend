using RepositoryModel.RepoRequestObjectModels.Paging;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoServiceDAL.Interfaces.Common
{
    public interface IRepositoryObjectBaseDAL<TIdType, TRepo>
    {
        RepositoryResponseBase<TRepo> DeleteWholeTableContent();

        bool ObjectExist(TIdType inId);
        RepositoryResponseBase<TRepo> Get(TIdType inId);
        RepositoryResponseBase<IEnumerable<TRepo>> GetList(RepositoryPaging inRepoPaging);
        RepositoryResponseBase<TRepo> Update(TRepo inObject);
        RepositoryResponseBase<TRepo> Create(TRepo inObject);
        RepositoryResponseBase<TRepo> Delete(TRepo inObject);
        RepositoryResponseBase<TRepo> Delete(TIdType inId);

        RepositoryResponseBase<TRepo> SoftDelete(TRepo inObject);
        RepositoryResponseBase<TRepo> SoftDelete(TIdType inId);
    }
}
