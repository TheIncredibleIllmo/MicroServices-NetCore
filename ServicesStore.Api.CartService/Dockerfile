#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ServicesStore.Api.CartService/ServicesStore.Api.CartService.csproj", "ServicesStore.Api.CartService/"]
RUN dotnet restore "ServicesStore.Api.CartService/ServicesStore.Api.CartService.csproj"
COPY . .
WORKDIR "/src/ServicesStore.Api.CartService"
RUN dotnet build "ServicesStore.Api.CartService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServicesStore.Api.CartService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicesStore.Api.CartService.dll"]
