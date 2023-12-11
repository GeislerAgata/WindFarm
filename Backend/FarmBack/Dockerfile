FROM mcr.microsoft.com/dotnet/sdk:6.0  AS build-env
WORKDIR /app


COPY . ./
# Restore as distinct layers
RUN dotnet restore FarmBack/FarmBack.csproj
# Build and publish a release
RUN dotnet publish -c Release -o out FarmBack/FarmBack.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "FarmBack.dll"]