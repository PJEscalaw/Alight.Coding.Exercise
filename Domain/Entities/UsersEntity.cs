using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Users")]
public class UsersEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } //MANDATORY
    public string LastName { get; set; } //MANDATORY
    public string Email { get; set; } //MANDATORY, UNIQUE
}
