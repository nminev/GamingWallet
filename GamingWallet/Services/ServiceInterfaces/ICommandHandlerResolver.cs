using GamingWallet.Commands;

namespace GamingWallet.Services.ServiceInterfaces;

public interface ICommandHandlerResolver
{
    public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;
}
