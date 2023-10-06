FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
# Copy csproj and restore
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY Src /tmp
WORKDIR /tmp
RUN dotnet publish GestionTareas.sln -c Release -o /app
# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY --from=base /app .

WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "GestionTareas.Api.dll"]