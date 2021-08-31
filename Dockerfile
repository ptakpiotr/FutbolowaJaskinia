FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /app

COPY FutbolowaJaskinia.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as release
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","FutbolowaJaskinia.dll"]
