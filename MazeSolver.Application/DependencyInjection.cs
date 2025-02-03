using MazeSolver.Application.Interfaces;
using MazeSolver.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MazeSolver.Application;
public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IMazeRepository, MazeRepository>();
        services.AddSingleton<IMazeSolver, MazeSolver>();

        return services;
    }
}