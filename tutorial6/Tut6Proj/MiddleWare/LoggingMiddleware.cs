using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tut6Proj.MiddleWare
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;// only place its accessible since readonly
                                               //_ -convention for private?
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request != null) // take name of path body parameter form request
            {
                string path = httpContext.Request.Path;
                string querryStr = httpContext.Request?.QueryString.ToString(); // ? - nullable since it cannot exist
                string method = httpContext.Request.Method.ToString();
                string bodyParameters = "";

                using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyParameters = await reader.ReadToEndAsync();
                }

            }

            await _next(httpContext);
        }
    }
}