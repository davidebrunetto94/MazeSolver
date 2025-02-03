using FluentValidation;
using MazeSolver.Api.Endpoints;
using MazeSolver.Api.Models;
using MazeSolver.Application;
using MazeSolver.Domain.Models;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.RegisterApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Maze Solver API",
        Version = "v1",
        Description = "An API for submitting and solving mazes."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Validation
builder.Services.AddScoped<IValidator<Maze>, MazeValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/solve", MazeHandler.ProcessMazeAsync)
.ProducesProblem(statusCode: (int)HttpStatusCode.BadRequest)
.WithName("Solve Maze")
.WithOpenApi();

app.MapGet("/list", ListMazes.List)
.Produces<List<Maze>?>()
.WithName("List mazes")
.WithOpenApi();

app.Run();
public partial class Program { }
