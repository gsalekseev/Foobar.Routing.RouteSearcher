FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Foobar.Routing.RouteSearcher/Foobar.Routing.RouteSearcher.csproj", "Foobar.Routing.RouteSearcher/"]
RUN dotnet restore "Foobar.Routing.RouteSearcher/Foobar.Routing.RouteSearcher.csproj"
COPY . .
WORKDIR "/src/Foobar.Routing.RouteSearcher"
RUN dotnet build "Foobar.Routing.RouteSearcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Foobar.Routing.RouteSearcher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Foobar.Routing.RouteSearcher.dll"]
