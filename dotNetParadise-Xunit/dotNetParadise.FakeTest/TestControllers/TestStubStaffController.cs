using dotNetParadise.FakeTest.Stubs;
using Microsoft.AspNetCore.Http.HttpResults;
using Sample.Api.Controllers;
using Sample.Repository.Entities;

namespace dotNetParadise.FakeTest.TestControllers;

public class TestStubStaffController
{

    [Fact]
    public async Task AddStaff_WhenCalled_ReturnNoContent()
    {
        //Arrange
        var staffController = new StaffController(new StubStaffRepository());
        var staff = new Staff()
        {
            Age = 10,
            Name = "Test",
            Email = "Test@163.com",
            Created = DateTimeOffset.Now,
        };
        //Act
        var result = await staffController.AddStaff(staff);

        //Assert
        Assert.IsType<Results<NoContent, ProblemHttpResult>>(result);
    }

    [Fact]
    public async Task GetStaffById_WhenCalled_ReturnOK()
    {
        //Arrange
        var staffController = new StaffController(new StubStaffRepository());
        var id = 1;
        //Act
        var result = await staffController.GetStaffById(id);

        //Assert
        Assert.IsType<Results<Ok<Staff>, NotFound>>(result);
        var okResult = (Ok<Staff>)result.Result;
        Assert.Equal(id, okResult.Value?.Id);
    }
    
      //先暂时省略后面测试方法....

}
