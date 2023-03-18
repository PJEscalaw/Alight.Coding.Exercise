namespace Business.Dtos.Outputs;

public class AddressesOutputDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Street { get; set; }        
    public string City { get; set; }
    public int? PostCode { get; set; }
}
