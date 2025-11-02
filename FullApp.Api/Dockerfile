# src/FullApp.Api/Dockerfile

# Stage 1: Build the application (uses the .NET SDK image)
# This multi-stage build significantly reduces the final image size.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy the Solution file (.sln) and all Project files (*.csproj)
# We copy them first to allow NuGet restore to be cached efficiently.
# The '..' path is used because the Dockerfile is inside src/FullApp.Api, 
# and the solution file is in the root directory (../.. from WORKDIR /src).
COPY FullApp.sln .
COPY src/FullApp.Api/FullApp.Api.csproj src/FullApp.Api/
COPY src/FullApp.Core/FullApp.Core.csproj src/FullApp.Core/
COPY src/FullApp.Infrastructure/FullApp.Infrastructure.csproj src/FullApp.Infrastructure/
COPY src/FullApp.Shared/FullApp.Shared.csproj src/FullApp.Shared/

# 2. Restore NuGet dependencies for all projects in the solution
RUN dotnet restore FullApp.sln

# 3. Copy the rest of the source code
# Note: The context (where the build is run from) must be the root of the solution.
COPY . .

# 4. Publish the API project (build in Release mode)
# This command builds the entire solution and publishes the entry point project.
WORKDIR /src/src/FullApp.Api
RUN dotnet publish -c Release -o /app/publish

# ---

# Stage 2: Runtime image (uses the minimal ASP.NET runtime image)
# This smaller image is used for running the application in production.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# 5. Copy the published output from the build stage
COPY --from=build /app/publish .

# 6. Set environment variable to indicate the app is running inside a container
ENV DOTNET_RUNNING_IN_CONTAINER=true

# 7. Expose the port Kestrel is running on (typically 8080 in .NET 8 containers)
EXPOSE 8080 
# Note: Kestrel runs on port 8080 by default in containers since .NET 8

# 8. Define the entry point to start the API
ENTRYPOINT ["dotnet", "FullApp.Api.dll"]