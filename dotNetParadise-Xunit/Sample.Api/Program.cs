using System.Reflection.Metadata;
using Sample.Repository.Contexts;
using Sample.Repository.Extensions;
using Sample.Repository.SeedData;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddEFCoreInMemoryAndRepository();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.Run();


