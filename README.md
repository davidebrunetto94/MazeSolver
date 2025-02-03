# Maze Solver API

## Prerequisites
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop) (optional)

## Local Development Setup
Start a terminal instance inside the MazeSolver.Api folder
### Restore Dependencies
```bash
dotnet restore
```

### Build the Application
```bash
dotnet build
```

### Run the Application
```bash
dotnet run
```
Take note of the port that the application is litening on
## Docker Deployment
Start a terminal inside the root folder of the project
### Build Docker Image
```bash
docker build -t maze-solver-api .
```

### Run Docker Container
```bash
docker run -d -p 8080:8080 --name maze-solver-api maze-solver-api
```

## API Documentation
Navigate to `http://localhost:{**replace_with_application_port**}/swagger/index.html` if running locally or `http://localhost:8080/swagger/index.html` if running in Docker for Swagger UI with API documentation

## Troubleshooting
- Verify .NET 8 SDK is installed
- Check Docker configuration
- Ensure port 8080 is available
