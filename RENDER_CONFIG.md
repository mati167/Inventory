# Configuración de Variables de Entorno para Render

## Descripción

La aplicación usa dos métodos para obtener la API key de OMDb:
1. **Producción (Render):** Lee de la variable de entorno `OMDB_API_KEY`
2. **Desarrollo:** Lee del archivo `appsettings.json`

## Configuración en Render

### Pasos para agregar la variable de entorno:

1. **Ve a tu servicio en Render**
   - Abre tu proyecto en https://render.com
   - Selecciona tu servicio web (films-app-2klz)

2. **Accede a Environment**
   - En el panel izquierdo, haz clic en **Environment**

3. **Agrega la nueva variable**
   - Haz clic en **+ Add Environment Variable**
   - **Key:** `OMDB_API_KEY`
   - **Value:** `1c23b701`
   - Haz clic en **Save**

4. **Redeploy la aplicación**
   - Render automáticamente redesplegará la aplicación con la nueva variable

### Variables recomendadas a configurar en Render:

```
OMDB_API_KEY=1c23b701
ASPNETCORE_ENVIRONMENT=Production
```

## Desarrollo Local

En desarrollo, la aplicación lee automáticamente de `appsettings.Development.json`:

```json
"ExternalApis": {
  "OMDb": {
    "BaseUrl": "https://www.omdbapi.com/",
    "ApiKey": "1c23b701"
  }
}
```

## Orden de precedencia

```
1. Variable de entorno (OMDB_API_KEY) - Si existe, se usa esta
2. appsettings.json - Si no existe la variable, usa el valor del archivo
```

## Cambiar la API Key en Producción

Para cambiar la API key en producción:

1. Ve a **Environment** en Render
2. Edita el valor de `OMDB_API_KEY`
3. Haz clic en **Save**
4. Render hará redeploy automáticamente

No necesitas hacer cambios de código ni recompilación.
