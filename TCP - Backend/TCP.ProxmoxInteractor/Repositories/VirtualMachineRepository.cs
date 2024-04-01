using TCP.ProxmoxInteractor.Factories;

namespace TCP.ProxmoxInteractor.Repositories;

public class VirtualMachineRepository : BaseProxmoxRepository, IVirtualMachineRepository
{
    public VirtualMachineRepository(IProxmoxClientFactory proxmoxClientFactory) : base(proxmoxClientFactory) {}

    public async Task<dynamic> ListQemuMachines(string node)
    {
        var client = _proxmoxClientFactory.Create();
        var qemuMachines = await client.Nodes[node].Qemu.Vmlist();

        return qemuMachines.Response;
    }

    public async Task<dynamic> ListIxcMachines(string node)
    {
        var client = _proxmoxClientFactory.Create();
        var ixcMachines = await client.Nodes[node].Lxc.Vmlist();

        return ixcMachines.Response;
    }
}