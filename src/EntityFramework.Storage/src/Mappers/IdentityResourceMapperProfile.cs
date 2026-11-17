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
    /// Defines entity/model mapping for identity resources.
    /// </summary>
    /// <seealso cref="IRegister" />
    public class IdentityResourceMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the identity resource mappings with the given configuration.
        /// </summary>
        /// <param name="config">The mapping configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Entities.IdentityResource, Models.IdentityResource>()
                .Map(dest => dest.UserClaims, src => MappingHelpers.MapList(src.UserClaims, x => x.Type))
                .Map(dest => dest.Properties, src => MappingHelpers.ToDictionary(src.Properties));

            config.NewConfig<Models.IdentityResource, Entities.IdentityResource>()
                .ShallowCopyForSameType(true)
                .Map(dest => dest.UserClaims, src => MappingHelpers.MapList(src.UserClaims, x => new Entities.IdentityResourceClaim { Type = x }))
                .Map(dest => dest.Properties, src => MappingHelpers.ToProperties<Entities.IdentityResourceProperty>(src.Properties));
        }
    }
}
