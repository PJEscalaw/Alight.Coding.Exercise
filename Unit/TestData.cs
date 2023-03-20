using Business.Dtos.Inputs;
using Business.Dtos.Outputs;
using Business.Features.Users.Commands;
using Business.Features.Users.Queries;
using Domain.Entities;
using Mapster;

namespace Unit;

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

    public static CreateUsersCommand CreateUsersCommandInvalid()
    {
        var command = CreateUsersCommand();
        command.UsersInputDto.FirstName = string.Empty;
        command.UsersInputDto.LastName = string.Empty;
        command.UsersInputDto.Email = string.Empty;
        command.UsersInputDto.Address.City = string.Empty;
        command.UsersInputDto.Address.Street = string.Empty;
        command.UsersInputDto.Employments.ToList().ForEach(x =>
        {
            x.Company = string.Empty;
            x.MonthsOfExperience = 0;
            x.Salary = 0;
            x.StartDate = DateTime.Now;
            x.EndDate = DateTime.Now.AddYears(-10);
        });

        return command;
    }

    public static GetUsersByIdQuery GetUsersByIdQuery()
        => new GetUsersByIdQuery { Id = 1 };

    public static UpdateUsersCommand UpdateUsersCommand()
        => new ()
        {
            UsersInputDto = new()
            {
                UserId = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email@email.com",
                Address = new()
                {
                    City = "City",
                    Street = "Street",
                    PostCode = 2022
                },
                Employments = new List<UpdateEmploymentsInputDto>
                {
                     new()
                        {
                            Id = 1,
                            Company = "Company-A",
                            MonthsOfExperience = 10_000,
                            Salary = 20_000,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddYears(2),
                        },
                        new()
                        {
                            Id = 1,
                            Company = "Company-B",
                            MonthsOfExperience = 10_000,
                            Salary = 20_000,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddYears(2),
                        }
                }
            }
        };

    public static UpdateUsersCommand UpdateUsersCommandInvalid()
    {
        var command = UpdateUsersCommand();
        command.UsersInputDto.FirstName = string.Empty;
        command.UsersInputDto.LastName = string.Empty;
        command.UsersInputDto.Email = string.Empty;
        command.UsersInputDto.Address.City = string.Empty;
        command.UsersInputDto.Address.Street = string.Empty;
        command.UsersInputDto.Employments.ToList().ForEach(x =>
        {
            x.Company = string.Empty;
            x.MonthsOfExperience = 0;
            x.Salary = 0;
            x.StartDate = DateTime.Now;
            x.EndDate = DateTime.Now.AddYears(-10);
        });

        return command;
    }

    public static UsersOutputDto UsersOutputDto()
    {
        var userOutputDto = new UsersOutputDto();
        return UsersEntity().Adapt(userOutputDto);
    }

    public static AddressesOutputDto AddressesOutputDto()
    {
        var addressesDto = new AddressesOutputDto();
        return AddressesEntity().Adapt(addressesDto);
    }

    public static List<EmploymentsOutputDto> EmploymentsOutputDto()
    {
        var employmentsDto = new List<EmploymentsOutputDto>();
        return EmploymentsEntities().Adapt(employmentsDto);
    }

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
                Id = 1,
                UserId = 1,
                Company = "Company-A",
                MonthsOfExperience = 10_000,
                Salary = 20_000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(2),
            },
            new()
            {
                Id = 2,
                UserId = 1,
                Company = "Company-B",
                MonthsOfExperience = 10_000,
                Salary = 20_000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(2),
            }
        };
}
