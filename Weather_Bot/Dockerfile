#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Weather_Bot/Weather_Bot.csproj", "Weather_Bot/"]
RUN dotnet restore "Weather_Bot/Weather_Bot.csproj"
COPY . .
WORKDIR "/src/Weather_Bot"
RUN dotnet build "Weather_Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weather_Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weather_Bot.dll"]