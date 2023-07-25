using GamingWallet.Commands;

namespace GamingWallet.Utility;

public interface ICommandHandlerResolver
{
    public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;
}
