namespace Domain.Entities;

/// <summary>
/// Entity for Addresses.
/// </summary>
public class AddressesEntity
{
    public int Id { get; set; }
    public string Street { get; set; } //MANDATORY        
    public string City { get; set; } //MANDATORY
    public int? PostCode { get; set; }
}
