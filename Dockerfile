#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim-arm64v8 AS base

# Create the user and group for raspberry
#RUN addgroup --gid 1000 app && adduser --uid 1000 --gid 1000 --home /app --disabled-password --gecos "" app

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MealPlanner.API/MealPlanner.API.csproj", "MealPlanner.API/"]
COPY ["MealPlanner.Application/MealPlanner.Application.csproj", "MealPlanner.Application/"]
COPY ["MealPlanner.Domain/MealPlanner.Domain.csproj", "MealPlanner.Domain/"]
COPY ["MealPlanner.Infrastructure/MealPlanner.Infrastructure.csproj", "MealPlanner.Infrastructure/"]
RUN dotnet restore "./MealPlanner.API/MealPlanner.API.csproj"
COPY . .
WORKDIR "/src/MealPlanner.API"
RUN dotnet build "./MealPlanner.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MealPlanner.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MealPlanner.API.dll"]