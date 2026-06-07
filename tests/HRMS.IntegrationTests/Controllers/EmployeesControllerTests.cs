using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Xunit;
namespace HRMS.IntegrationTests.Controllers;
public class EmployeesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public EmployeesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturn401_WhenNotAuthenticated()
    {
        var response = await _client.GetAsync("/api/v1/employees");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
