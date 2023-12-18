

Admin:
user: juan@gmail.com
pass: 123

Comun:
user: carlos@gmail.com
pass: 123


NAVEGACION:

**Login**
Se ingresa con mail y contraseña. A los tres intentos fallidos se bloquea el usuario.

**Registro**
Formulario de registro en caso de que el usuario no tenga cuenta

**Alojamiento**
En esta seccion hay un formulario de busqueda donde se puede filtrar por destino, fechas, cantidades de personas y habitaciones.
Por debajo del formulario hay dos paquetes que no tienen implementacion, son para decorar. (La idea era mostrar hoteles/paquetes en oferta)
Si se encuentran resultados se indexa a otra seccion y sino hay resultados salta un mensaje de error para notificar que no se encontro ningun hotel en ese destino. (Para tener resultados se recomienda buscar "rosario")
Notas: 
-las fechas, personas y habitaciones influyen sobre los precios que se muestran en las vistas
-las fecha de inicio no puede ser <= a la actual y la otra no puede ser <= a la de inicio

**Resultados**
Aca se muestran los resultados de la busqueda de hoteles con el detalle de sus precios segun todo lo indicado anteriormente.
Con el boton "Comprar" salta un mensaje de alerta y si se confirma se realiza la reserva, se indexa al inicio (Alojamiento) y salta un mensaje para notificar que se pudo realizar la reserva.
Notas:
-Clickeando el icono que aparece al lado del precio en los resultados se abre un modal con el detalle de los precios. Esto lamentablemente no quedo dinamico y siempre se muestra la info del primer resultado. Esto solo es en el detalle del modal, los precios que estan a simple vista estan bien.
-El "raiting", "ver en mapa" y la alerta "solo quedan 3" no tienen implementacion. Son para decorar.

**Vuelos**
Formulario de busqueda donde se puede filtrar por origen, destino, fecha y cantidad de personas. Por debajo del formulario se muestra una tabla con todos los vuelos. Por cada vuelo al final esta la opcion de reservar la cual al ser clickeada dispara un mensaje de alerta, si se confirma se indexa al inicio y salta un mensaje confirmando la reserva (si es que la reserva se pudo hacer exitosamente).
Notas:
-Todo lo que se ingresa en el formulario antes de clickear "Buscar" influye en los resultados, incluso la cantidad de personas. Esto ultimo altera los costos de los vuelos. El problema es que si se busca por ej. un origen, se elige una cantidad de personas y despues se reserva, la reserva se hace sobre el costo y la cantidad de personas que se eligio al buscar la primera vez. Quizas no parece un problema pero al ver el flujo es poco intuitivo porque se setean las personas y estas no influyen en el costo de los resultados sin antes apretar "Buscar".

**Base de datos**
Dashboard para admins donde se pueden gestionar todas las entidades.
Notas:
-Ciudad, UsuarioHabitacion y UsuarioVuelo no se tuvieron en cuenta. 
-Todas las tablas tienen paginado
-No se implementaron mensajes de alerta
-Se puede asociar imagenes a los hoteles. Si estos no tienen una imagen seteada se muestra una imagen indicando que no tiene foto.
-Cuando se crean habitaciones estas no necesariamente se crean de a una. Se pueden elegir muchas y estas se asocian todas al hotel seleccionado.
-Lo ideal para probar el flujo entre hotel-habitacion-reservaHabitacion-usuario seria crear un hotel, asociarle/crearle habitaciones y a esas habitaciones asociarles/crearles reservas. Al borrar el hotel, poner un breakpoint en el post del delete del controlador y ver que pasa.
-Para crear reservas de habitaciones hay que elegir usuario, hotel, fechas, personas y habitaciones (estas dos ultimas por defecto son 1). El costo de la reserva esta bloqueado y es obligatorio, es decir, este se setea en funcion de todo lo anterior mencionado. Esto quedo dinamico. Cada vez que se cambia algo, se cambia el costo. Las fechas tienen la misma logica del buscador de alojamiento. Para ver como se calcula el costo ver "ObtenerCosto" en ReservaHabitacionController. El problema con este flujo es que todas las reservas de habitaciones que se crean tienen la cantidad total de personas elegidas. Quedo pendiente mapear las personas por habitacion.
-Para crear reservas de vuelos el costo tambien esta bloqueado y este se setea con el costo del vuelo seleccionado. Esto es dinamico. Se elige el vuelo, se setea el costo. 

