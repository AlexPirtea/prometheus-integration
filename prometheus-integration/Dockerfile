FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY prometheus-integration/prometheus-integration.csproj prometheus-integration/
RUN dotnet restore "prometheus-integration/prometheus-integration.csproj"
COPY . .
WORKDIR "/src/prometheus-integration"
RUN dotnet build "prometheus-integration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "prometheus-integration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "prometheus-integration.dll"]
