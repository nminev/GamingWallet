namespace GamingWallet.Commands;


/// <summary>
/// Defines the contract for a command handler that can handle a specific type of command.
/// </summary>
/// <typeparam name="TCommand">The type of command that the handler can handle. This type must implement the <see cref="ICommand"/> interface.</typeparam>
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="command">The command to handle. This must be of type <typeparamref name="TCommand"/>.</param>
    void Handle(TCommand command);
}
