# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0.100 AS build-env

WORKDIR /app

# Combine WORKDIR and COPY to reduce layers
COPY . .

# Restore as distinct layers
RUN dotnet restore FarmBack/FarmBack.csproj

# Build and publish a release
RUN dotnet publish -c Release -o out FarmBack/FarmBack.csproj

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0.0

WORKDIR /app

# Copy only necessary files from the build stage
COPY --from=build-env /app/out .

# Expose the port the app will run on
EXPOSE 7082

# Set the entry point
ENTRYPOINT ["dotnet", "FarmBack.dll"]