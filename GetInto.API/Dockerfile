#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GetInto.API/GetInto.API.csproj", "GetInto.API/"]
RUN dotnet restore "GetInto.API/GetInto.API.csproj"
COPY . .
WORKDIR "/src/GetInto.API"
RUN dotnet build "GetInto.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GetInto.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetInto.API.dll"]