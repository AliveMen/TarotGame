#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TarotGame.Web/TarotGame.Web.csproj", "TarotGame.Web/"]
RUN dotnet restore "TarotGame.Web/TarotGame.Web.csproj"
COPY . .
WORKDIR "/src/TarotGame.Web"
RUN dotnet build "TarotGame.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TarotGame.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TarotGame.Web.dll"]