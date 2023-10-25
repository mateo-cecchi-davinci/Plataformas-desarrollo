

Admin:
user: juan@gmail.com
pass: 123

Comun:
user: PRUEBA
pass: 123


NAVEGACION:

**Login**
Se loguea con mail y contraseña 

**Registro**
Si no esta registrado se puede registrar y elegir (solo por esta instancia) si es admin o no

**Admin**
Crud de usuarios-hoteles-vuelos-reservasHoteles-reservasVuelos y filtrado de datos

**Comun**
Puede filtrar datos, crear reservas, ver el listado de ciudades y ver su perfil
Las reservas creadas se reflejan en su perfil en donde se ve el listado de todas sus reservas y su credito.
Sus reservas estan limitadas al credito que tiene disponible ya que las mismas actualizan su credito descontandole el monto que corresponde al costo de la reserva en si.


NOTAS:

Si el usuario se equivoca tres veces se bloquea
Si es admin ingresa al form de admin y si no al comun
Se filtra en funcion de los text box y los combo box
Ej. si en destino se pone la ciudad chaco, solo aparecen los vuelos que tienen ese destino.
Eso funciona para casi todos los campos.
Algunos decidimos no filtrarlos como por ejemplo cantidad de personas.
Cuando se modifica un usuario, un hotel o un vuelo ese cambio se refleja en las reservas.
Si se crea/modifica un usuario o un hotel los mismos se reflejaran en los combo box habiendo cerrado sesion ya que el contenido de los combo box se carga/actualiza cuando se inicia sesion. 


COMENTARIOS:

-Hay que deslogear al usuario cuando se borra a si mismo (tirar algun mensaje de advertencia para avisarle que se esta por auto-borrar)
-Que los cambios en usuarios, hoteles y vuelos impacten dinamicamente en los combo box para no tener q salir
-Si se eliminan usuario, hotel o vuelo tambien se derian eliminar las reservas asociadas
-Agregar mensajes lindos cuando se hacen cosas. Osea, ponerle un poquito mas de onda al MessageBox
-El admin deberia poder alternar entre la perspectiva del usuario comun y el usuario administrador???
-Cuando se clickea el vuelo reservado en el perfil se tendria que abrir una ventana con toda la data de la reserva
-La fecha impacta sobre el precio? hoteles agregarReserva-modificarReserva
-No se reflejan las modificaciones en el UI. Por ejemplo las de usuarios en el modo admin y la cantidad de hotelesVisitados-vuelosTomados en el modo usuario-comun
-Cuando se bloquea un usuario eso tambien se tendria que hacer en la bdd
-Para elegir un vuelo valido a la hora de hacer una reserva se tiene que elegir la fecha de ese vuelo, si el vuelo de bs as a cordoba es el primero de octubre, hay que elegir esa fecha
-Si se modifica el nombre de un hotel, este no se refleja en los combo box de reserva hotel (lo mismo para todo lo que es modificable y tiene combo box), entonces cuando se selecciona para cargar/modificar una reserva salta un MessageBox porque no se encuentra el hotel. Hay que cerrar y volver a abrir la app para que se actualicen los combo box.
-Flatan metodos en DAL para modificar todo lo modificable. (credito, capacidad_hotel, capacidad_vuelo)
Esos metodos se tendrian que ejecutar dentro de modificarReservaHotel o modificarReservaVuelo. Ejemplo cuando se resta el costo de un hotel/vuelo del credito de un usuario. Estos cambios se hacen en memoria pero no en la bdd.
-No se deberia aumentar cantidad cada vez que se modifican las reservas, se deberia aumentar solo cuando se agrega una relacion que antes no existia o cuando se agrega una relacion que antes existia pero tiene un id de reserva distinto.*CORREGIDO*
-Cuando se modifica una reserva, si por ejemplo en usuario_hotel esa relacion no existe se tiene que crear y si existe se tiene que actualizar la cantidad en funcion del id de la reserva, ya que si este sigue siendo el mismo entonces a la cantidad no hay que hacerle nada.*CORREGIDO*
-No se actualizan las propiedades de usuarioHotel y usuarioVuelo cuando se hacen modificaciones de las reservas (se hace en la bdd pero no en memoria).
Para que se refleje en la memoria hay que cerrar y abrir la app.


IMPLEMENTAR: 

HOTEL
huespedes

VUELO
vendido

