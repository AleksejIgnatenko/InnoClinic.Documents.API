# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["InnoClinic.Documents.API/InnoClinic.Documents.API.csproj", "InnoClinic.Documents.API/"]
COPY ["InnoClinic.Documents.Application/InnoClinic.Documents.Application.csproj", "InnoClinic.Documents.Application/"]
COPY ["InnoClinic.Documents.Core/InnoClinic.Documents.Core.csproj", "InnoClinic.Documents.Core/"]
COPY ["InnoClinic.Documents.DataAccess/InnoClinic.Documents.DataAccess.csproj", "InnoClinic.Documents.DataAccess/"]
COPY ["InnoClinic.Documents.Infrastructure/InnoClinic.Documents.Infrastructure.csproj", "InnoClinic.Documents.Infrastructure/"]

RUN dotnet restore "InnoClinic.Documents.API/InnoClinic.Documents.API.csproj"

COPY . .

WORKDIR "/src/InnoClinic.Documents.API"
RUN dotnet build "InnoClinic.Documents.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "InnoClinic.Documents.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.Documents.API.dll"]