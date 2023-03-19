using Business.Features.Employments.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EmploymentsController : BaseController
{
    /// <summary>
    /// I'm really not sure if this is included on the requirement. 
    /// It was specified at the bottom of the document.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployment(int id)
    {
        _ = await Mediator.Send(new DeleteEmploymentsCommand { Id = id });
        return NoContent();
    }
}
