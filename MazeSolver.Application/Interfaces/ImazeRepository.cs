using MazeSolver.Domain.Models;

namespace MazeSolver.Application.Interfaces;

public interface IMazeRepository
{
    List<Maze> GetMazes();
    void SolveMaze(Maze maze);

}
