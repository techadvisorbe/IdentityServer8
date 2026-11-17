/*
 Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer8.Models;

namespace IdentityServer8.EntityFramework.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for clients.
    /// </summary>
    /// <seealso cref="IRegister" />
    public class ClientMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the client mappings with the given configuration.
        /// </summary>
        /// <param name="config">The mapping configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Entities.Client, Models.Client>()
                .Map(dest => dest.ClientSecrets, src => src.ClientSecrets ?? new List<Entities.ClientSecret>())
                .Map(dest => dest.AllowedGrantTypes, src => MappingHelpers.MapList(src.AllowedGrantTypes, x => x.GrantType))
                .Map(dest => dest.RedirectUris, src => MappingHelpers.MapList(src.RedirectUris, x => x.RedirectUri))
                .Map(dest => dest.PostLogoutRedirectUris, src => MappingHelpers.MapList(src.PostLogoutRedirectUris, x => x.PostLogoutRedirectUri))
                .Map(dest => dest.AllowedScopes, src => MappingHelpers.MapList(src.AllowedScopes, x => x.Scope))
                .Map(dest => dest.AllowedIdentityTokenSigningAlgorithms, src => AllowedSigningAlgorithmsConverter.ToModel(src.AllowedIdentityTokenSigningAlgorithms))
                .Map(dest => dest.IdentityProviderRestrictions, src => MappingHelpers.MapList(src.IdentityProviderRestrictions, x => x.Provider))
                .Map(dest => dest.Claims, src => src.Claims ?? new List<Entities.ClientClaim>())
                .Map(dest => dest.AllowedCorsOrigins, src => MappingHelpers.MapList(src.AllowedCorsOrigins, x => x.Origin))
                .Map(dest => dest.Properties, src => MappingHelpers.ToDictionary(src.Properties));

            config.NewConfig<Models.Client, Entities.Client>()
                .ShallowCopyForSameType(true)
                .Map(dest => dest.ClientSecrets, src => src.ClientSecrets ?? new List<Secret>())
                .Map(dest => dest.AllowedGrantTypes, src => MappingHelpers.MapList(src.AllowedGrantTypes, x => new Entities.ClientGrantType { GrantType = x }))
                .Map(dest => dest.RedirectUris, src => MappingHelpers.MapList(src.RedirectUris, x => new Entities.ClientRedirectUri { RedirectUri = x }))
                .Map(dest => dest.PostLogoutRedirectUris, src => MappingHelpers.MapList(src.PostLogoutRedirectUris, x => new Entities.ClientPostLogoutRedirectUri { PostLogoutRedirectUri = x }))
                .Map(dest => dest.AllowedScopes, src => MappingHelpers.MapList(src.AllowedScopes, x => new Entities.ClientScope { Scope = x }))
                .Map(dest => dest.AllowedIdentityTokenSigningAlgorithms, src => AllowedSigningAlgorithmsConverter.ToEntity(src.AllowedIdentityTokenSigningAlgorithms))
                .Map(dest => dest.IdentityProviderRestrictions, src => MappingHelpers.MapList(src.IdentityProviderRestrictions, x => new Entities.ClientIdPRestriction { Provider = x }))
                .Map(dest => dest.Claims, src => src.Claims ?? new List<ClientClaim>())
                .Map(dest => dest.AllowedCorsOrigins, src => MappingHelpers.MapList(src.AllowedCorsOrigins, x => new Entities.ClientCorsOrigin { Origin = x }))
                .Map(dest => dest.Properties, src => MappingHelpers.ToProperties<Entities.ClientProperty>(src.Properties));

            config.NewConfig<Entities.ClientClaim, ClientClaim>()
                .MapWith(src => new ClientClaim(src.Type, src.Value, ClaimValueTypes.String));

            config.NewConfig<ClientClaim, Entities.ClientClaim>();

            config.NewConfig<Entities.ClientSecret, Secret>();

            config.NewConfig<Secret, Entities.ClientSecret>();
        }
    }
}
