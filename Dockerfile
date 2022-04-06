FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as stageBuild

WORKDIR /app

COPY ./docker-texvn-api.csproj /app/docker-texvn-api.csproj

RUN dotnet restore

COPY . .

RUN dotnet build \
    && dotnet publish -c Release -o /build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal as stageRelease

WORKDIR /app

COPY --from=stageBuild /build ./

ENTRYPOINT [ "dotnet", "docker-texvn-api.dll" ]