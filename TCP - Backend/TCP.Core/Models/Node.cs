namespace TCP.Core.Models;

public class Node
{
    public string node { get; set; }
    public string status { get; set; }
    public float? cpu { get; set; }
    public string? level { get; set; }
    public long? maxcpu { get; set; }
    public long? maxmem { get; set; }
    public long? mem { get; set; }
    public string? ssl_fingerprint { get; set; }
    public int? uptime { get; set; }
}