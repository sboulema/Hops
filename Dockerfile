FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Hops/*.csproj ./Hops/
RUN dotnet restore

# copy everything else and build app
COPY src/Hops/. ./Hops/
WORKDIR /app/Hops
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY --from=build /app/Hops/out ./

ENTRYPOINT ["dotnet", "Hops.dll"]