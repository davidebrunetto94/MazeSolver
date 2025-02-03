using FluentValidation;
using MazeSolver.Domain.Models;

namespace MazeSolver.Api.Models;

public class MazeValidator : AbstractValidator<Maze>
{
    public MazeValidator()
    {
        RuleFor(x => x.Grid)
            .NotEmpty().WithMessage("Maze cannot be empty.") // Maze cannot be empty
            .Must(HasSingleStartAndGoal).WithMessage("Maze must contain exactly one 'S' and one 'G'.") // Maze must have only one start and one end
            .Must(IsValidSize).WithMessage("Maze must be at most 20x20 in size."); // Cannot be bigger than 20x20
    }

    private bool HasSingleStartAndGoal(char[][] mazeGrid)
    {
        int sCounter = 0;
        int gCounter = 0;

        for (int i = 0; i < mazeGrid.Length; i++)
        {
            for (int j = 0; j < mazeGrid[i].Length; j++)
            {
                if (mazeGrid[i][j] == 'S')
                {
                    if (sCounter > 0) return false;
                    sCounter++;
                }
                if (mazeGrid[i][j] == 'G')
                {
                    if (gCounter > 0) return false;
                    gCounter++;
                }
            }
        }

        return sCounter == 1 && gCounter == 1;
    }

    private bool IsValidSize(char[][] mazeGrid)
    {
        const int maxDim = 20;

        if (mazeGrid.Length > maxDim) return false;

        for (int i = 0; i < mazeGrid.Length; i++)
        {
            int columnsInRow = mazeGrid[i].Length;
            if (columnsInRow > maxDim) return false;
        }
        return true;
    }
}