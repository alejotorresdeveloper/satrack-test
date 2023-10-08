# GestionTareas
CRUD para tareas 

Esta APi expone los metodos que permiten a los usuario 
- Crear tareas
- Modificar tareas
- Eliminar tareas
- Buscar tareas por id
- Buscar tareas por id de categoria
- Cambiar tareas de estado (Nueva, En progreso, Terminada)

# Para ejecutar el API

Se debe tener en cuenta este comentario en el appsettings.json
![image](https://github.com/alejotorresdeveloper/satrack-test/assets/114192168/34c3b9a2-ab53-4854-8b4e-af65015c4edc)

Se debe correr el siguiente comando:

```
docker-compose up -d
```

Este crea la imagen de la base de datos y del API, si no se hace el cambio mencionado en el appsettings se genera un error de visibilidad etre el API y la base de datos, de igual forma si se ejcuta en el IDE se debe considerar este cambio en el appsettings.
