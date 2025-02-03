using FluentAssertions;
using MazeSolver.Application.Interfaces;
using MazeSolver.Application.Repositories;
using MazeSolver.Domain.Models;
using NSubstitute;

namespace MazeSolver.Tests.Unit_Tests;
public class MazeRepositoryTests
{
    private readonly IMazeSolver _mazeSolver = Substitute.For<IMazeSolver>();
    private readonly MazeRepository _repository;

    public MazeRepositoryTests()
    {
        _repository = new MazeRepository(_mazeSolver);
        _mazeSolver.Solve(
        Arg.Any<char[][]>(),
        Arg.Any<(int, int)>()
        )
            .Returns((List<(int, int)>?)null);
    }

    [Fact]
    public void SolveMaze_AddsMaze()
    {
        // Arrange
        var maze = new Maze("SX");

        // Act
        _repository.SolveMaze(maze);
        var allMazes = _repository.GetMazes();

        // Assert
        allMazes.Should().HaveCount(1);
        allMazes.FirstOrDefault().Should().NotBeNull();
        allMazes.FirstOrDefault().Should().Be(maze);
    }

    [Fact]
    public void GetMazes_ReturnsAllMazes()
    {
        // Arrange
        var maze1 = new Maze("SX");
        var maze2 = new Maze("SG");
        var maze3 = new Maze("SX\nXG");

        // Act
        _repository.SolveMaze(maze1);
        _repository.SolveMaze(maze2);
        _repository.SolveMaze(maze3);
        var allMazes = _repository.GetMazes();

        // Assert
        allMazes.Should().HaveCount(3);
        allMazes.Should().ContainInOrder(maze1, maze2, maze3);
    }
}
