namespace PetFam.Accounts.Infrastructure;

public class AdminOptions()
{
    public static string SectionName = "Admin";
    public static string RoleName = "Admin";
    
    public string UserName { get; init; }= string.Empty;
    public string Email { get; init; }= string.Empty;
    public string Password { get; init; }= string.Empty;
}