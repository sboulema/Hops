FROM microsoft/dotnet:1.1.0-sdk-msbuild
 
COPY src/Hops/ /dotnetapp
WORKDIR /dotnetapp

RUN dotnet restore
 
EXPOSE 5000/tcp
ENTRYPOINT dotnet run --server.urls http://0.0.0.0:5000
