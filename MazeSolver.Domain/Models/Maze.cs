namespace MazeSolver.Domain.Models;
public class Maze
{
    public char[][] Grid { get; init; }
    public List<(int, int)>? Solution { get; set; }

    public Maze(string mazeString)
    {
        // Get each line from the string
        var lines = mazeString.Split('\n');

        // Split each line into a char array to get grid
        Grid = lines.Select(x => x.ToCharArray()).ToArray();
    }
}
