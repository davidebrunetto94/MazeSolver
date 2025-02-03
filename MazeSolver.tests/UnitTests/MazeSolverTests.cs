using FluentAssertions;
using MazeSolver.Domain.Models;
using static System.Net.Mime.MediaTypeNames;
namespace MazeSolver.Tests;

public class MazeSolverTests
{
    private readonly Application.MazeSolver _sut = new();

    private readonly List<(Maze maze, List<(int, int)> path)> _validMazes =
    [
        (
        new Maze("S_________\n_XXXXXXXX_\n_X______X_\n_X_XXXX_X_\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"),
            new List<(int, int)>
            {
                (0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), (0, 8), (0, 9),
                (1, 9), (2, 9), (3, 9), (4, 9), (5, 9), (6, 9), (7, 9), (8, 9), (9, 9), (9,8)
            }
        ),
        (
        new Maze("S_________\nXXXXXXXXX_\nXX______X_\nXX_XXXX_X_\nXX_X__X_X_\nXX_X__X_X_\nXX_X____X_\nXX_XXXXXX_\nXX________\nXXXXXXXXG_"),
            new List<(int, int)>
            {
                (0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), (0, 8), (0, 9),
                (1, 9), (2, 9), (3, 9), (4, 9), (5, 9), (6, 9), (7, 9), (8, 9), (9, 9), (9,8)
            }
        ),
        (
        new Maze("S__\nXXG"),
            new List<(int, int)>
            {
                (0, 0), (0, 1), (0, 2), (1, 2)
            }
        )
    ];

    private readonly List<(Maze maze, List<(int, int)>? path)> _invalidMazes =
    [
        (
            new Maze("SX\nXG"),
            null
        ),
        (
            new Maze("SXX\nXGX"),
            null
        ),
        (
            new Maze("SX_\nXG_"),
            null
        )
    ];


    [Fact]
    public void ValidMazes_ReturnCorrectSolution()
    {
        foreach (var maze in _validMazes)
        {
            var solution = _sut.Solve(maze.maze.Grid, (0, 0));
            solution.Should().BeEquivalentTo(maze.path);
        }
    }

    [Fact]
    public void InvalidMazes_ReturnNull()
    {
        foreach (var maze in _invalidMazes)
        {
            var solution = _sut.Solve(maze.maze.Grid, (0, 0));
            solution.Should().BeEquivalentTo(maze.path);
        }
    }
}