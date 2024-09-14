namespace PetFam.Api.Response
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
}
