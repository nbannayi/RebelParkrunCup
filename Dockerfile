# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything into container
COPY . .

# Restore dependencies
RUN dotnet restore RebelParkrunCup.Server/RebelParkrunCup.Server.csproj

# Publish the app (includes Client and Shared)
RUN dotnet publish RebelParkrunCup.Server/RebelParkrunCup.Server.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expose port 8080 for Cloud Run
EXPOSE 8080

# Copy published output from build stage
COPY --from=build /app/publish .

# Copy the database file
COPY RebelParkrunCup.Server/parkrun.db /app/parkrun.db

# Run the app
ENTRYPOINT ["dotnet", "RebelParkrunCup.Server.dll"]
