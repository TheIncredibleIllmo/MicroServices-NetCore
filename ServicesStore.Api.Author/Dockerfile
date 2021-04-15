#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ServicesStore.Api.Author/ServicesStore.Api.Author.csproj", "ServicesStore.Api.Author/"]
RUN dotnet restore "ServicesStore.Api.Author/ServicesStore.Api.Author.csproj"
COPY . .
WORKDIR "/src/ServicesStore.Api.Author"
RUN dotnet build "ServicesStore.Api.Author.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServicesStore.Api.Author.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicesStore.Api.Author.dll"]
