using API;
using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddWebAPI()
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure()
    .AddMemoryCache();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var services = app.Services.CreateScope())
{
    var context = services.ServiceProvider.GetService<DataContext>();
    services.ServiceProvider.GetRequiredService<IDbInitializer>().Initilize();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();