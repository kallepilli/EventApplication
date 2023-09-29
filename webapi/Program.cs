using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories;
using webapi.Repositories.Interfaces;
using webapi.Services;
using webapi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the database connection.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddTransient<IBaseRepository<Event>, EventRepository>();
builder.Services.AddTransient<IEventService<Event, EventDTO>, EventService>();

builder.Services.AddTransient<IEventParticipantRepository<EventParticipant>, EventParticipantRepository>();
builder.Services.AddTransient<IEventParticipantService<EventParticipant, EventParticipantDTO>, EventParticipantService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        dbContext?.Database.EnsureDeleted();
        dbContext?.Database.EnsureCreated();
    }

}

app.UseCors(policy => policy
    .WithOrigins("https://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
