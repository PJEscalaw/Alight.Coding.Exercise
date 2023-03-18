namespace Business.Dtos.Outputs;

public class UsersOutputDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; } 
    public AddressesOutputDto Address { get; set; }
    public IEnumerable<EmploymentsOutputDto> Employments { get; set; }
}
