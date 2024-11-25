using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.IdentityManagers;
using PetFam.Accounts.Infrastructure.Models;
using PetFam.Accounts.Infrastructure.Options;

namespace PetFam.Accounts.Infrastructure;

public class JwtTokenProvider : ITokenProvider
{
    private readonly IOptions<JwtOptions> _jwtJwtOptions;
    private readonly ILogger<JwtTokenProvider> _logger;
    private readonly PermissionManager _permissionManager;

    public JwtTokenProvider(
        IOptions<JwtOptions> jwtOptions,
        ILogger<JwtTokenProvider> logger,
        PermissionManager permissionManager)
    {
        _jwtJwtOptions = jwtOptions;
        _logger = logger;
        _permissionManager = permissionManager;
    }

    public async Task<string> GetAccessToken(User user, CancellationToken cancellationToken = default)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtJwtOptions.Value.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var roleClaims = user.Roles
            .Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));
        var permissionCodes = await _permissionManager
            .GetUserPermissionsCode(user.Id, cancellationToken);
        var permissionClaims = permissionCodes
            .Select(x => new Claim(CustomClaims.Permission, x));
        
        Claim[] claims =
        [
            new Claim(CustomClaims.Sub, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? String.Empty),
        ];

        claims = claims
            .Concat(roleClaims)
            .Concat(permissionClaims)
            .ToArray();

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtJwtOptions.Value.Issuer,
            audience: _jwtJwtOptions.Value.Audience,
            expires: DateTime.Now.AddMinutes(
                double.Parse(_jwtJwtOptions.Value.Expiration)),
            signingCredentials: signingCredentials,
            claims: claims);

        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        _logger.LogInformation("{user} successfully logged in", user.Email);

        return stringToken;
    }
}