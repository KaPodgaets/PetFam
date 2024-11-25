using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Application.Models;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.Accounts.Infrastructure.IdentityManagers;
using PetFam.Accounts.Infrastructure.Options;
using PetFam.Shared.Models;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Infrastructure;

public class JwtTokenProvider : ITokenProvider
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly ILogger<JwtTokenProvider> _logger;
    private readonly PermissionManager _permissionManager;
    private readonly AccountsWriteDbContext _accountsContext;
    private readonly IRefreshSessionsManager _refreshSessionsManager;
    

    public JwtTokenProvider(
        IOptions<JwtOptions> jwtOptions,
        ILogger<JwtTokenProvider> logger,
        PermissionManager permissionManager, 
        AccountsWriteDbContext accountsContext,
        IRefreshSessionsManager refreshSessionsManager)
    {
        _jwtOptions = jwtOptions;
        _logger = logger;
        _permissionManager = permissionManager;
        _accountsContext = accountsContext;
        _refreshSessionsManager = refreshSessionsManager;
    }

    public async Task<JwtResult> GenerateAccessToken(User user, CancellationToken cancellationToken = default)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var roleClaims = user.Roles
            .Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));
        var permissionCodes = await _permissionManager
            .GetUserPermissionsCode(user.Id, cancellationToken);
        var permissionClaims = permissionCodes
            .Select(x => new Claim(CustomClaims.Permission, x));
        
        var jti = Guid.NewGuid();
        
        Claim[] claims =
        [
            new Claim(CustomClaims.Sub, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? String.Empty),
            new Claim(CustomClaims.Jti, jti.ToString()),
        ];

        claims = claims
            .Concat(roleClaims)
            .Concat(permissionClaims)
            .ToArray();

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            expires: DateTime.Now.AddMinutes(
                double.Parse(_jwtOptions.Value.Expiration)),
            signingCredentials: signingCredentials,
            claims: claims);

        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        _logger.LogInformation("{user} successfully logged in", user.Email);
        
        return new JwtResult(stringToken, jti);
    }

    public async Task<Guid> GenerateRefreshToken(User user, Guid jti, CancellationToken cancellationToken = default)
    {
        var refreshSession = new RefreshSession
        {
            CreatedAt = DateTime.UtcNow,
            ExpiresIn = DateTime.UtcNow.AddDays(30),
            UserId = user.Id,
            RefreshToken = Guid.NewGuid(),
            Jti = jti
        };
        
        await _refreshSessionsManager.Create(refreshSession, cancellationToken);

        return refreshSession.RefreshToken;
    }

    public async Task<Result<IReadOnlyList<Claim>>> GetUserClaims(
        string token,
        CancellationToken cancellationToken = default)
    {
        var validationParameters = JwtValidationParametersFactory.CreateWithoutLifeTime(_jwtOptions.Value);
        var jwtHandler = new JwtSecurityTokenHandler();
        var result = await jwtHandler.ValidateTokenAsync(token, validationParameters);
        if (result.IsValid is false)
            return Errors.Tokens.NotValid().ToErrorList();
        
        return result.ClaimsIdentity.Claims.ToList();
    } 
}