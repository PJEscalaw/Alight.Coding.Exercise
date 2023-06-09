﻿namespace Business.Dtos.Inputs.Base;

public class EmploymentInputBaseDto
{
    public string Company { get; set; }
    public int? MonthsOfExperience { get; set; }
    public int? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
