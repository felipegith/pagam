FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pame.Api/Pame.Api.csproj", "Pame.Api/"]
COPY ["Pame.Application/Pame.Application.csproj", "Pame.Application/"]
COPY ["Pame.Domain/Pame.Domain.csproj", "Pame.Domain/"]
COPY ["Pame.Infrastructure/Pame.Infrastructure.csproj", "Pame.Infrastructure/"]
RUN dotnet restore "Pame.Api/Pame.Api.csproj"
COPY . .
WORKDIR "/src/Pame.Api"
RUN dotnet build "Pame.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Pame.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pame.Api.dll"]
