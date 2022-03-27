#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ModuloEmail.API/ModuloEmail.API.csproj", "ModuloEmail.API/"]
RUN dotnet restore "ModuloEmail.API/ModuloEmail.API.csproj"
COPY . .
WORKDIR "/src/ModuloEmail.API"
RUN dotnet build "ModuloEmail.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModuloEmail.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloEmail.API.dll"]