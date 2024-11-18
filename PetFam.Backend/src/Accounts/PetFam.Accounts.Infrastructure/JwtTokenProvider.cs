using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure;

public class JwtTokenProvider:ITokenProvider
{
    private readonly IOptions<JwtOptions> _jwtJwtOptions;
    private readonly ILogger<JwtTokenProvider> _logger;

    public JwtTokenProvider(
        IOptions<JwtOptions> jwtOptions,
        ILogger<JwtTokenProvider> logger)
    {
        _jwtJwtOptions = jwtOptions;
        _logger = logger;
    }
    public string GetAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtJwtOptions.Value.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        Claim[] claims = [
            new Claim(CustomClaims.Sub, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? String.Empty)
        ]; 
        
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