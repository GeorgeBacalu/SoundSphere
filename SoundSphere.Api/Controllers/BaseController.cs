﻿using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;

namespace SoundSphere.Api.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
        public BaseController() { }

        protected Guid GetUserId()
        {
            string? rawToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            string? token = rawToken?.Substring("Bearer ".Length).Trim();
            var parserToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string? rawUserId = parserToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
            return rawUserId != null ? Guid.Parse(rawUserId) : Guid.Empty;
        }
    }
}