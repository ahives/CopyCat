using CopyCat.Data.Model;
using CopyCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace CopyCat.Endpoint.Controllers;

[ApiController]
[Route("api")]
public class ImpersonationController :
    ControllerBase
{
    private readonly IAccountImpersonationService _service;

    public ImpersonationController(IAccountImpersonationService service)
    {
        _service = service;
    }

    [Route("impersonate")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public async Task<IActionResult> Impersonate(string sendingFacilityId, string sendingClientId)
    {
        var result = await _service.FindActiveAccount(sendingFacilityId, sendingClientId);

        if (result.IsData)
            return Ok(result.Data);

        return BadRequest();
    }
}