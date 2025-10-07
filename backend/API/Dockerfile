# Old (currently .NET 8)
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# New (use .NET 7)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "API.dll"]
