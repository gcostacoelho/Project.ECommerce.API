using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Project.ECommerce.API.Authentication.src.Models.Utils;
public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static ApiResponse<T> Fail(string errorMessage, HttpStatusCode statusCode)
    {
        return new ApiResponse<T> { Succeeded = false, StatusCode = statusCode, Message = errorMessage };
    }

    public static ApiResponse<T> Success(T data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new ApiResponse<T> { Succeeded = true, Data = data, StatusCode = httpStatusCode };
    }
}