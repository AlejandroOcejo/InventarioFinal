# Inventario Docker Image

Este documento proporciona las instrucciones para descargar y ejecutar la imagen Docker del proyecto Inventario.

## Descargar la Imagen

Para descargar la última versión de la imagen `Inventario` desde Docker Hub, ejecuta el siguiente comando:

```bash
docker pull pablofrancopinilla/inventario:final
```
## Ejecutar el Contenedor
Una vez descargada la imagen, puedes ejecutar un contenedor basado en esa imagen usando el siguiente comando:

```bash
docker run -it -p 7244:7244 pablofrancopinilla/inventario:final
```
