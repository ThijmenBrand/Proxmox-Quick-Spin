namespace TCP.Core.Models;

public class QemuConfiguration
{
   public string? Description { get; set; }
   public string? CiPassword { get; set; }
   public string? SshKeys { get; set; }
   public string? CiUser { get; set; }
   public string? IpGateway { get; set; }
   public int? CpuCores { get; set; }
   public int? CpuSockets { get; set; }
   public int? CpuLimit { get; set; }
   public string? IpAddress { get; set; }
   public int? Memory { get; set; }
   //Boot vm on system startup
   public bool? OnBoot { get; set; }
   //Protect vm and its disc from being removed
   public bool? Protection { get; set; }
   //Enable or Disable template
   public bool? Template { get; set; } 
}