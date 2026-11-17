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
    /// Defines entity/model mapping for API resources.
    /// </summary>
    /// <seealso cref="IRegister" />
    public class ApiResourceMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the API resource mappings with the given configuration.
        /// </summary>
        /// <param name="config">The mapping configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Entities.ApiResource, Models.ApiResource>()
                .Map(dest => dest.ApiSecrets, src => src.Secrets ?? new List<Entities.ApiResourceSecret>())
                .Map(dest => dest.Scopes, src => MappingHelpers.MapList(src.Scopes, x => x.Scope))
                .Map(dest => dest.UserClaims, src => MappingHelpers.MapList(src.UserClaims, x => x.Type))
                .Map(dest => dest.Properties, src => MappingHelpers.ToDictionary(src.Properties))
                .Map(dest => dest.AllowedAccessTokenSigningAlgorithms, src => AllowedSigningAlgorithmsConverter.ToModel(src.AllowedAccessTokenSigningAlgorithms));

            config.NewConfig<Models.ApiResource, Entities.ApiResource>()
                .ShallowCopyForSameType(true)
                .Map(dest => dest.Secrets, src => src.ApiSecrets ?? new List<Models.Secret>())
                .Map(dest => dest.Scopes, src => MappingHelpers.MapList(src.Scopes, x => new Entities.ApiResourceScope { Scope = x }))
                .Map(dest => dest.UserClaims, src => MappingHelpers.MapList(src.UserClaims, x => new Entities.ApiResourceClaim { Type = x }))
                .Map(dest => dest.Properties, src => MappingHelpers.ToProperties<Entities.ApiResourceProperty>(src.Properties))
                .Map(dest => dest.AllowedAccessTokenSigningAlgorithms, src => AllowedSigningAlgorithmsConverter.ToEntity(src.AllowedAccessTokenSigningAlgorithms));

            config.NewConfig<Entities.ApiResourceSecret, Models.Secret>();

            config.NewConfig<Models.Secret, Entities.ApiResourceSecret>();
        }
    }
}
