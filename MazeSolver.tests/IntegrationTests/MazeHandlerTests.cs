using FluentAssertions;
using MazeSolver.Api.Mappers;
using MazeSolver.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MazeSolver.IntegrationTests;

public class MazeHandlerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MazeHandlerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProcessMazeAsync_ShouldReturnOk_WhenMazeIsSolvable()
    {
        // Arrange
        var request = new MazeRequest
        {
            Maze = "S_________\n_XXXXXXXX_\n_X______X_\n_X_XXXX_X_\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"
        };

        // Act
        var response = await _client.PostAsJsonAsync("solve", request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task ProcessMazeAsync_ShouldReturnSolution()
    {
        // Arrange
        var request = new MazeRequest
        {
            Maze = "S_________\nXXX_XXXXX_\nXX______X_\nXX__XXX_X_\nXX_X__X_X_\nXX_X__X_X_\nXX_X____X_\nXX_XXXXXX_\nXX________\nXXXXXXXXG_"
        };

        var expectedSolution = new List<(int, int)>
        {
            (0, 0), (0, 1), (0, 2), (0, 3), (1,3), (2,3), (3, 3), (3, 2), (4,2), (5,2), (6,2),
            (7,2), (8,2), (8,3), (8,4), (8,5), (8,6), (8,7), (8,8), (9,8)
        };
        // Act
        var response = await _client.PostAsJsonAsync("solve", request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var json = await response.Content.ReadAsStringAsync();
        var mazeResponse = JsonConvert.DeserializeObject<List<Coordinate>>(json);
        mazeResponse.Should().BeEquivalentTo(MazeMappers.MapSolutionToCoordinate((expectedSolution)));

    }

    [Fact]
    public async Task ProcessMazeAsync_ShouldReturnBadRequest_WhenMazeIsNotSolvable()
    {
        // Arrange
        var request = new MazeRequest
        {
            Maze = "SXG"
        };

        // Act
        var response = await _client.PostAsJsonAsync("solve", request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
