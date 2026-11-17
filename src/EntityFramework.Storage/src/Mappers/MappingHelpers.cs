/*
 Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer8.EntityFramework.Mappers
{
    /// <summary>
    /// Collection helpers shared by the entity/model mapping configurations.
    /// A <c>null</c> source collection always maps to an empty destination collection.
    /// </summary>
    static class MappingHelpers
    {
        public static List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source, Func<TSource, TDestination> map)
        {
            return source == null ? new List<TDestination>() : source.Select(map).ToList();
        }

        /// <summary>
        /// Maps entity properties to a model dictionary. Throws if the source contains duplicate keys.
        /// </summary>
        public static Dictionary<string, string> ToDictionary<TProperty>(IEnumerable<TProperty> source)
            where TProperty : Entities.Property
        {
            var result = new Dictionary<string, string>();
            if (source != null)
            {
                foreach (var item in source)
                {
                    result.Add(item.Key, item.Value);
                }
            }
            return result;
        }

        public static List<TProperty> ToProperties<TProperty>(IDictionary<string, string> source)
            where TProperty : Entities.Property, new()
        {
            return MapList(source, x => new TProperty { Key = x.Key, Value = x.Value });
        }
    }
}
