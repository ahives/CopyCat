using CopyCat.Data.Model;
using CopyCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace CopyCat.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountAdminController :
    ControllerBase
{
    private readonly IAccountService _service;

    public AccountAdminController(IAccountService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<Account>))]
    public IActionResult GetAccounts()
    {
        Result<IReadOnlyList<Account>> result = _service.GetAllAccounts();
        
        return Ok(result.Data);
    }

    [HttpPost(Name = "CreateAccount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateAccount(Account account)
    {
        Result result = _service.TryCreateAccount(account);

        if (result.IsData)
            return Ok();

        return BadRequest();
    }
}