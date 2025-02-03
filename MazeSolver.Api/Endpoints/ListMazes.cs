using MazeSolver.Api.Mappers;
using MazeSolver.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MazeSolver.Api.Endpoints;


public static class ListMazes
{
    /// <summary>
    /// Retrieves a list of previously submitted mazes along with their solutions (if available).
    /// </summary>
    /// <param name="mazeRepository">The repository used to fetch the mazes.</param>
    /// <returns>A list of mazes with their solutions.</returns>
    public static IResult List([FromServices] IMazeRepository mazeRepository)
    {
        var mazes = mazeRepository.GetMazes();
        return Results.Ok(mazes.Select(x => MazeMappers.MapMazeToResponse(x)));
    }
}
