using System.Collections;
using Corsinvest.ProxmoxVE.Api;
using Corsinvest.ProxmoxVE.Api.Extension;
using Corsinvest.ProxmoxVE.Api.Shared.Models;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using TCP.Core.Models;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor.Factories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.ProxmoxInteractor.Repositories;

public class VirtualMachineRepository : BaseProxmoxRepository, IVirtualMachineRepository
{
    public VirtualMachineRepository(IProxmoxClientFactory proxmoxClientFactory) : base(proxmoxClientFactory) {}

    public async Task<IEnumerable<NodeVmQemu>> ListQemuMachines(string node)
    {
        var client = _proxmoxClientFactory.Create();
        var qemuMachines = await client.Nodes[node].Qemu.Get();

        return qemuMachines;
    }

    public async Task<IEnumerable<NodeVmLxc>> ListLxcMachines(string node)
    {
        var client = _proxmoxClientFactory.Create();
        var lxcMachines = await client.Nodes[node].Lxc.Get();

        return lxcMachines;
    }

    public async Task<VmConfigQemu> GetQemuMachine(string node, int vmId)
    {
        var client = _proxmoxClientFactory.Create();
        var qemuMachine = await client.Nodes[node].Qemu[vmId].Config.Get();

        return qemuMachine;
    }

    public async Task<VmConfigLxc> GetLxcMachine(string node, int vmId)
    {
        var client = _proxmoxClientFactory.Create();
        var lxcMachine = await client.Nodes[node].Lxc[vmId].Config.Get();

        return lxcMachine;
    }

    public async Task<Result> CloneQemuMachine(string node, int vmId, CloneConfiguration configuration)
    {
        var client = _proxmoxClientFactory.Create();

        if (configuration.FullClone == true && configuration.TargetStorage == null)
        {
            throw new ArgumentNullException(nameof(configuration.TargetStorage),
                "When creating a full clone, target storage must be defined!");
        }
        
        var result = await client.Nodes[node].Qemu[vmId].Clone.CloneVm(newid: configuration.NewId, name: configuration.Name,
            description: configuration.Description, full: configuration.FullClone, storage: configuration.TargetStorage);

        return result;
    }
    
    public async Task<Result> CloneLxcMachine(string node, int vmId, CloneConfiguration configuration)
    {
        var client = _proxmoxClientFactory.Create();

        if (configuration.FullClone == true && configuration.TargetStorage == null)
        {
            throw new ArgumentNullException(nameof(configuration.TargetStorage),
                "When creating a full clone, target storage must be defined!");
        }
        
        var result = await client.Nodes[node].Lxc[vmId].Clone.CloneVm(newid: configuration.NewId, hostname: configuration.Name,
            description: configuration.Description, full: configuration.FullClone, storage: configuration.TargetStorage);

        return result;
    }

    public async Task<Result> CreateOrUpdateQemuConfig(string node, int vmId, QemuConfiguration configuration)
    {
        var client = _proxmoxClientFactory.Create();
        
        //TODO move this complex logic somewhere else

        if ((!string.IsNullOrEmpty(configuration.IpAddress) && configuration.IpAddress != "dhcp") && configuration.IpGateway == null)
        {
            throw new ArgumentNullException(nameof(configuration.IpGateway),
                "when assigning static IP, gateway must be defined!");
        }

        var ipConfig = new Dictionary<int, string>();
        string? memoryString = null;

        if (string.IsNullOrEmpty(configuration.IpGateway) || string.IsNullOrEmpty(configuration.IpAddress))
        {
            var ipString = configuration.IpGateway != null
                ? $"gw={configuration.IpGateway},ip={configuration.IpAddress}"
                : $"ip={configuration.IpAddress}";

            ipConfig.Add(0, ipString);
        }

        if (configuration.Memory is >= 0)
        {
            memoryString = $"current={configuration.Memory}";
        }


        var result = await client.Nodes[node].Qemu[vmId].Config.UpdateVm(
            description: configuration.Description, ciuser: configuration.CiUser, cipassword: configuration.CiPassword,
            sshkeys: configuration.SshKeys,
            cores: configuration.CpuCores, cpulimit: configuration.CpuLimit, ipconfigN: ipConfig, memory: memoryString,
            onboot: configuration.OnBoot, protection: configuration.Protection, sockets: configuration.CpuSockets,
            template: configuration.Template);

        return result;
    }
}