using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Midlewares;

internal class FastEndpointOTELMidleware
{
    private static readonly ActivitySource ActivitySource = new("WebApi.FastEndpoints");

    private readonly RequestDelegate _next;

    public FastEndpointOTELMidleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task Invoke(HttpContext context)
    {
        var activity = ActivitySource.StartActivity(
            $"FastEndpoint {context.Request.Method} {context.Request.Path}",
            ActivityKind.Server);

        activity?.SetTag("http.method", context.Request.Method);
        activity?.SetTag("endpoint.path", context.Request.Path);
        activity?.SetTag("user.authenticated", context.User?.Identity?.IsAuthenticated ?? false);

        try
        {
            await _next(context);
            activity?.SetTag("http.status_code", context.Response.StatusCode);
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            throw;
        }
    }
}
