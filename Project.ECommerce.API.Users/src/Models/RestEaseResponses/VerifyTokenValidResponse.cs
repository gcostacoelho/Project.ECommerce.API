using System.Net;

namespace Project.ECommerce.API.Users.src.Models.RestEaseResponses;
public class VerifyTokenValidResponse
{
    public string Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}