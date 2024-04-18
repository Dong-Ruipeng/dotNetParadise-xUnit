using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Sample.Repository.Entities;

namespace dotNetParadise.IntegrationTest;

public class DefaultWebApplicationFactoryTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public DefaultWebApplicationFactoryTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAll_Query_ReturnOkAndListStaff()
    {
        //Arrange
        var httpClient = _factory.CreateClient();
        //act
        var response = await httpClient.GetAsync("/api/Staff");
        //Assert
        //校验状态码
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //校验用户
        var users = await response.Content.ReadFromJsonAsync<List<Staff>>();
        Assert.NotNull(users);
    }

    [Fact]
    public async Task GetConfig_WhenCalled_ReturnOk() {
        //Arrange
        var httpClient = _factory.CreateClient();
        //act
        var response = await httpClient.GetAsync("/GetConfig");
        //Assert
        //校验状态码
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //校验用户
        var config = await response.Content.ReadFromJsonAsync<string>();
        Assert.NotNull(config);
    }
}
