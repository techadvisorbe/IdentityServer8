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
    /// Defines entity/model mapping for scopes.
    /// </summary>
    /// <seealso cref="IRegister" />
    public class ScopeMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the scope mappings with the given configuration.
        /// </summary>
        /// <param name="config">The mapping configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Entities.ApiScope, Models.ApiScope>()
                .Map(dest => dest.UserClaims, src => MappingHelpers.MapList(src.UserClaims, x => x.Type))
                .Map(dest => dest.Properties, src => MappingHelpers.ToDictionary(src.Properties));

            config.NewConfig<Models.ApiScope, Entities.ApiScope>()
                .ShallowCopyForSameType(true)
                .Map(dest => dest.UserClaims, src => MappingHelpers.MapList(src.UserClaims, x => new Entities.ApiScopeClaim { Type = x }))
                .Map(dest => dest.Properties, src => MappingHelpers.ToProperties<Entities.ApiScopeProperty>(src.Properties));
        }
    }
}
