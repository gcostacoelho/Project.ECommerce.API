using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project.ECommerce.API.Orders.src.Models.Utils;

namespace Project.ECommerce.API.Orders.src.Middlewares
{
    public class ApiExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            try
            {
                await _next(context);
            }

            catch (ApiException error)
            {
                var responseModel = ApiResponse<string>.Fail(error.Message, error.StatusCode);

                switch (error.StatusCode)
                {
                    case HttpStatusCode.NoContent:
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        break;

                    case HttpStatusCode.NotFound:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case HttpStatusCode.BadRequest:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case HttpStatusCode.Unauthorized:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                }

                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }

            catch (Exception error)
            {
                var responseModel = ApiResponse<string>.Fail(error.Message, HttpStatusCode.InternalServerError);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}