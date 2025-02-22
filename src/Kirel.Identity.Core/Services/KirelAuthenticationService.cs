﻿using Kirel.Identity.Core.Models;
using Kirel.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Kirel.Identity.Core.Services;

/// <summary>
/// Provides methods for getting the user within the authentication procedure using different methods
/// </summary>
/// <typeparam name="TKey"> User key type </typeparam>
/// <typeparam name="TUser"> User type </typeparam>
public class KirelAuthenticationService<TKey, TUser>
    where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
    where TUser : KirelIdentityUser<TKey>
{
    /// <summary>
    /// Identity user manager
    /// </summary>
    protected readonly UserManager<TUser> UserManager;

    /// <summary>
    /// Constructor for KirelAuthenticationService
    /// </summary>
    /// <param name="userManager"> </param>
    public KirelAuthenticationService(UserManager<TUser> userManager)
    {
        UserManager = userManager;
    }

    /// <summary>
    /// Provides the ability to get the identity user after checking the login and password
    /// </summary>
    /// <param name="login"> User login </param>
    /// <param name="password"> User password </param>
    /// <returns> User class instance </returns>
    /// <exception cref="KirelAuthenticationException"> Returned if the user is not found or if the password is incorrect </exception>
    /// <exception cref="KirelIdentityStoreException"> If user or role managers fails on store based operations </exception>
    public async Task<TUser> LoginByPassword(string login, string password)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            throw new KirelAuthenticationException("Login or password cannot be empty");
        var user = await UserManager.FindByNameAsync(login);
        if (user == null) throw new KirelIdentityStoreException($"User with login {login} is not found");
        var result = await UserManager.CheckPasswordAsync(user, password);
        if (!result) throw new KirelAuthenticationException("Wrong password");
        return user;
    }
}