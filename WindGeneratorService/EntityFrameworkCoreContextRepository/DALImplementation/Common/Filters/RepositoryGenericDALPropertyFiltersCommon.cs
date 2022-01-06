using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoRequestObjectModels.Filters;
using RepositoryModel.RepoRequestObjectModels.Sort;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.Common.Filters
{
    public static class tmpExtension
    {
        public static string ToString(this object inThis)
        {
            return inThis?.ToString() ?? "";
        }
    }

    internal class RepositoryGenericDALPropertyFiltersCommon
    {
        internal static IQueryable<TDbObject> ApplyGlobalGeneralPropertyFilter<TDbObject>(
          ref IQueryable<TDbObject> inQueryable,
          RepositoryFilter itemF
          )
          where TDbObject : ARepoBaseEntity
        {
            try
            {
                Expression<Func<TDbObject, bool>> actualExpresion = null;
                if (itemF != null)
                {
                    var tmpInd = itemF.PropName.IndexOf("::-->>");
                    if (tmpInd >= 0)
                    {
                        itemF.PropName = itemF.PropName.Substring(0, tmpInd);
                    }
                    if (itemF.ApplyFilter)
                    {
                        switch (itemF.FilterType)
                        {
                            case EFilterTypes.Contain:

                                actualExpresion = GetDynamicQueryWithExpresionTrees_contains<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.EndWith:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_endWith<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.Equal:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_equal<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.StartWith:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_startWith<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.NotEqual:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_notEqual<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.LessThan:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_lessThen<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.GreaterThan:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_greaterThen<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.LessThanOrEqual:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_lessThanOrEqual<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                            case EFilterTypes.GreaterThanOrEqual:
                                actualExpresion = GetDynamicQueryWithExpresionTrees_greaterThanOrEqual<TDbObject>(itemF.PropName, itemF.FilterValue);
                                inQueryable = inQueryable.Where(actualExpresion);
                                break;
                        }
                    }
                    if (itemF.ApplySort)
                    {
                        switch (itemF.SortType)
                        {
                            case ESortTypes.Ascending:
                                inQueryable = GetDynamicQueryWithExpresionTrees_propertyForSort(inQueryable, itemF.PropName, ListSortDirection.Ascending);
                                break;
                            case ESortTypes.Descending:
                                inQueryable = GetDynamicQueryWithExpresionTrees_propertyForSort(inQueryable, itemF.PropName, ListSortDirection.Descending);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ApplyGlobalGeneralPropertyFilter. Details: {ex.Message}", ex);
            }
            return inQueryable;
        }


        private static IQueryable<TDbObject> GetDynamicQueryWithExpresionTrees_propertyForSort<TDbObject>(IQueryable<TDbObject> source, string propertyName, ListSortDirection inSortOrder)
        {

            var type = typeof(TDbObject);
            var parameter = Expression.Parameter(type, "p");
            var property = Expression.Property(parameter, propertyName); //type.GetTypeInfo().GetDeclaredProperty(propertyName);
            var propertyAccess = Expression.MakeMemberAccess(parameter, property.Member);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var typeArguments = new Type[] { type, property.Type };
            //var methodName = inSortOrder == ListSortDirection.Ascending ? "ThenBy" : "ThenByDescending";
            var methodName = inSortOrder == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression, Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<TDbObject>(resultExp);

            /*
            var type = typeof(TDbObject);
            var property = type.GetTypeInfo().GetDeclaredProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var typeArguments = new Type[] { type, property.PropertyType };
            var methodName = inSortOrder == ListSortDirection.Ascending ? "ThenBy" : "ThenByDescending";
            //var methodName = inSortOrder == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression, Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<TDbObject>(resultExp);
            */
        }

        //private static Func<TDbObject, bool> GetDynamicQueryWithExpresionTrees<TDbObject>(string propertyName, string val)
        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_equal<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
            #endregion
            Expression body = Expression.Equal(member, valueExpression);
            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_notEqual<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
            #endregion
            Expression body = Expression.NotEqual(member, valueExpression);
            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_startWith<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param, true);
            #endregion
            MethodInfo mi = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            var to_string_method = typeof(object).GetMethod("ToString");
            var member_as_string = Expression.Call(member, to_string_method);
            Expression body = Expression.Call(member, mi, valueExpression);

            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_lessThen<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
            #endregion

            Expression body = Expression.LessThan(member, valueExpression);
            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_greaterThen<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
            #endregion


            Expression body = Expression.GreaterThan(member, valueExpression);
            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_lessThanOrEqual<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
            #endregion


            Expression body = Expression.LessThanOrEqual(member, valueExpression);
            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_greaterThanOrEqual<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
            #endregion


            Expression body = Expression.GreaterThanOrEqual(member, valueExpression);
            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_endWith<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param, true);
            #endregion
            MethodInfo mi = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
            var to_string_method = typeof(object).GetMethod("ToString");
            var member_as_string = Expression.Call(member, to_string_method);
            Expression body = Expression.Call(member, mi, valueExpression);

            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static Expression<Func<TDbObject, bool>> GetDynamicQueryWithExpresionTrees_contains<TDbObject>(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(TDbObject), "x");

            #region Convert to specific data type
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param, true);
            #endregion
            MethodInfo mi = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            var to_string_method = typeof(object).GetMethod("ToString");
            var member_as_string = Expression.Call(member, to_string_method);
            Expression body = Expression.Call(member, mi, valueExpression);

            var final = Expression.Lambda<Func<TDbObject, bool>>(body: body, parameters: param);
            //return final.Compile();
            return final;
        }

        private static UnaryExpression GetValueExpression(string propertyName, string val, ParameterExpression param, bool inForceString = false)
        {
            //TODO
            bool convertToNull = false;

            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            if (inForceString)
            {
                propertyType = typeof(string);
            }
            else if (propertyName == "ParentAlarmGroupId" && val == "null" || propertyName == "ParentDashboardObjectSetId" && val == "null")
            {
                propertyType = typeof(Nullable<long>);
                convertToNull = true;
            }
            var converter = TypeDescriptor.GetConverter(propertyType);

            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();

            object propertyValue = null;
            if (convertToNull == false)
            {
                propertyValue = converter.ConvertFromInvariantString(val);
            }
            var constant = Expression.Constant(propertyValue);
            return Expression.Convert(constant, propertyType);
        }
    }
}
