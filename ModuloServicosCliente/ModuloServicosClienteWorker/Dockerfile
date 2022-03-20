#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ModuloServicosClienteWorker/ModuloServicosClienteWorker.csproj", "ModuloServicosClienteWorker/"]
RUN dotnet restore "ModuloServicosClienteWorker/ModuloServicosClienteWorker.csproj"
COPY . .
WORKDIR "/src/ModuloServicosClienteWorker"
RUN dotnet build "ModuloServicosClienteWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModuloServicosClienteWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloServicosClienteWorker.dll"]