**Perfil del usuario**
Info del usuario, reservas, habitaciones usadas y vuelos tomados.


NOTAS:

-El sidebar se abre y se cierra clickeando sobre el logo de la pagina o el icono de buscar. Este ultimo no tiene implementacion.
-Las vistas Home/Index y Shared/Paquetes decidi no sacarlas. No estan a simple vista en la navegacion, pero si se ingresan por url se abren. Estas no tienen implementacion.
-Hay muchas cosas que estan medio feas. Tuve problemas con los include. En los Edit termine comentando todos los update y seteando directamente las propiedades a la entidad trackeada.
-El codigo esta todo nefasto. Quedaron todos los nombres en español/ingles.
-Las reservas que se hacen a partir del usuario comun funcionan pero de otra manera. Perdon por esto. Las resolvi mandando solicitudes por ajax. Los metodos quedaron en HotelController y VueloController. Tendiran que haber quedado en ReservaHabitacionController y ReservaVueloController. Esto igual es simplemente cambiar las rutas donde se mandan las peticiones, copiar/pegar y listo. Por eso no me parecio algo tan relevante. Me di cuenta tarde de que estos metodos iban en otro lado :)
-Se calculan impuestos sobre los costos de los resultados de las busquedas de alojamiento pero en el crud no. Para los vuelos no hay inpuestos.


OPCIONAL:

-Mostrar la fecha de los vuelos en el crud de las reservas. Para identificarlos mejor.
-No se deberian poder mostrar/reservar vuelos con fechas del pasado. 
-Traducir todo a español
-Los precios en los detalles de los resultados tienen que ser dinamicos en los modal.
-Arreglar los estilos responsivos del login/registro
-Mensaje para que el usuario sepa que no le alcanza para reservar y no indexarlo al home sino devolverlo a los resultados de la busqueda anterior.
-Los metodos de reservar deberian estar en los controladores de las reservas
-Las habitaciones deberian estar agrupadas en el crud
-Validar q un usuario no pueda reservar mas de 4 habitaciones diferentes con los mismos rangos de fechas
-Borrar imagen del hotel cuando se borra el hotel o se modifica su imagen.
-Hay que deslogear al usuario cuando se borra a si mismo (tirar algun mensaje de advertencia para avisarle que se esta por auto-borrar)
-solo se deberian mostrar vuelos que esten a partir de la fecha de hoy en adelante
-arreglar el popover de hotel. se pueden agregar mas de 8 personas cuando se agregan 7 y despues se agregan habitaciones. Si en la primer habitacion se agregan 7 personas y despues se agregan 3 habitaciones, personas = 10
-Implementar mensaje de alerta cuando quedan como min 5 hab
-Implementar rating
-Implementar mapas para ver la ubicacion de un hotel
-Implementar apis para hoteles, vuelos y ciudades
-No necesariamente es un error pero cuando se elige una cantidad de personas y no se clickea el boton de buscar en el home de vuelos, la compra se realiza sobre el costo que figura en la tabla que por defecto es 1 persona.

Me hubiese encantado poder arreglar e implementar mas cosas. Queria hacer un carrito para agregar hoteles, vuelos y poder hacer la compra de una.
Otra cosa copada era que para el registro se mande un mail de verificacion y que al clickear una url se confirme el registro.
Mandar una factura de consumidor final al mail del usuario logeado con los detalles de la reserva.

