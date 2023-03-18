using Business.Dtos.Inputs;
using Business.Features.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateUsersAsync(UsersInputDto usersInputDto) 
        => Ok(await Mediator.Send(new CreateUsersCommand { UsersInputDto = usersInputDto }));
}
