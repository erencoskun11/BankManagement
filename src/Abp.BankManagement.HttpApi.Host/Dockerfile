# Base image (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Container içinde çalýþtýðýný bilsin
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Sadece Host uygulamasý ve baðýmlý projelerin csproj'larýný kopyala
COPY ["src/Abp.BankManagement.HttpApi.Host/Abp.BankManagement.HttpApi.Host.csproj", "src/Abp.BankManagement.HttpApi.Host/"]
COPY ["src/Abp.BankManagement.Application/Abp.BankManagement.Application.csproj", "src/Abp.BankManagement.Application/"]
COPY ["src/Abp.BankManagement.Domain.Shared/Abp.BankManagement.Domain.Shared.csproj", "src/Abp.BankManagement.Domain.Shared/"]
COPY ["src/Abp.BankManagement.Domain/Abp.BankManagement.Domain.csproj", "src/Abp.BankManagement.Domain/"]
COPY ["src/Abp.BankManagement.Application.Contracts/Abp.BankManagement.Application.Contracts.csproj", "src/Abp.BankManagement.Application.Contracts/"]
COPY ["src/Abp.BankManagement.EntityFrameworkCore/Abp.BankManagement.EntityFrameworkCore.csproj", "src/Abp.BankManagement.EntityFrameworkCore/"]

# NuGet paketlerini indir
RUN dotnet restore "src/Abp.BankManagement.HttpApi.Host/Abp.BankManagement.HttpApi.Host.csproj"

# Tüm kaynaklarý kopyala, build ve publish adýmlarýna geç
COPY . .
WORKDIR "/src/src/Abp.BankManagement.HttpApi.Host"
RUN dotnet build "Abp.BankManagement.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Abp.BankManagement.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime image
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Abp.BankManagement.HttpApi.Host.dll"]
