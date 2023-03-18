using Business.Dtos.Inputs.Base;

namespace Business.Dtos.Inputs;

public class CreateUsersInputDto : UsersInputBaseDto 
{
    public AddressesInputDto Address { get; set; }
    public IEnumerable<CreateEmploymentsInputDto> Employments { get; set; }
}
