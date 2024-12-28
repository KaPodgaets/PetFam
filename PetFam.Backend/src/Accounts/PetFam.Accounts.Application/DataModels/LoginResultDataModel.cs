using PetFam.Accounts.Contracts.Responses;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.DataModels;

public class LoginResultDataModel
{
    public LoginResultDataModel(string accessToken, Guid refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public string AccessToken { get; init; }
    public Guid RefreshToken { get; init; }

    public Result<LoginResponse> ToResponse()
    {
        return new LoginResponse(AccessToken);
    }
}