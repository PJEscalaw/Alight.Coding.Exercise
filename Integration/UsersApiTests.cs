using Business.Dtos.Inputs;
using Business.Dtos.Outputs;
using Business.Features.Users.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Integration;

public class UsersApiTests
{
    private readonly HttpClient _httpClient;
    public UsersApiTests()
    {
        var webApplicationFactory = new WebApplicationFactory<Program>();
        _httpClient = webApplicationFactory.CreateDefaultClient();
    }

    [Test]
    public async Task CreateUser()
    {
        //arrange
        var url = "/api/Users";
        var input = new CreateUsersInputDto
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
        };

        //actual
        var response = await _httpClient.PostAsJsonAsync(url, input);
        
        //assert
        response.EnsureSuccessStatusCode();
        var createdUserId = await response.Content.ReadFromJsonAsync<int>();
        createdUserId.Should().NotBe(0);
    }

    [Test]
    public async Task GetUserById()
    {
        //arrange
        var url = "/api/Users/1";

        //actual
        var response = await _httpClient.GetAsync(url);

        //assert
        response.EnsureSuccessStatusCode();
        var retrievedUser = await response.Content.ReadFromJsonAsync<UsersOutputDto>();

        retrievedUser?.Id.Should().Be(1);
        retrievedUser.Should().NotBeNull();
    }

    [Test]
    public async Task UpdateUser()
    {
        //arrange
        var url = "/api/Users";
        var input = new UpdateUsersInputDto
        {
            UserId = 1,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test@gmai.com",
            Address = new()
            {
                Street = "Street",
                City = "City",
                PostCode = 0000
            },
            Employments = new List<UpdateEmploymentsInputDto>()
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
                            Id = 2,
                            Company = "Company-B",
                            MonthsOfExperience = 10_000,
                            Salary = 20_000,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddYears(2),
                        }
                    }
        };

        //actual
        var response = await _httpClient.PutAsJsonAsync(url, input);

        //assert
        response.EnsureSuccessStatusCode();
        var updatedUserId = response.Content.ReadFromJsonAsync<int>();
        updatedUserId.Should().NotBe(0);
    }
}
