namespace PetFam.Shared.SharedKernel.Errors
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is invalid.");
            }

            public static Error ValueIsNotUnique(string? name = null)
            {
                var label = name ?? "value";
                return Error.Conflict("value.not.unique", $"{label} is not unique");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id {id}";
                return Error.Validation("record.not.found", $"record not found {forId}");
            }

            public static Error NotFound(string? name = null)
            {
                var forName = name == null ? "" : $" for Id {name}";
                return Error.Validation("record.not.found", $"record not found {forName}");
            }

            public static Error ValueIsRequired(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is required.");
            }

            public static Error Failure()
            {
                return Error.Failure("internal.failure", "unknown server error");
            }

            public static Error DeletionEntityWithRelation()
            {
                return Error.Conflict("can't.delete.entity", "can not delete entity with relation");
            }

            public static Error AlreadyExist(string? email = null)
            {
                return Error.Validation("volunteer.already.exist", $"volunteer with email: {email} already exist");
            }
        }

        public static class VolunteerErrors
        {
            public static Error AlreadyExist(string? email = null)
            {
                return Error.Validation("volunteer.already.exist", $"volunteer with email: {email} already exist");
            }
        }

        public static class Species
        {
            public static Error AlreadyExist(string? name = null)
            {
                return Error.Validation("species.already.exist", $"species with name: {name} already exist");
            }

            public static Error CannotDeleteDueToRelatedRecords(Guid? id = null)
            {
                return Error.Conflict("species.relations.exist",
                    $"species with id: {id} still has relations");
            }
        }

        public static class Breed
        {
            public static Error CannotDeleteDueToRelatedRecords(Guid? id = null)
            {
                return Error.Conflict("breed.relations.exist",
                    $"breed with id: {id} still has relations");
            }
        }

        public static class Pet
        {
            public static Error PhotoNotFound(string? path = null)
            {
                return Error.Validation("photo.not.found",
                    $"photo with path: {path} was not found");
            }
        }

        public static class User
        {
            public static Error InvalidCredentials()
            {
                return Error.Failure("user.password.incorrect", "user or password is incorrect");
            }
        }

        public static class Tokens
        {
            public static Error ExpiredToken()
            {
                return Error.Validation("token.expired", "token expired");
            }

            public static Error NotValid()
            {
                return Error.Validation("token.not.valid", "token not valid");
            }
        }

        public static class VolunteeringApplications
        {
            public static Error ChangeStatusNotAllowed()
            {
                return Error.Validation("status.change.denied", "Change status not allowed");
            }
        }

        public static class Discussions
        {
            public static Error CannotAddMessageToClosedDiscussion()
            {
                return Error.Failure("discussions.is.closed", "Can not add message to closed discussion");
            }

            public static Error IncorrectNumberOfParticipants()
            {
                return Error.Failure("discussions.not.created", "number of participants incorrect");
            }

            public static Error CannotAddNewMessageFromNonParticipants()
            {
                return Error.Failure("message.not.added", "Can not add message from non-participants");
            }

            public static Error OnlyAuthorCanRemoveMessage()
            {
                return Error.Failure("message.not.removed", "Only author can remove message");
            }
        }

        public static class Messages
        {
            public static Error CannotBeEmptyOrWhitespace()
            {
                return Error.Failure("message.empty", "Message cannot be empty or whitespace");
            }
        }
    }
}