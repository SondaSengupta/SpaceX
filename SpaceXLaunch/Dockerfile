FROM microsoft/aspnetcore:2.0-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-sac2016 AS build
WORKDIR /src
COPY SpaceXLaunch/Api.csproj SpaceXLaunch/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
RUN dotnet restore SpaceXLaunch/Api.csproj
COPY . .
WORKDIR /src/SpaceXLaunch
RUN dotnet build Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.dll"]
