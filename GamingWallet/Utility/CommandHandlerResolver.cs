using GamingWallet.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace GamingWallet.Utility;

/// <inheritdoc/>

public class CommandHandlerResolver : ICommandHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandlerResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
    {
        return _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
    }
}