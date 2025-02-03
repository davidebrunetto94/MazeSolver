using MazeSolver.Api.Models;
using MazeSolver.Domain.Models;

namespace MazeSolver.Api.Mappers;

public class MazeMappers
{
    public static List<Coordinate> MapSolutionToCoordinate(List<(int, int)>? solution)
    {
        return (solution ?? []).Select(c => new Coordinate { X = c.Item1, Y = c.Item2 }).ToList();
    }

    public static string MapGridToResponse(char[][] grid)
    {
        var stringMaze = string.Empty;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                stringMaze += grid[i][j];
            }
            stringMaze += "\n";
        }
        return stringMaze.TrimEnd();
    }

    public static MazeResponse MapMazeToResponse(Maze maze)
    {
        return new MazeResponse
        {
            Maze = MapGridToResponse(maze.Grid),
            Solution = MapSolutionToCoordinate(maze.Solution)
        };
    }
}
