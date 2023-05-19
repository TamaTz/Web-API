using API.Contexts;
using API.Contracts;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext to Sql Server
var connectionString = builder.Configuration
                              .GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString));

// Add Repository to the container.
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
