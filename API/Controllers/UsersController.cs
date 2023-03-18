using Business.Dtos.Inputs;
using Business.Features.Users.Commands;
using Business.Features.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateUsersAsync(CreateUsersInputDto usersInputDto) 
        => Ok(await Mediator.Send(new CreateUsersCommand { UsersInputDto = usersInputDto }));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUsersByIdAsycn(int id) 
        => Ok(await Mediator.Send(new GetUsersByIdQuery { Id = id }));

    [HttpPut]
    public async Task<IActionResult> UpdateUsersAsync(UpdateUsersInputDto usersInputDto)
      => Ok(await Mediator.Send(new UpdateUsersCommand { UsersInputDto = usersInputDto }));
}
