using FluentAssertions;
using MazeSolver.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace MazeSolver.Tests.IntegrationTests;
public class ListMazesTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ListMazesTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ListMazes_ShouldReturnOk()
    {
        // Arrange
        var request = new MazeRequest
        {
            Maze = "S_________\n_XXXXXXXX_\n_X______X_\n_X_XXXX_X_\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"
        };
        await _client.PostAsJsonAsync("solve", request);

        // Act
        var response = await _client.GetAsync("list");
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}