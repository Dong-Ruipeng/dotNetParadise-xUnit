using System.Net;
using System.Net.Http.Json;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Repository.Contexts;
using Sample.Repository.Entities;
using Sample.Repository.SeedData;

namespace dotNetParadise.IntegrationTest;

public class SampleApiTest(SampleApiWebAppFactory factory) : IClassFixture<SampleApiWebAppFactory>
{

    [Fact]
    public async Task GetAll_Query_ReturnOkAndListStaff()
    {
        //Arrange
        var httpClient = factory.CreateClient();
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
    public async Task GetConfig_WhenCalled_ReturnOk()
    {
        //Arrange
        var httpClient = factory.CreateClient();
        //act
        var response = await httpClient.GetAsync("/GetConfig");
        //Assert
        //校验状态码
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //校验用户
        var config = await response.Content.ReadFromJsonAsync<string>();
        Assert.NotNull(config);
    }

    // 后面测试暂时省略。。。。
}
