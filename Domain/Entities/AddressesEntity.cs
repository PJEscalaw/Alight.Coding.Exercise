using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Addresses")]
public class AddressesEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Street { get; set; }       
    public string City { get; set; }
    public int? PostCode { get; set; }
}
