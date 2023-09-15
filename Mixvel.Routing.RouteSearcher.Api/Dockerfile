FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mixvel.Routing.RouteSearcher/Mixvel.Routing.RouteSearcher.csproj", "Mixvel.Routing.RouteSearcher/"]
RUN dotnet restore "Mixvel.Routing.RouteSearcher/Mixvel.Routing.RouteSearcher.csproj"
COPY . .
WORKDIR "/src/Mixvel.Routing.RouteSearcher"
RUN dotnet build "Mixvel.Routing.RouteSearcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mixvel.Routing.RouteSearcher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mixvel.Routing.RouteSearcher.dll"]
