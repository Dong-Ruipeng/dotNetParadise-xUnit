using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Sample.Repository.Entities;
using Sample.Repository.Repositories;

namespace dotNetParadise.AutoFixture;

public class AutoFixtureStaffTest
{
    private readonly IFixture _fixture;
    public AutoFixtureStaffTest()
    {
        _fixture = new Fixture();
        _fixture.Customize<Staff>(composer => composer.With(x => x.Email, "zhangsan@163.com"));
    }

    [Fact]
    public void Staff_SetProperties_ValuesAssignedCorrectly()
    {
        //Arrange
        Staff staff = new Staff();
        //生成Int类型
        staff.Id = _fixture.Create<int>();
        //生成string 类型
        staff.Name = _fixture.Create<string>();
        //生成DateTimeOffset类型
        staff.Created = _fixture.Create<DateTimeOffset>();
        //生成 List<string>?
        staff.Addresses = _fixture.CreateMany<string>(Random.Shared.Next(1, 100)).ToList();
        //Act
        //...省略
        // Assert
        Assert.NotNull(staff); // 验证 staff 对象不为 null

        // 验证 staff.Id 是 int 类型
        Assert.IsType<int>(staff.Id);

        // 验证 staff.Name 是 string 类型
        Assert.IsType<string>(staff.Name);

        // 验证 staff.Created 是 DateTimeOffset? 类型
        Assert.IsType<DateTimeOffset>(staff.Created);

        // 验证 staff.Addresses 是 List<string> 类型
        Assert.IsType<List<string>>(staff.Addresses);

        // 验证 staff.Addresses 不为 null
        Assert.NotNull(staff.Addresses);

        // 验证 staff.Addresses 中的元素数量在 1 到 100 之间
        Assert.InRange(staff.Addresses.Count, 1, 100);

    }

    [Fact]
    public void Staff_ObjectCreation_ValuesAssignedCorrectly()
    {
        // Arrange
        Staff staff = _fixture.Create<Staff>(); // 使用 AutoFixture 直接创建 Staff 对象

        // Act
        //...省略

        // Assert
        Assert.NotNull(staff); // 验证 staff 对象不为 null

        // 验证 staff.Id 是 int 类型
        Assert.IsType<int>(staff.Id);

        // 验证 staff.Name 是 string 类型
        Assert.IsType<string>(staff.Name);

        // 验证 staff.Created 是 DateTimeOffset? 类型
        Assert.IsType<DateTimeOffset>(staff.Created);

        // 验证 staff.Addresses 是 List<string> 类型
        Assert.IsType<List<string>>(staff.Addresses);

        // 验证 staff.Addresses 不为 null
        Assert.NotNull(staff.Addresses);

        // 验证 staff.Addresses 中的元素数量在 1 到 100 之间
        Assert.InRange(staff.Addresses.Count, 1, 100);
    }



    [Theory, AutoData]
    public void Staff_Constructor_InitializesPropertiesCorrectly(
        int id, string name, string email, int? age, List<string> addresses, DateTimeOffset? created)
    {
        // Act
        var staff = new Staff { Id = id, Name = name, Email = email, Age = age, Addresses = addresses, Created = created };

        // Assert
        Assert.Equal(id, staff.Id);
        Assert.Equal(name, staff.Name);
        Assert.Equal(email, staff.Email);
        Assert.Equal(age, staff.Age);
        Assert.Equal(addresses, staff.Addresses);
        Assert.Equal(created, staff.Created);
    }

    [Theory]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
    [InlineAutoData(3)]
    [InlineAutoData]
    public void Staff_ConstructorByInlineData_InitializesPropertiesCorrectly(
     int id, string name, string email, int? age, List<string> addresses, DateTimeOffset? created)
    {
        // Act
        var staff = new Staff { Id = id, Name = name, Email = email, Age = age, Addresses = addresses, Created = created };

        // Assert
        Assert.Equal(id, staff.Id);
        Assert.Equal(name, staff.Name);
        Assert.Equal(email, staff.Email);
        Assert.Equal(age, staff.Age);
        Assert.Equal(addresses, staff.Addresses);
        Assert.Equal(created, staff.Created);
    }


    [Fact]
    public void Staff_SetCustomValue_ShouldCorrectly()
    {
        var staff = _fixture.Build<Staff>()
            .With(_ => _.Name, "Ruipeng")
            .Create();
        Assert.Equal("Ruipeng", staff.Name);
    }


    [Fact]
    public void Test_DisableAutoProperties()
    {
        // Arrange
        var fixture = new Fixture();
        var sut = fixture.Build<Staff>()
                         .OmitAutoProperties()
                         .Create();

        // Assert
        Assert.Equal(0, sut.Id); // 验证 Id 属性为默认值 0
        Assert.Null(sut.Name); // 验证 Name 属性为 null
        Assert.Null(sut.Email); // 验证 Email 属性为 null
        Assert.Null(sut.Age); // 验证 Age 属性为 null
        Assert.Null(sut.Addresses); // 验证 Addresses 属性为 null
        Assert.Null(sut.Created); // 验证 Created 属性为 null
    }


    [Fact]
    public void Test_UpdateMethod()
    {
        // Arrange
        var fixture = new Fixture();
        var staff1 = fixture.Create<Staff>();
        var staff2 = fixture.Create<Staff>();

        // 使用 Do 方法执行自定义操作
        var staff3 = fixture.Build<Staff>()
                                  .Do(x => staff1.Update(staff2))
                                  .Create();

        // Assert
        Assert.Equal(staff2.Name, staff1.Name); // 验证 Name 是否更新
        Assert.Equal(staff2.Email, staff1.Email); // 验证 Email 是否更新
        Assert.Equal(staff2.Age, staff1.Age); // 验证 Age 是否更新
        Assert.Equal(staff2.Addresses, staff1.Addresses); // 验证 Addresses 是否更新
        Assert.Equal(staff2.Created, staff1.Created); // 验证 Created 是否更新
    }


    [Fact]
    public void Test_StaffNameIsJohnDoe()
    {
        // Arrange
        Staff staff = _fixture.Create<Staff>();

        // Act

        // Assert
        Assert.Equal("zhangsan@163.com", staff.Email);
    }



    [Fact]
    public async Task Repository_Add_ShouleBeSuccess()
    {
        _fixture.Customize(new AutoMoqCustomization());
        var repoMock = _fixture.Create<IStaffRepository>();
        Assert.NotNull(repoMock);

        var staff = _fixture.Create<Staff>();

        await repoMock.AddStaffAsync(staff, default);
    }

}
