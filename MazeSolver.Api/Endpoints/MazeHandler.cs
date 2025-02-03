using FluentValidation;
using MazeSolver.Api.Mappers;
using MazeSolver.Api.Models;
using MazeSolver.Application.Interfaces;
using MazeSolver.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MazeSolver.Api.Endpoints;


public static class MazeHandler
{
    /// <summary>
    /// Processes a maze, validates it, attempts to solve it, and returns the solution if any.
    /// </summary>
    /// <param name="request">The maze request containing the maze grid.</param>
    /// <param name="validator">The validator used to check if the maze is valid.</param>
    /// <param name="mazeRepository">The repository that processes and solves the maze.</param>
    /// <returns>
    /// - <see cref="Results.Ok(object)"/> if the maze is solved successfully
    /// - <see cref="Results.BadRequest"/> if the request is null or the maze is unsolvable.
    /// - <see cref="Results.ValidationProblem"/> if the maze fails validation.
    /// </returns>
    public static async Task<IResult> ProcessMazeAsync(MazeRequest request,
        [FromServices] IValidator<Maze> validator,
        [FromServices] IMazeRepository mazeRepository)
    {
        if (request == null)
        {
            return Results.BadRequest();
        }

        // Validate request
        var maze = new Maze(request.Maze);
        var validationResult = await validator.ValidateAsync(maze);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        mazeRepository.SolveMaze(maze);
        if (maze.Solution is null)
        {
            return Results.BadRequest("Maze is not solvable");
        }

        return Results.Ok(MazeMappers.MapSolutionToCoordinate(maze.Solution));
    }
}