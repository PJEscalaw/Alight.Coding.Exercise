namespace Business.Dtos.Inputs;

public class AddressesInputDto
{
    public string? Street { get; set; } //MANDATORY        
    public string? City { get; set; } //MANDATORY
    public int? PostCode { get; set; }
}
