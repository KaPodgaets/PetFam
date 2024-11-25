using Microsoft.AspNetCore.Http;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Presentation.Providers;

public class HttpContextProvider
{
    private const string REFRESH_TOKEN = "refreshToken";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Result<Guid> GetRefreshSessionCookie()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return Errors.General.Failure().ToErrorList();
        }
        
        if (!_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(REFRESH_TOKEN, out var refreshToken))
        {
            return Errors.General.NotFound(REFRESH_TOKEN).ToErrorList();
        }

        return Guid.Parse(refreshToken);
    }

    public Shared.SharedKernel.Result.Result SetRefreshSessionCookie(Guid refreshToken)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return Errors.General.Failure().ToErrorList();
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append(REFRESH_TOKEN, refreshToken.ToString());

        return Result.Success();
    }

    public Result DeleteRefreshSessionCookie()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return Errors.General.Failure().ToErrorList();
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Delete(REFRESH_TOKEN);

        return Result.Success();
    }
}