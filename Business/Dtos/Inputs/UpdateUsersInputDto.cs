using Business.Dtos.Inputs.Base;

namespace Business.Dtos.Inputs;

public class UpdateUsersInputDto : UsersInputBaseDto
{
    public int UserId { get; set; }
    public AddressesInputDto Address { get; set; }
    public IEnumerable<UpdateEmploymentsInputDto> Employments { get; set; }
}
