using MazeSolver.Application.Interfaces;

namespace MazeSolver.Application;
internal class MazeSolver : IMazeSolver
{
    // All possible moves
    readonly int[][] directions =
    [
        [-1, 0], // Up
            [1, 0],  // Down
            [0, -1], // Left
            [0, 1]   // Right
    ];

    public List<(int, int)>? Solve(char[][] mazeGrid, (int, int) actual, List<(int, int)>? mazePath = null, Dictionary<(int, int), int>? seen = null, int backtrackCounter = 0)
    {
        // Initialize to empty list if null path
        mazePath ??= [];
        // Initialize to empty dict if seen none
        seen ??= [];

        mazePath.Add(actual);

        // Update seen
        seen.Add(actual, 1);

        // Check if G reached
        if (mazeGrid[actual.Item1][actual.Item2] == 'G')
        {
            return mazePath;
        }

        // Try possible moves
        for (int k = 0; k < directions.Length; k++)
        {
            var newCol = actual.Item1 + directions[k][0];
            var newRow = actual.Item2 + directions[k][1];
            var newPos = (newCol, newRow);

            // Check if move is viable and is not seen before
            if (IsViableMove(mazeGrid, newCol, newRow) && !seen.ContainsKey(newPos))
            {
                backtrackCounter++;
                var path = Solve(mazeGrid, (newCol, newRow), mazePath, seen, backtrackCounter);
                if (path != null)
                {
                    return path;
                }
            }
        }

        // If no move is viable there is no path, remove the last element from the path
        mazePath.RemoveAt(mazePath.Count - 1);
        return null;
    }

    // Checks both if we're out of bounds and if it's a wall
    private static bool IsViableMove(char[][] mazeGrid, int i, int j)
    {
        return i >= 0 && i < mazeGrid.Length &&
               j >= 0 && j < mazeGrid[i].Length &&
               mazeGrid[i][j] != 'X';
    }

}
