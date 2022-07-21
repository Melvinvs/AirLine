using LoginService.Config;
using LoginService.Entity;
using LoginService.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddScoped<IdataRepository, DataRepository>();
builder.Services.AddDbContext<LoginDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddConsulConfig();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseMvc();
}

app.MapPost("/login", ([FromServices] IdataRepository db, User user) =>
{
    return db.Register(user);
});
app.UseConsul();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
