namespace Business.Dtos.Inputs;

public class EmploymentsInputDto
{
    public string Company { get; set; }
    public int? MonthsOfExperience { get; set; } //MANDATORY
    public int? Salary { get; set; } //MANDATORY
    public DateTime? StartDate { get; set; } //MANDATORY
    public DateTime? EndDate { get; set; }
}
