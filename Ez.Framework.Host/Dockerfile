FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 9000
EXPOSE 9001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ez.Framework.Host/Ez.Framework.Host.csproj", "Ez.Framework.Host/"]
COPY ["Ez.Framework.Domain/Ez.Framework.Domain.csproj", "Ez.Framework.Domain/"]
COPY ["Ez.Framework.Application/Ez.Framework.Application.csproj", "Ez.Framework.Application/"]
COPY ["Ez.Framework.Persistence/Ez.Framework.Persistence.csproj", "Ez.Framework.Persistence/"]
RUN dotnet restore "Ez.Framework.Host/Ez.Framework.Host.csproj"
COPY . .
WORKDIR "/src/Ez.Framework.Host"
RUN dotnet build "Ez.Framework.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ez.Framework.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ez.Framework.Host.dll"]
