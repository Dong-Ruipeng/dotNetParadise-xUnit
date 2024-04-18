using System.Text.RegularExpressions;
using dotNetParadise.FakeTest.Mocks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sample.Api.Controllers;
using Sample.Repository.Entities;
using Sample.Repository.Repositories;
using Xunit.Abstractions;

namespace dotNetParadise.FakeTest.TestControllers;

public class TestMockStaffController
{
    private readonly ITestOutputHelper _testOutputHelper;
    public TestMockStaffController(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    [Fact]
    public async Task AddStaff_WhenCalled_ReturnNoContent()
    {
        //Arrange
        var mock = new Mock<IStaffRepository>();
       
        mock.Setup(_ => _.AddStaffAsync(It.IsAny<Staff>(), default));
        var staffController = new StaffController(mock.Object);
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
        var mock = new Mock<IStaffRepository>();
        var id = 1;
        mock.Setup(_ => _.GetStaffByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(() => new Staff()
        {
            Id = id,
            Name = "张三",
            Age = 18,
            Email = "zhangsan@163.com",
            Created = DateTimeOffset.Now
        });

        var staffController = new StaffController(mock.Object);

        //Act
        var result = await staffController.GetStaffById(id);

        //Assert
        Assert.IsType<Results<Ok<Staff>, NotFound>>(result);
        var okResult = (Ok<Staff>)result.Result;
        Assert.Equal(id, okResult.Value?.Id);
        _testOutputHelper.WriteLine(okResult.Value?.Name);

    }

    [Fact]
    public void Test_Moq_Demo()
    {
        //Arrange
        var mock = new Mock<IFoo>();
        // ref arguments
        var instance = new Bar();
        // Only matches if the ref argument to the invocation is the same instance
        mock.Setup(foo => foo.Submit(ref instance)).Returns(true);
        {
            //匹配满足条件的值
            mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);
            //It.Is 断言
            var result = mock.Object.Add(3);
            Assert.False(result);
        }

        {
            mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Moq.Range.Inclusive))).Returns(true);
            var inRangeResult = mock.Object.Add(3);
            Assert.True(inRangeResult);
        }


        {
            mock.Setup(x => x.DoSomethingStringy(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");
            var result = mock.Object.DoSomethingStringy("a");
            Assert.Equal("foo", result);
        }

        //设置属性值
        {
            mock.Setup(foo => foo.Name).Returns("bar");
            Assert.Equal("bar", mock.Object.Name);
        }
        //SetupUp
        {
            // Arrange
            mock = new Mock<IFoo>();
            mock.SetupSet(foo => foo.Name = "foo").Verifiable();
            //Act
            mock.Object.Name = "foo";
            //Asset
            mock.Verify();
        }
        //VerifySet直接验证属性的设置操作
        {
            // Arrange
            mock = new Mock<IFoo>();
            //Act
            mock.Object.Name = "foo";
            //Asset
            mock.VerifySet(person => person.Name = "foo");
        }

        {
            // Arrange
            mock = new Mock<IFoo>();
            // start "tracking" sets/gets to this property
            mock.SetupProperty(f => f.Name);

            // alternatively, provide a default value for the stubbed property
            mock.SetupProperty(f => f.Name, "foo");

            //Now you can do:

            IFoo foo = mock.Object;
            // Initial value was stored
            //Asset
            Assert.Equal("foo", foo.Name);
        }
    }

    [Fact]
    public void Test_Moq_Event()
    {
        {
            var handled = false;
            var mock = new Mock<HasEvent>();
            //设置订阅行为
            mock.SetupAdd(m => m.Event += It.IsAny<Action>()).CallBase();
            // 订阅事件并设置事件处理逻辑
            Action eventHandler = () => handled = true;
            mock.Object.Event += eventHandler;
            mock.Object.RaiseEvent();
            Assert.True(handled);

            // 重置标志为 false
            handled = false;
            //  移除事件处理程序
            mock.SetupRemove(h => h.Event -= It.IsAny<Action>()).CallBase();
            // 移除事件处理程序
            mock.Object.Event -= eventHandler;
            // 再次触发事件
            mock.Object.RaiseEvent();

            // Assert -  验证事件是否被正确处理
            Assert.False(handled); // 第一次应该为 true，第二次应该为 false

        }
    }


    [Fact]
    public void Test_Moq_RaiseEvent()
    {
        {
            // Arrange
            var handled = false;
            var mock = new Mock<HasEvent>();
            //设置订阅行为
            mock.Object.Event += () => handled = true;

            //act
            mock.Raise(m => m.Event += null);
            // Assert - 验证事件是否被正确处理
            Assert.True(handled);

        }
    }


    [Fact]
    public void Test_Moq_CallBacks()
    {
        //Arrange
        var mock = new Mock<IFoo>();
        var calls = 0;
        var callArgs = new List<string>();

        {
            mock.Setup(foo => foo.DoSomething("ping"))
                .Callback(() => calls++)
               .Returns(true);

            // Act
            mock.Object.DoSomething("ping");

            // Assert
            Assert.Equal(1, calls); // 验证 DoSomething 方法被调用一次
        }

        //CallBack 捕获参数
        {
            //Arrange
            mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Callback<string>(s => callArgs.Add(s))
                .Returns(true);
            //Act
            mock.Object.DoSomething("a");
            //Asset
            // 验证参数是否被添加到 callArgs 列表中
            Assert.Contains("a", callArgs);
        }
        //SetupProperty
        {
            //Arrange
            mock = new Mock<IFoo>();
            mock.SetupProperty(foo => foo.Name);
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Callback((string s) => mock.Object.Name = s)
                .Returns(true);
            //Act
            mock.Object.DoSomething("a");
            // Assert
            Assert.Equal("a", mock.Object.Name);
        }


    }

    [Fact]
    public void Test_Moq_Verify()
    {

        {
            //Arrange
            var mock = new Mock<IFoo>();
            //Act
            mock.Object.Add(1);
            // Assert
            mock.Verify(foo => foo.Add(1));
        }


        {
            var mock = new Mock<IFoo>();
            mock.Verify(foo => foo.DoSomething("ping"), Times.Never());
        }

        {
            var mock = new Mock<IFoo>();
            mock.VerifyGet(foo => foo.Name);
        }
    }

    [Fact]
    public void TestStrictMockBehavior_WithUnsetExpectation()
    {
        // Arrange
        var mock = new Mock<IFoo>(MockBehavior.Strict);
        //mock.Setup(_ => _.Add(It.IsAny<int>())).Returns(true);
        // Act & Assert
        Assert.Throws<MockException>(() => mock.Object.Add(3));
    }


    [Fact]
    public void TestPartialMockWithCallBase()
    {
        // Arrange
       var mock = new Mock<UserBase> { CallBase = true };
        mock.As<IUser>().Setup(foo => foo.GetName()).Returns("MockName");
        // Act
        string result = mock.Object.GetName();//调用模拟对象的 GetName() 方法，此时基类的实现被调用，返回值为 "BaseName"。

        // Assert
        Assert.Equal("BaseName", result);

        //Act
        var valueOfSetupMethod = ((IUser)mock.Object).GetName();//通过强制类型转换将模拟对象转换为 IUser 接口类型，调用接口方法 GetName()，返回值为 "MockName"。
        //Assert
        Assert.Equal("MockName", valueOfSetupMethod);
    }

    [Fact]
    public void TestRecursiveMock()
    {
        // Arrange
        var mock = new Mock<IFoo> { DefaultValue = DefaultValue.Mock };

        // Act
        Bar value = mock.Object.Bar;
        var barMock = Mock.Get(value);
        barMock.Setup(b => b.Submit()).Returns(true);

        // Assert
        Assert.True(mock.Object.Bar.Submit());
    }

    [Fact]
    public void TestRepositoryMock()
    {
        // Create a MockRepository with MockBehavior.Strict and DefaultValue.Mock
        var repository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };

        // Create a mock using the repository settings
        var fooMock = repository.Create<IFoo>();

        // Create a mock overriding the repository settings with MockBehavior.Loose
        var barMock = repository.Create<Bar>(MockBehavior.Loose);

        // Verify all verifiable expectations on all mocks created through the repository
        repository.Verify();

        // Additional setup and assertions can be done on fooMock and barMock as needed
        // For example:
        barMock.Setup(b => b.Submit()).Returns(true);
        Assert.True(barMock.Object.Submit());
    }


    [Fact]
    public void TestSetupSequence()
    {
        // Arrange
        var mock = new Mock<IFoo>();
        mock.SetupSequence(f => f.GetCount())
            .Returns(3)
            .Returns(2)
            .Returns(1)
            .Returns(0)
            .Throws(new InvalidOperationException());

        // Act & Assert
        Assert.Equal(3, mock.Object.GetCount());
        Assert.Equal(2, mock.Object.GetCount());
        Assert.Equal(1, mock.Object.GetCount());
        Assert.Equal(0, mock.Object.GetCount());

        Assert.Throws<InvalidOperationException>(() => mock.Object.GetCount());
    }


}


public class HasEvent
{
    public virtual event Action Event;

    public void RaiseEvent() => this.Event?.Invoke();
}


public interface IUser
{
    string GetName();
}

public class UserBase : IUser
{
    public virtual string GetName()
    {
        return "BaseName";
    }

    string IUser.GetName() => "Name";
}

