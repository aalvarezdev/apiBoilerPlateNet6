FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore Backend.Api/Backend.Api.csproj
RUN dotnet publish Backend.Api/Backend.Api.csproj -c Release --no-restore -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
ARG BUILD_COMMIT=local
ARG BUILD_NUMBER=local
ARG BUILD_EXECUTION_ID=local
ARG BUILD_PIPELINE_URL=""
ENV BUILD_COMMIT=$BUILD_COMMIT \
    BUILD_NUMBER=$BUILD_NUMBER \
    BUILD_EXECUTION_ID=$BUILD_EXECUTION_ID \
    BUILD_PIPELINE_URL=$BUILD_PIPELINE_URL
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "Backend.Api.dll"]
