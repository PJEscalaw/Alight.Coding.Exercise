namespace Business.Dtos.Inputs;

public class UsersInputDto
{
    public string? FirstName { get; set; } //MANDATORY
    public string? LastName { get; set; } //MANDATORY
    public string? Email { get; set; } //MANDATORY, UNIQUE
    public AddressesInputDto Address { get; set; }
    public IEnumerable<EmploymentsInputDto> Employments { get; set; }
}
