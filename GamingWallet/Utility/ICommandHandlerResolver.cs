using GamingWallet.Commands;

namespace GamingWallet.Utility;

/// <summary>
/// Defines the contract for a service that can resolve command handlers.
/// </summary>
public interface ICommandHandlerResolver
{
    /// <summary>
    /// Resolves the command handler for the specified command type.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command for which to resolve the handler.</typeparam>
    /// <returns>The command handler for the specified command type.</returns>
    ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;
}