using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Employments")]
public class EmploymentsEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Company { get; set; }
    public uint MonthsOfExperience { get; set; }
    public uint Salary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
