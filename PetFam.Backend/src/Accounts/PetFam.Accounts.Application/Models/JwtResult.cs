namespace PetFam.Accounts.Application.Models;

public record JwtResult(string AccessToken, Guid AccessTokenJti);