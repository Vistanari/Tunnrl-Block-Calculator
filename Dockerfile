# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TunnelBlockCalculator.csproj ./
RUN dotnet restore TunnelBlockCalculator.csproj

COPY . ./
RUN dotnet publish TunnelBlockCalculator.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TunnelBlockCalculator.dll"]
