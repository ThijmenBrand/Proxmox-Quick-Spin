namespace TCP.ProxmoxInteractor.Repositories;

public interface IVirtualMachineRepository
{
  public Task<dynamic> ListQemuMachines(string node);
    public Task<dynamic> ListIxcMachines(string node);
}