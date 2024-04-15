//using Api Token

using Corsinvest.ProxmoxVE.Api;
using Newtonsoft.Json;

var client = new PveClient("192.168.128.200");
client.ApiToken = "root@pam!TCP=78bce79f-9aef-4b8d-a182-0c21a6a83eff";

var version = await client.Version.Version();
version.Response.data
Console.WriteLine(JsonConvert.SerializeObject(version.Response.data, Formatting.Indented));