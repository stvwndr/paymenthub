#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["backend/src/PaymentHub.Ame.Api/PaymentHub.Ame.Api.csproj", "backend/src/PaymentHub.Ame.Api/"]
RUN dotnet restore "backend/src/PaymentHub.Ame.Api/PaymentHub.Ame.Api.csproj"
COPY . .
WORKDIR "/src/backend/src/PaymentHub.Ame.Api/"
RUN dotnet build "PaymentHub.Ame.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentHub.Ame.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentHub.Ame.Api.dll"]
