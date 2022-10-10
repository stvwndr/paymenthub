#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["backend/src/PaymentHub.PagSeguro.Api/PaymentHub.PagSeguro.Api.csproj", "backend/src/PaymentHub.PagSeguro.Api/"]
RUN dotnet restore "backend/src/PaymentHub.PagSeguro.Api/PaymentHub.PagSeguro.Api.csproj"
COPY . .
WORKDIR "/src/backend/src/PaymentHub.PagSeguro.Api"
RUN dotnet build "PaymentHub.PagSeguro.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentHub.PagSeguro.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentHub.PagSeguro.Api.dll"]