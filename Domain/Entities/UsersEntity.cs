namespace Domain.Entities;

/// <summary>
/// Entity for Users.
/// </summary>
public class UsersEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } //MANDATORY
    public string LastName { get; set; } //MANDATORY
    public string Email { get; set; } //MANDATORY, UNIQUE
    public int AddressId { get; set; }
    public IEnumerable<int> EmploymentIds { get; set; }
}
