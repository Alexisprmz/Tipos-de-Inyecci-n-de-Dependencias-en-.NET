Cómo implementaste cada tipo de servicio.
Transient: Se registra mediante AddKeyedTransient con la clave "transient". Esto crea una nueva instancia de OrderService cada vez que se solicita.
Scoped: Se registra mediante AddKeyedScoped con la clave "scoped". Esto crea una nueva instancia que vive durante toda la duración de una petición HTTP.
Singleton: Se registra mediante AddKeyedSingleton con la clave "singleton". Esto crea una única instancia que se reutiliza durante toda la vida de la aplicación.
El controlador OrdersController inyecta los tres servicios mediante la clave usando el atributo [FromKeyedServices] y expone endpoints para ver el estado y agregar órdenes en cada tipo de servicio.

Qué comportamiento observaste en las pruebas.
Transient: Cada vez que se solicita el servicio, se obtiene una instancia nueva, por lo que el listado de órdenes y el ID de instancia cambian en cada llamada. No mantiene estado entre llamadas.
Scoped: La instancia es única para cada petición HTTP, por lo que las operaciones dentro de una misma petición ven el mismo estado, pero se pierde estado entre diferentes peticiones.
Singleton: El servicio es el mismo durante toda la ejecución de la aplicación (persistente). El estado (órdenes, ID de instancia) se mantiene entre todas las peticiones.

En qué escenarios usarías Transient, Scoped y Singleton en proyectos reales.
Transient: Servicios ligeros y sin estado que pueden crearse muchas veces, como validadores, conversores o servicios que no mantienen datos.
Scoped: Servicios con estado dentro del contexto de una petición, como conexión a base de datos, unidades de trabajo o servicios que manejan estado temporal por usuario.
Singleton: Servicios que mantienen estado global o config que debe ser único en toda la aplicación, por ejemplo, servicios de logging, cachés en memoria o gestores de configuración.

Diagramas simples que expliquen visualmente el ciclo de vida.
Transient:
<img width="1024" height="3541" alt="Untitled diagram-2025-10-26-211858" src="https://github.com/user-attachments/assets/20d9898f-662c-48ee-941d-09d4252a60a4" />
Scoped:
<img width="1134" height="3541" alt="Untitled diagram-2025-10-26-212037" src="https://github.com/user-attachments/assets/5100bcd6-79cf-4d40-9022-811234663265" />
Singleton:
<img width="1596" height="1446" alt="Untitled diagram-2025-10-26-212144" src="https://github.com/user-attachments/assets/30af76a2-918d-491e-b8f6-c2813949a7dd" />


