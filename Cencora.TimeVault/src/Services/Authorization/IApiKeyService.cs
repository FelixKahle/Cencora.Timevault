// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.AspNetCore.Authentication;

namespace Cencora.TimeVault.Services.Authorization;

/// <summary>
/// The service for handling API key authentication.
/// </summary>
public interface IApiKeyService
{
    /// <summary>
    /// Authenticates the given API key.
    /// </summary>
    /// <param name="apiKey">The API key.</param>
    /// <returns>The authentication result.</returns>
    public Task<AuthenticateResult> AuthenticateAsync(string apiKey);
}