namespace PetFam.Accounts.Infrastructure.Options;

public class JwtOptions()
{
    public static string SectionName = nameof(JwtOptions);
    
    public string Issuer { get; init; }= string.Empty;
    public string Audience { get; init; }= string.Empty;
    public string SecurityKey { get; init; }= string.Empty;
    public string Expiration { get; init; }= string.Empty;
}