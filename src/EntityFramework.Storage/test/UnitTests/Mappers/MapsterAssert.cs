/*
 Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using System;
using Mapster;

namespace IdentityServer8.EntityFramework.UnitTests.Mappers;

internal static class MapsterAssert
{
    /// <summary>
    /// Compiles the profile in a strict configuration: every type pair must be explicitly configured, and every
    /// member of the destination type of each <paramref name="strictMaps"/> pair must have a mapped source.
    /// </summary>
    public static void ConfigurationIsValid(IRegister profile, params (Type Source, Type Destination)[] strictMaps)
    {
        var config = new TypeAdapterConfig { RequireExplicitMapping = true };
        config.Apply(profile);
        foreach (var (source, destination) in strictMaps)
        {
            config.ForType(source, destination).Settings.RequireDestinationMemberSource = true;
        }
        config.Compile();
    }
}
