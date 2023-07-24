using GamingWallet.Commands;
using GamingWallet.Services.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

public class CommandHandlerResolver : ICommandHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandlerResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
    {
        return _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
    }
}
