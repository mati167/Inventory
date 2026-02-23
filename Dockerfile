# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["Inventory.Api.csproj", "./"]
RUN dotnet restore "./inventory/Inventory.Api.csproj"

# Copiar el resto del código y publicar
COPY . .
RUN dotnet publish "Inventory.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto que Render usa por defecto
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Inventory.Api..dll"]