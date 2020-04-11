using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tut6Proj.Services;

namespace Tut6Proj.MiddleWare
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next; // readonly since that's only place it's accessible

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDbService dbService)
        {
            if (context.Request != null)
            {
                string path = context.Request.Path; // /api/students
                string method = context.Request.Method; // GET, POST, etc
                string querryString = context.Request?.QueryString.ToString(); // nullable as querryString doesn't have to be passed
                string bodyStr = "";
                
                using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true)) // fetch body of request to the string
                {
                    bodyStr = await reader.ReadToEndAsync();
                }
                
                string logEntrySeparator = "--------------------------------\n\t\tLog Entry:";
                string logTime = DateTime.Now.ToLongTimeString();
                string logDate = DateTime.Now.ToLongDateString();

                List<string> logData = new List<string> {
                    logEntrySeparator, logTime, logDate, path, method, querryString, bodyStr
                };
                dbService.SaveLogData(logData);
            }

            if (_next != null) await _next(context); // passing new inforamtion to next middleware
        }
    }
}