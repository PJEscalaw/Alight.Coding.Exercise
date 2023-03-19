using Business.Dtos.Inputs;
using Business.Features.Users.Commands;
using Domain.Entities;

namespace Unit.Features.Users.Commands;

public static class TestData
{
    public static CreateUsersCommand CreateUsersCommand() 
        => new()
        {
            UsersInputDto = new()
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Email = "Email@gmail.com",
                    Address = new()
                    {
                        Street = "Street",
                        City = "City",
                        PostCode = 0000
                    },
                    Employments = new List<CreateEmploymentsInputDto>()
                    {
                        new()
                        {
                            Company = "Company-A",
                            MonthsOfExperience = 10_000,
                            Salary = 20_000,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddYears(2),
                        },
                        new()
                        {
                            Company = "Company-B",
                            MonthsOfExperience = 10_000,
                            Salary = 20_000,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddYears(2),
                        }
                    }
                }
        };

    public static UsersEntity UsersEntity() 
        => new()
        {
            Id = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email@gmail.com"
        };

    public static AddressesEntity AddressesEntity()
        => new()
        {
            Street = "Street",
            City = "City",
            PostCode = 0000,
            UserId = 1,
        };

    public static List<EmploymentsEntity> EmploymentsEntities() 
        => new()
        {
            new()
            {
                Company = "Company-A",
                MonthsOfExperience = 10_000,
                Salary = 20_000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(2),
            },
            new()
            {
                Company = "Company-B",
                MonthsOfExperience = 10_000,
                Salary = 20_000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(2),
            }
        };
}
