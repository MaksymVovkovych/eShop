﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Order/Order.API/Order.API/Order.API.csproj", "Order/Order.API/Order.API/"]
COPY ["Shared/Shared.Applications/Shared.csproj", "Shared/Shared.Applications/"]
RUN dotnet restore "Order/Order.API/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/Order/Order.API/Order.API"
RUN dotnet build "Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]
