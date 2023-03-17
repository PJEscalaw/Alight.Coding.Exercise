namespace Domain.Entities;

/// <summary>
/// Entity for Employments.
/// </summary>
public class EmploymentsEntity
{
    public int Id { get; set; }
    public string Company { get; set; }
    public uint MonthsOfExperience { get; set; } //MANDATORY
    public uint Salary { get; set; } //MANDATORY
    public DateTime StartDate { get; set; } //MANDATORY
    public DateTime? EndDate { get; set; }
}
