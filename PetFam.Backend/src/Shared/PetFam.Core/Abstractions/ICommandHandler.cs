
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Shared.Abstractions
{
    public interface ICommandHandler<TValue, in TCommand> where TCommand : ICommand
    {
        Task<Result<TValue>> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<Result> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
