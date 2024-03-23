using Microsoft.AspNetCore.Mvc;

namespace Ez.Framework.Controllers;

/// <summary>
/// TestController
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class TimeController : ControllerBase
{
    /// <summary>
    /// GetTimeNow
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult GetTimeNow() => Ok(DateTime.Now);

}