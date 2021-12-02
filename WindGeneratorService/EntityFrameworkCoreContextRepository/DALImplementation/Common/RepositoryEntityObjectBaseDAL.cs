using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Common.Filters;
using RepoServiceDAL.Interfaces.Common;
using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoRequestObjectModels.Paging;
using RepositoryModel.RepoResponseObjectModels.Common;
using RepositoryModel.RepoResponseObjectModels.Validations;
using RepositoryModel.RepoResponseObjectModels.Validations.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.Common
{
    public class RepositoryEntityObjectBaseDAL<Tdb> : IRepositoryObjectBaseDAL<long, Tdb> where Tdb : ARepoBaseEntity
    {
        public RepositoryEntityObjectBaseDAL(WindServiceMainDbContext inDb)
        {
            if (inDb == null)
            {
                throw new ArgumentNullException("inDb", "Error creating implementation of Entity framework Repository Data Access Layer. Database context not exist.");
            }
            db = inDb;
        }

        public WindServiceMainDbContext db { get; set; }

        #region Only for test purpose
        public virtual RepositoryResponseBase<Tdb> DeleteWholeTableContent()
        {
            RepositoryResponseBase<Tdb> toRet = new RepositoryResponseBase<Tdb>();
            try
            {
                db.Set<Tdb>().RemoveRange(db.Set<Tdb>());
                db.SaveChanges();
                toRet.Success = true;
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
            }

            return toRet;
        }
        #endregion

        public virtual RepositoryResponseBase<Tdb> Create(Tdb inObject)
        {

            RepositoryResponseBase<Tdb> toRet = new RepositoryResponseBase<Tdb>();
            try
            {
                List<ValidationResult> valResult = new List<ValidationResult>();
                bool isValid = GenericValidator.Validate(inObject, valResult);
                if (isValid)
                {
                    Type tmpType = inObject.GetType();
                    db.Set<Tdb>().Add(inObject);
                    db.SaveChanges();
                    toRet.Success = true;
                    toRet.Value = inObject;
                }
                else
                {
                    toRet.Success = false;
                    toRet.Message = "Not all properties are correct.";
                    toRet.MessageDescription = ValidarionResultConverter.GetMessageDescription(valResult);
                    toRet.Value = inObject;
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = inObject;
            }
            return toRet;

        }

        public virtual RepositoryResponseBase<Tdb> Delete(Tdb inObject)
        {
            return this.Delete(inObject?.Id ?? 0);
        }

        public virtual RepositoryResponseBase<Tdb> Delete(long inId)
        {
            RepositoryResponseBase<Tdb> toRet = new RepositoryResponseBase<Tdb>();
            Tdb dbObj = null;
            try
            {
                dbObj = db.Set<Tdb>().Find(inId);
                if (dbObj == null)
                {
                    throw new Exception(string.Format("Delete object error. Object with id: {0}, not found in database.", inId));
                }

                db.Set<Tdb>().Remove(dbObj);
                db.SaveChanges();

                toRet.Success = true;
                toRet.Value = dbObj;
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = dbObj;
            }

            return toRet;
        }


        public virtual RepositoryResponseBase<Tdb> SoftDelete(Tdb inObject)
        {
            return this.SoftDelete(inObject?.Id ?? 0);
        }

        public virtual RepositoryResponseBase<Tdb> SoftDelete(long inId)
        {
            RepositoryResponseBase<Tdb> toRet = new RepositoryResponseBase<Tdb>();
            Tdb dbObj = null;
            try
            {
                dbObj = db.Set<Tdb>().Find(inId);
                if (dbObj == null)
                {
                    throw new Exception(string.Format("Soft Delete object error. Object with id: {0}, not found in database.", inId));
                }

                dbObj.SoftDeleted = true;
                db.SaveChanges();

                toRet.Success = true;
                toRet.Value = dbObj;
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = dbObj;
            }

            return toRet;
        }

        public virtual RepositoryResponseBase<Tdb> Get(long inId)
        {
            RepositoryResponseBase<Tdb> toRet = new RepositoryResponseBase<Tdb>();
            Tdb dbObj = null;
            try
            {
                dbObj = db.Set<Tdb>().Find(inId);
                if (dbObj == null)
                {
                    throw new Exception(string.Format("Object with id: {0}, not found in database.", inId));
                }
                RepositoryCommonHelperFunctions.CustomGetInclude(dbObj, db);

                toRet.Success = true;
                toRet.Value = dbObj;
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = dbObj;
            }

            return toRet;
        }

        public virtual RepositoryResponseBase<IEnumerable<Tdb>> GetList(RepositoryPaging inRepoPaging)
        {
            RepositoryResponseBase<IEnumerable<Tdb>> toRet = new RepositoryResponseBase<IEnumerable<Tdb>>();
            List<Tdb> currentPageList = null;
            try
            {
                if (inRepoPaging == null)
                {
                    inRepoPaging = new RepositoryPaging();
                }
                if (inRepoPaging.RepositoryFilters == null || inRepoPaging.RepositoryFilters.Count == 0 || !inRepoPaging.RepositoryFilters.Any(o => o.ApplySort))
                {// Seting default sorting by id, if noting defined. This is only because of pagging process. When we use pagging (taking top(xxx)) we must define at least one sorting.
                    if (inRepoPaging.RepositoryFilters == null)
                    {
                        inRepoPaging.RepositoryFilters = new List<RepositoryModel.RepoRequestObjectModels.Filters.RepositoryFilter>();
                    }
                    inRepoPaging.RepositoryFilters.Add(
                        new RepositoryModel.RepoRequestObjectModels.Filters.RepositoryFilter()
                        {
                            //PropName = "Id",
                            //ApplySort = true,
                            //SortType = RepositoryModel.RepoRequestObjectModels.Sort.ESortTypes.Ascending

                            //PropName = "Name",
                            //ApplyFilter = true,
                            //FilterType = RepositoryModel.RepoRequestObjectModels.Filters.EFilterTypes.Equal,
                            //FilterValue = "Zeljko",

                            //PropName = "Name",
                            //ApplyFilter = true,
                            //FilterType = RepositoryModel.RepoRequestObjectModels.Filters.EFilterTypes.StartWith,
                            //FilterValue = "ma",

                            //PropName = "Id",
                            //ApplyFilter = true,
                            //FilterType = RepositoryModel.RepoRequestObjectModels.Filters.EFilterTypes.Equal,
                            //FilterValue = "2",

                            //PropName = "Id",
                            //ApplyFilter = true,
                            //FilterType = RepositoryModel.RepoRequestObjectModels.Filters.EFilterTypes.StartWith,
                            //FilterValue = "1",

                            PropName = "Id",
                            ApplySort = true,
                            SortType = RepositoryModel.RepoRequestObjectModels.Sort.ESortTypes.Descending
                        }
                    );
                }
                if (!inRepoPaging.RepositoryFilters.Any(o => o.PropName == "SoftDeleted"))
                {
                    inRepoPaging.RepositoryFilters.Add(new RepositoryModel.RepoRequestObjectModels.Filters.RepositoryFilter()
                    {
                        PropName = "SoftDeleted",
                        ApplyFilter = true,
                        FilterType = RepositoryModel.RepoRequestObjectModels.Filters.EFilterTypes.Equal,
                        FilterValue = "false"
                    });
                }



                IQueryable<Tdb> entityQueryable = db.Set<Tdb>().AsQueryable();
                foreach (var itemF in inRepoPaging.RepositoryFilters)
                {
                    if (itemF != null)
                    {

                        entityQueryable = RepositoryGenericDALPropertyFiltersCommon.ApplyGlobalGeneralPropertyFilter(ref entityQueryable, itemF);
                        //entityQueryable = entityQueryable.OrderBy(o => o.Id);

                    }
                }

                inRepoPaging.totalCount = entityQueryable.Count();
                if (inRepoPaging.countPerPage == 0)
                {
                    inRepoPaging.countPerPage = 100;
                }
                inRepoPaging.totalPages = (int)Math.Ceiling((decimal)inRepoPaging.totalCount / inRepoPaging.countPerPage);

                int tmpZeroBased = (inRepoPaging.page - 1);
                if (tmpZeroBased < 0)
                    tmpZeroBased = 0;

                currentPageList = entityQueryable.Skip(tmpZeroBased * inRepoPaging.countPerPage).Take(inRepoPaging.countPerPage).ToList();
                if (currentPageList == null)
                {
                    throw new Exception("Error pagging object in database.");
                }

                List<ARepoBaseEntity> tmpList = currentPageList.Cast<ARepoBaseEntity>().ToList();
                RepositoryCommonHelperFunctions.CustomGetInclude(tmpList, db);

                toRet.Success = true;
                toRet.PaggingObject = inRepoPaging;
                toRet.Value = currentPageList;
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = currentPageList;
            }

            return toRet;
        }
        public virtual bool ObjectExist(long inId)
        {
            return db.Set<Tdb>().Any(o => o.Id == inId);
        }

        public virtual RepositoryResponseBase<Tdb> Update(Tdb inObject)
        {
            RepositoryResponseBase<Tdb> toRet = new RepositoryResponseBase<Tdb>();
            Tdb dbObject = null;
            try
            {
                long inId = inObject?.Id ?? 0;
                dbObject = db.Set<Tdb>()
                    .FirstOrDefault(o => o.Id == inId);
                if (dbObject == null)
                {
                    throw new Exception(string.Format("Update object error. Object with id: {0}, not found in database.", inId));
                }

                List<ValidationResult> valResult = new List<ValidationResult>();
                bool isValid = GenericValidator.Validate(inObject, valResult);
                if (isValid)
                {
                    db.Entry(dbObject).CurrentValues.SetValues(inObject);
                }
                else
                {
                    toRet.Success = false;
                    toRet.Message = "Not all properties are correct.";
                    toRet.MessageDescription = ValidarionResultConverter.GetMessageDescription(valResult);
                    toRet.Value = inObject;
                    return toRet;
                }

                db.SaveChanges();

                toRet.Success = true;
                toRet.Value = dbObject;
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = dbObject;
            }

            return toRet;
        }
    }
}
