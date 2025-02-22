﻿using Kirel.Identity.Controllers;
using Kirel.Identity.Server.Core.Services;
using Kirel.Identity.Server.Domain;
using Kirel.Identity.Server.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Kirel.Identity.Server.API.Controllers;

/// <inheritdoc />

[ApiController]
[Route("registration")]
public class RegistrationController : KirelRegistrationController<RegistrationService, UserRegistrationDto, Guid, User>
{
    /// <inheritdoc />
    public RegistrationController(RegistrationService service) : base(service)
    {
    }
}