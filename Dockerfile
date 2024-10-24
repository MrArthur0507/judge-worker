
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

RUN apk add --no-cache docker

WORKDIR /src

COPY ./ ./

RUN dotnet restore

RUN dotnet build --configuration Release --output /app/build

RUN dotnet publish --configuration Release --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

WORKDIR /app
RUN apk add --no-cache docker-cli
COPY --from=build /app/publish ./


CMD ["sh", "-c", "dotnet JudgeWorker.dll"]



