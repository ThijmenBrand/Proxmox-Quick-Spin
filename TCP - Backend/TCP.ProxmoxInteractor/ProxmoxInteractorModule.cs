using Microsoft.Extensions.DependencyInjection;
using TCP.ProxmoxInteractor.Factories;
using TCP.ProxmoxInteractor.Repositories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.ProxmoxInteractor;

public static class ProxmoxInteractorModule
{
    public static IServiceCollection RegisterProxmoxModule(this IServiceCollection services)
    {
        services.AddSingleton<IProxmoxRepository, ProxmoxRepository>();
        services.AddSingleton<IProxmoxClientFactory, ProxmoxClientFactory>();
        services.AddSingleton<INodesRepository, NodesRepository>();
        services.AddSingleton<IVirtualMachineRepository, VirtualMachineRepository>();

        return services;
    }
}