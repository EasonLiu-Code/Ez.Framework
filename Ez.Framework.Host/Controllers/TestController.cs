using Microsoft.AspNetCore.Mvc;

namespace Ez.Framework.Controllers;

/// <summary>
/// 数据测试
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class TestController
{
    /// <summary>
    /// Template
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<DateTime> TestMessageAsync()
    {
        return DateTime.Now;
    }
}