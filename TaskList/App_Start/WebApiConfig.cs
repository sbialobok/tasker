﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TaskList
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //TODO: DO I need this?
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
