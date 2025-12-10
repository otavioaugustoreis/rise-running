
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RiseRunning-ScannerCode/RiseRunning-ScannerCode.csproj", "RiseRunning-ScannerCode/"]
RUN dotnet restore "./RiseRunning-ScannerCode/RiseRunning-ScannerCode.csproj"
COPY . .
WORKDIR "/src/RiseRunning-ScannerCode"
RUN dotnet build "./RiseRunning-ScannerCode.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RiseRunning-ScannerCode.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN mkdir -p /app/Data
RUN chown -R app:app /app/Data

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RiseRunning-ScannerCode.dll"]