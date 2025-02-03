namespace MazeSolver.Application.Interfaces;
public interface IMazeSolver
{
    List<(int, int)>? Solve(char[][] mazeGrid, (int, int) actual, List<(int, int)>? mazePath = null, Dictionary<(int, int), int>? seen = null, int backtrackCounter = 0);
}
