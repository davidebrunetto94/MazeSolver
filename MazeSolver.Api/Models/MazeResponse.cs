namespace MazeSolver.Api.Models;

public class MazeResponse
{
    public string Maze { get; set; }
    public List<Coordinate> Solution { get; set; }
}

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
}