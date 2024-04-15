using System.Net;

namespace TCP.Core.Models;

public class Response<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }

}