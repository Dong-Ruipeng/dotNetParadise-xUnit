using Sample.Repository.Contexts;
using Sample.Repository.Extensions;
using Sample.Repository.SeedData;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddEFCoreInMemoryAndRepository();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<SampleDbContext>())
{
    context.Database.EnsureCreated();
    FakeData.Init(1000);
    await context.Staffs.AddRangeAsync(FakeData.Staffs);
    await context.SaveChangesAsync();
}
app.MapGet("GetConfig", IResult () =>
{
    return TypedResults.Ok(app.Configuration["Config"]);
});
app.Run();


public partial class Program { }