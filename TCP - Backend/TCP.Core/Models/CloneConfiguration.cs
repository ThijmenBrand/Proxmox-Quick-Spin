namespace TCP.Core.Models;

public class CloneConfiguration
{

    public int NewId { get; set; } 
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? TargetStorage { get; set; }
    public bool? FullClone { get; set; } = true;
}