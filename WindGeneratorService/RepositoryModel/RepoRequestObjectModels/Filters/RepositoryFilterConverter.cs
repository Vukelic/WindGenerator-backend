using RepositoryModel.RepoRequestObjectModels.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryModel.RepoRequestObjectModels.Filters
{
    public static class RepositoryFilterConverter
    {
        public static EFilterTypes defaultFilterType = EFilterTypes.StartWith;
        public static ESortTypes defaultSortType = ESortTypes.Ascending;

        public static Dictionary<string, EFilterTypes> Text2FilterTypeMapper = new Dictionary<string, EFilterTypes>()
        {
            { "sw", EFilterTypes.StartWith },
            { "co", EFilterTypes.Contain },
            { "ew", EFilterTypes.EndWith },
            { "eq", EFilterTypes.Equal },
            { "neq", EFilterTypes.NotEqual },
            { "lt", EFilterTypes.LessThan },
            { "gt", EFilterTypes.GreaterThan },
            { "ltoe", EFilterTypes.LessThanOrEqual },
            { "gtoe", EFilterTypes.GreaterThanOrEqual },
        };

        public static Dictionary<string, ESortTypes> Text2FilterSortTypeMapper = new Dictionary<string, ESortTypes>()
        {
            { "as", ESortTypes.Ascending },
            { "de", ESortTypes.Descending }
        };

        internal static List<RepositoryFilter> ConvertToRepositoryFilters(Dictionary<string, string> filters, Dictionary<string, string> filtersType, Dictionary<string, string> orders)
        {
            List<RepositoryFilter> toRet = new List<RepositoryFilter>();
            try
            {
                int numberOfFilters = filters?.Count ?? 0;
                int numberOfFiltersType = filtersType?.Count ?? 0;
                int numberOfOrders = orders?.Count ?? 0;

                if (numberOfFilters > 0)
                {
                    foreach (var item in filters)
                    {
                        if (item.Key != null && item.Value != null)
                        {
                            toRet.AddOrUpdateFilter_ByFilterValue(item.Key, item.Value);
                        }
                    }
                }
                if (numberOfFiltersType > 0)
                {
                    foreach (var item in filtersType)
                    {
                        if (item.Key != null && item.Value != null)
                        {
                            string tmpLowerFilterName = item.Value.ToLower().Trim();
                            if (RepositoryFilterConverter.Text2FilterTypeMapper.ContainsKey(tmpLowerFilterName))
                            {
                                var foundFilter = RepositoryFilterConverter.Text2FilterTypeMapper[tmpLowerFilterName];
                                toRet.AddOrUpdateFilter_ByFilterType(item.Key, foundFilter);
                            }

                        }
                    }
                }
                if (numberOfOrders > 0)
                {
                    foreach (var item in orders)
                    {
                        if (item.Key != null && item.Value != null)
                        {
                            string tmpLowerFilterName = item.Value.ToLower().Trim();
                            if (RepositoryFilterConverter.Text2FilterSortTypeMapper.ContainsKey(tmpLowerFilterName))
                            {
                                var foundFilterSort = RepositoryFilterConverter.Text2FilterSortTypeMapper[tmpLowerFilterName];
                                toRet.AddOrUpdateFilter_ByFilterSort(item.Key, foundFilterSort);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return toRet;
        }

        internal static void AddOrUpdateFilter_ByFilterValue(this List<RepositoryFilter> inThis, string inPropName, string inFilterValue)
        {
            if (inThis == null)
            {
                inThis = new List<RepositoryFilter>();
            }
            var existingObject = inThis.FirstOrDefault(o => o.PropName.Equals(inPropName, StringComparison.InvariantCultureIgnoreCase));
            if (existingObject == null)
            {
                existingObject = new RepositoryFilter()
                {
                    ApplyFilter = true,
                    FilterType = RepositoryFilterConverter.defaultFilterType,
                    FilterValue = inFilterValue,
                    PropName = inPropName,
                    ApplySort = false,
                    SortType = RepositoryFilterConverter.defaultSortType
                };
                inThis.Add(existingObject);
            }
            else
            {
                existingObject.ApplyFilter = true;
                existingObject.FilterValue = inFilterValue;
            }
        }

        internal static void AddOrUpdateFilter_ByFilterType(this List<RepositoryFilter> inThis, string inPropName, EFilterTypes inFilterType)
        {
            if (inThis == null)
            {
                inThis = new List<RepositoryFilter>();
            }
            var existingObject = inThis.FirstOrDefault(o => o.PropName.Equals(inPropName, StringComparison.InvariantCultureIgnoreCase));
            if (existingObject == null)
            {
                existingObject = new RepositoryFilter()
                {
                    ApplyFilter = false,
                    FilterType = inFilterType,
                    FilterValue = "",
                    PropName = inPropName,
                    ApplySort = false,
                    SortType = RepositoryFilterConverter.defaultSortType
                };
                inThis.Add(existingObject);
            }
            else
            {
                existingObject.FilterType = inFilterType;
            }
        }

        internal static void AddOrUpdateFilter_ByFilterSort(this List<RepositoryFilter> inThis, string inPropName, ESortTypes inFilterSortType)
        {
            if (inThis == null)
            {
                inThis = new List<RepositoryFilter>();
            }
            var existingObject = inThis.FirstOrDefault(o => o.PropName.Equals(inPropName, StringComparison.InvariantCultureIgnoreCase));
            if (existingObject == null)
            {
                existingObject = new RepositoryFilter()
                {
                    ApplyFilter = false,
                    FilterType = RepositoryFilterConverter.defaultFilterType,
                    FilterValue = "",
                    PropName = inPropName,
                    ApplySort = true,
                    SortType = inFilterSortType
                };
                inThis.Add(existingObject);
            }
            else
            {
                existingObject.SortType = inFilterSortType;
                existingObject.ApplySort = true;
            }
        }
    }
}
