using CopyCat.Data;
using CopyCat.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace CopyCat.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountAdminController :
    ControllerBase
{
    private readonly IAccountAdminProvider _provider;

    public AccountAdminController(IAccountAdminProvider provider)
    {
        _provider = provider;
    }

    [HttpGet(Name = "GetAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<Account>))]
    public IActionResult GetAccounts()
    {
        return Ok(_provider.GetAccounts());
    }

    [HttpPost(Name = "AddAccount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddAccount(Account account)
    {
        _provider.TryAddAccount(account);
        return Ok();
        // if (!IsValidAddAccountRequest(account))
        //     return BadRequest();
        //
        // if (_provider.TryAddAccount(account))
        //     return Ok();
    }
}