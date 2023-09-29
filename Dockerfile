FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ./src .
RUN dotnet restore "./Thrima.WebApi/Thrima.WebApi.csproj"
RUN dotnet build "./Thrima.WebApi/Thrima.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./Thrima.WebApi/Thrima.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Thrima.WebApi.dll"]