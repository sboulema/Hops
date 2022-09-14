FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Hops/*.csproj ./src/Hops/
RUN dotnet restore

# copy everything else and build app
COPY src/Hops/. ./src/Hops/
WORKDIR /app/src/Hops
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY --from=build /app/src/Hops/out ./
COPY --from=build /app/src/Hops/BrewDB/brewDB.sqlite ./

ENTRYPOINT ["dotnet", "Hops.dll"]