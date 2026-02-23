# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copiamos el archivo de proyecto entrando a la carpeta 'inventory'
# Nota: La ruta es relativa al Dockerfile
COPY ["inventory/Inventory.Api.csproj", "inventory/"]

# 2. Restauramos dependencias apuntando al archivo en su nueva ubicación
RUN dotnet restore "inventory/Inventory.Api.csproj"

# 3. Copiamos el resto del código
COPY . .

# 4. Publicamos el proyecto
WORKDIR "/src/inventory"
RUN dotnet publish "Inventory.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Importante: Render usa la variable PORT, asegúrate de que tu Program.cs la lea
# o configura el puerto en el panel de Render como 8080 si usas el default de .NET 8+
EXPOSE 8080

ENTRYPOINT ["dotnet", "Inventory.Api.dll"]