using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Models;

namespace UserManagement.Middleware
{
   
    public class GlobalExceptionHandling :IMiddleware
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly MyContext myContext;
        public GlobalExceptionHandling(MyContext myContext)
        {
            this.myContext = myContext;
            this._contextAccessor = new HttpContextAccessor();
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(_contextAccessor.HttpContext.Session.GetString("ID"))) {
                    // Get stack trace for the exception with source file information
                    var st = new StackTrace(ex, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    /*context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;*/
                    RouteData routeData = context.GetRouteData();
                    var route = routeData.Values["controller"].ToString();
                    var action = routeData.Values["action"].ToString();
                    var data = "";
                    if (!string.IsNullOrEmpty(context.Request.QueryString.Value))
                    {
                        data = context.Request.QueryString.Value;
                    }
                    else
                    {
                        data = GetRoutePath(context);
                    }
                    IErrorApplication errorApplication = new IErrorApplication
                    {
                        User_ID = Int32.Parse(_contextAccessor.HttpContext.Session.GetString("ID")),
                        ERROT_DATE = DateTime.Now,
                        CONTROLLER = route,
                        FUNCTION = action,
                        ERROR_LINE = line.ToString(),
                        ERROR_MESSAGE = ex.Message,
                        STATUS = "Need To Solve",
                        PARAM = data,
                        CREATE_BY = String.IsNullOrWhiteSpace(_contextAccessor.HttpContext.Session.GetString("Name")) ? null : _contextAccessor.HttpContext.Session.GetString("Name"),
                        CREATE_DATE = DateTime.Now,

                    };
                    myContext.IErrorApplication.Add(errorApplication);
                    myContext.SaveChanges();
                }
                /*ProblemDetails problemDetails = new ProblemDetails() {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Application Error",
                    Title = "Application Error",
                    Detail = "An Internal Application Error has occured"
                };
                string json= JsonConvert.SerializeObject(problemDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);*/
                context.Response.Redirect("../home/InternalServer");

            }
        }
        private string GetRoutePath(HttpContext httpContext)
        {
            string path = httpContext.Request.Path.ToString();
            string[] pathValues = path.Split("/");
            RouteData route = httpContext.GetRouteData();

            // Remove controller and action values from route.
            // We are looking to replace parameters only
            route.Values.Remove("controller");
            route.Values.Remove("action");
            foreach (KeyValuePair<string, object?> routeValue in route.Values)
            {
                for (int i = 1; i < pathValues.Length; i++)
                {
                    if (routeValue.Value == null) { continue; }

                    if (pathValues[i].Equals(routeValue.Value.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        pathValues[i] = "{" + routeValue.Key + "}";
                    }
                }
            }

            return string.Join("/", pathValues);
        }


    }
}
