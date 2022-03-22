#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ModuloReport.API/ModuloReport.API.csproj", "ModuloReport.API/"]
RUN dotnet restore "ModuloReport.API/ModuloReport.API.csproj"
COPY . .
WORKDIR "/src/ModuloReport.API"
RUN dotnet build "ModuloReport.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModuloReport.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloReport.API.dll"]