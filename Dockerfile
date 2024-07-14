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
WORKDIR /src
COPY ["Cencora.Timevault/Cencora.Timevault.csproj", "Cencora.Timevault/"]
RUN dotnet restore "./Cencora.Timevault/Cencora.Timevault.csproj"
COPY Cencora.Timevault/. Cencora.Timevault/
WORKDIR "/src/Cencora.Timevault"
RUN dotnet build "./Cencora.Timevault.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cencora.Timevault.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cencora.Timevault.dll"]