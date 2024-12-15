namespace PetFam.Discussions.Presentation.Requests;

public record EditMessageRequest(Guid UserId, string NewText);