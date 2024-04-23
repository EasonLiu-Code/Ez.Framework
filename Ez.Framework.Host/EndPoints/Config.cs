using Carter;
using Ez.Infrastructure.Common;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Ez.Framework.EndPoints;

/// <summary>
/// Config-EndPoint
/// </summary>
public class Config:ICarterModule
{
    /// <summary>
    /// AddRoutes
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/",
            httpContext =>
            {
                httpContext.Response.Redirect("/swagger/index.html");
                return Task.CompletedTask;
            });
        app.MapHealthChecks(
            "/HeartBeat",
            new HealthCheckOptions
            {
                ResponseWriter = async (httpContext, _) =>
                {
                    httpContext.Response.ContentType = "application/json; charset=utf-8";
                    await httpContext.Response.WriteAsync(
                        JsonConvert.SerializeObject(
                            new ApiResult<DateTime>
                            {
                                ErrorCode = 0,
                                IsSuccess = true,
                                Result = DateTime.Now
                            })).ConfigureAwait(false);
                }
            });
    }
}