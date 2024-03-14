using Carter;

namespace Ez.Framework.EndPoints;

/// <summary>
/// Tests-EndPoint
/// </summary>
public class Tests:ICarterModule
{
    /// <summary>
    /// AddRoutes
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("tests", (string request) => Task.FromResult($"测试EndPoints+{ request}")).WithName("testEndPointByCater");
    }
}