using MazeSolver.Application.Interfaces;
using MazeSolver.Domain.Models;

namespace MazeSolver.Application.Repositories;
public class MazeRepository : IMazeRepository
{
    private readonly List<Maze> _mazes;
    private readonly IMazeSolver _mazeSolver;

    public MazeRepository(IMazeSolver solver)
    {
        _mazes = [];
        _mazeSolver = solver;
    }

    private void AddMaze(Maze maze)
    {
        _mazes.Add(maze);
    }

    public List<Maze> GetMazes()
    {
        return _mazes;
    }

    public void SolveMaze(Maze maze)
    {
        var solution = _mazeSolver.Solve(maze.Grid, (0, 0));
        maze.Solution = solution;

        // Add it to the repo
        AddMaze(maze);
    }
}
