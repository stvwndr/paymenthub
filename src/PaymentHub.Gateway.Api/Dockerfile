#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PaymentHub.Gateway.Api/PaymentHub.Gateway.Api.csproj", "PaymentHub.Gateway.Api/"]
RUN dotnet restore "PaymentHub.Gateway.Api/PaymentHub.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/PaymentHub.Gateway.Api"
RUN dotnet build "PaymentHub.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentHub.Gateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentHub.Gateway.Api.dll"]