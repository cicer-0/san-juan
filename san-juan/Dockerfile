FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["san-juan.csproj", "./"]
RUN dotnet restore "./san-juan.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "san-juan.csproj" -c Release -o /app/build
RUN dotnet publish "san-juan.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "san-juan.dll"]