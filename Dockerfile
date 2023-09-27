﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mytask.API.csproj", "./"]
RUN dotnet restore "Mytask.API.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Mytask.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mytask.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mytask.API.dll"]
