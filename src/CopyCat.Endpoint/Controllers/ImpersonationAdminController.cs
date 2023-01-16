namespace CopyCat.Endpoint.Controllers;

using Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ImpersonationAdminController :
    ControllerBase
{
    private readonly IImpersonationAdminProvider _provider;

    public ImpersonationAdminController(IImpersonationAdminProvider provider)
    {
        _provider = provider;
    }

    [HttpPost(Name = "AddImpersonatedAccount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddImpersonatedAccount(ImpersonatedAccount account)
    {
        _provider.AddImpersonatedAccount(account);
        return Ok();
    }

    [HttpGet(Name = "GetImpersonatedAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public IActionResult GetImpersonatedAccounts(Guid accountId)
    {
        return Ok(_provider.GetImpersonatedAccounts(accountId));
    }

    // [HttpGet(Name = "GetAllImpersonatedAccounts")]
    // public List<ImpersonatedAccount> GetAllImpersonatedAccounts()
    // {
    //     return _provider.GetImpersonatedAccounts();
    // }
}