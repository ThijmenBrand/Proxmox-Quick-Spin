namespace TCP.Core.Models;

public class Qemu : VirtualMachine
{
    public int? pid { get; set; }
    public string? qmpstatus { get; set; }
    public string? running_machine { get; set; }
    public string? running_qemu { get; set; }
}