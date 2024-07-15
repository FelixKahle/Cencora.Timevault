# Copyright 2024 Cencora, All rights reserved.
#
# Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG NUGET_SOURCE
ARG GITHUB_TOKEN
WORKDIR /src
COPY ["Cencora.TimeVault/Cencora.TimeVault.csproj", "Cencora.TimeVault/"]
RUN dotnet nuget add source --username USERNAME --password $GITHUB_TOKEN --store-password-in-clear-text --name github $NUGET_SOURCE
RUN dotnet restore "./Cencora.TimeVault/Cencora.TimeVault.csproj"
COPY Cencora.TimeVault/. Cencora.TimeVault/
WORKDIR "/src/Cencora.Timevault"
RUN dotnet build "./Cencora.TimeVault.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cencora.TimeVault.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cencora.TimeVault.dll"]