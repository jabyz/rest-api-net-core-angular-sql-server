FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["rest-api-net-core-crud-demo.csproj", "./"]
RUN dotnet restore "rest-api-net-core-crud-demo.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "rest-api-net-core-crud-demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "rest-api-net-core-crud-demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "rest-api-net-core-crud-demo.dll"]
