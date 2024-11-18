namespace PetFam.Accounts.Infrastructure;

public class JwtOptions()
{
    public static string JwtOptionsName = nameof(JwtOptions);
    
    public string Issuer { get; init; }= string.Empty;
    public string Audience { get; init; }= string.Empty;
    public string SecurityKey { get; init; }= string.Empty;
    public string Expiration { get; init; }= string.Empty;
}