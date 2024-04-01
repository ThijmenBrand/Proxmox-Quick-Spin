namespace TCP.Core.Models;

public class VirtualMachine
{
    public string status { get; set; }
    public int vmid { get; set; }
    public int? cpus { get; set; }
    public long? maxdisk { get; set; }
    public long? maxmem { get; set; }
    public string? name { get; set; }
    public string? tags { get; set; }
    public int? uptime { get; set; }
}