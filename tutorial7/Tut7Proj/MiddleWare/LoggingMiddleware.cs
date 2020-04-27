using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tut7Proj.Models;
using Tut7Proj.Services;

namespace Tut7Proj.MiddleWare
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
            LoginModel loginModel = new LoginModel();
            if (context.Request != null)
            {
                string path = context.Request.Path; // /api/students
                string method = context.Request.Method; // GET, POST, etc
                string querryString = context.Request?.QueryString.ToString(); // nullable as querryString doesn't have to be passed
                string bodyStr = "";

                using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                // fetch body of request to the string
                {
                    bodyStr = await reader.ReadToEndAsync();
                }

                string logEntrySeparator = "--------------------------------\n\t\tLog Entry:";
                string logTime = DateTime.Now.ToLongTimeString();
                string logDate = DateTime.Now.ToLongDateString();

                List<string> logData = new List<string> {
                    logEntrySeparator, logTime, logDate, path, method, querryString, bodyStr
                };

                dbService.SavRequestDataToFile(logData);

                if (path.ToLower().Contains("login") && method == "POST") // WHEN LOGIN AND PASSWORD ARE PASSED SAVE THEM TO .txt FILE AND DB
                {
                    loginModel = FillLoginModel(bodyStr);
                    dbService.SaveLoginDataToFile(loginModel);
                    dbService.SaveLoginDataToDb(loginModel);
                }
                // var body = await new StreamReader(request.Body).ReadToEndAsync();
                // request.Body.Seek(0, SeekOrigin.Begin);
                // bodyStr = await reader.ReadToEndAsync();
                // bodyStr.Seek(0, SeekOrigin.Begin);
                // response.Body.Seek(0, SeekOrigin.Begin);

                context.Request.EnableBuffering();
                context.Request.Body.Position = 0;
            }
            if (_next != null) await _next(context); // passing new inforamtion to next middleware
        }

        public LoginModel FillLoginModel(string bodyStr) // not nice solution - TO CORRECT
        // signs prohibited in login and password: : and "
        {
            List<string> partsList = new List<string>();
            string[] parts = bodyStr.Split(':');
            foreach (string str in parts)
            {
                string[] parts2 = str.Split("\"");
                foreach (string s in parts2)
                {
                    partsList.Add(s);
                }
            }
            LoginModel loginModel = new LoginModel()
            {
                Login = partsList[7], // 4 when not using Login Model in Log_inRequest
                Password = partsList[12], // 9 when not using Login Model in Log_inRequest
                Role = partsList[17] // for single Role (which user himself specifies when logging in)
            };
            return loginModel;
        }
    }
}