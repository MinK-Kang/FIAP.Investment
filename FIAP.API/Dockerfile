FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["FIAP.API/FIAP.API.csproj", "FIAP.API/"]
COPY ["FIAP.Services/FIAP.Services.csproj", "FIAP.Services/"]
COPY ["FIAP.Repositories/FIAP.Repositories.csproj", "FIAP.Repositories/"]
COPY ["FIAP.Domain/FIAP.Domain.csproj", "FIAP.Domain/"]
RUN dotnet restore "FIAP.API/FIAP.API.csproj"
COPY . .
WORKDIR "/src/FIAP.API"
RUN dotnet build "FIAP.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FIAP.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.API.dll"]