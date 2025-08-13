# .NET SDK (build için)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Çözüm dosyasını ve tüm kaynak kodu kopyala
COPY ../../BankManagement.sln ./ 
COPY ../../src ./src

# Restore ve publish
RUN dotnet restore ./src/Abp.BankManagement.DbMigrator/Abp.BankManagement.DbMigrator.csproj
RUN dotnet publish ./src/Abp.BankManagement.DbMigrator/Abp.BankManagement.DbMigrator.csproj -c Release -o /app

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Abp.BankManagement.DbMigrator.dll"]
