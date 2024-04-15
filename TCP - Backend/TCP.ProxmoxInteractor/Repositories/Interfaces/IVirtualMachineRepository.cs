using Corsinvest.ProxmoxVE.Api;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using TCP.Core.Models;

namespace TCP.ProxmoxInteractor.Repositories.Interfaces;

public interface IVirtualMachineRepository
{
    public Task<IEnumerable<NodeVmQemu>> ListQemuMachines(string node);
    public Task<IEnumerable<NodeVmLxc>> ListLxcMachines(string node);
    public Task<VmConfigQemu> GetQemuMachine(string node, int vmId);
    public Task<VmConfigLxc> GetLxcMachine(string node, int vmId);

    public Task<Result> CloneQemuMachine(string node, int vmId, CloneConfiguration configuration);

    public Task<Result> CloneLxcMachine(string node, int vmId, CloneConfiguration configuration);

    public Task<Result> CreateOrUpdateQemuConfig(string node, int vmId, QemuConfiguration configuration);
}