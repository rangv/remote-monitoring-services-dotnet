﻿// Copyright (c) Microsoft. All rights reserved.

using System.Web.Http;
using Microsoft.Web.Http.Routing;
using Owin;

namespace Microsoft.Azure.IoTSolutions.IotHubManager.WebService
{
    /// <summary>Application wrapper started by the entry point, see Program.cs</summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.AddApiVersioning(o =>
            {
                // When this property is set to `true`, the HTTP headers
                // "api-supported-versions" and "api-deprecated-versions" will
                // be added to all valid service routes. This information is
                // useful for advertising which versions are supported and
                // scheduled for deprecation to clients. This information is
                // also useful when supporting the OPTIONS verb.
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = false;
            });

            config.Routes.MapHttpRoute(
                name: "VersionedApi",
                routeTemplate: "v{apiVersion}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { apiVersion = new ApiVersionRouteConstraint() });

            //TODO: If this fails with "Exception thrown: 'System.Reflection.TargetInvocationException' 
            //in mscorlib.dll" tell the developer to run VS as an admin
            app.UseWebApi(config);
        }
    }
}
