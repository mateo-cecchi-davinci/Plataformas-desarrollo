

Admin:
user: juan@gmail.com
pass: 123

Comun:
user: carlos@gmail.com
pass: 123


NAVEGACION:

**Login**
Se ingresa con mail y contraseña 

**Registro**
Formulario de registro en caso de que el usuario no tenga cuenta

**Admin**
Crud de usuarios-hoteles-vuelos-reservasHoteles-reservasVuelos y filtrado de datos

**Comun**
Puede filtrar datos, crear reservas, ver el listado de ciudades y ver su perfil
Las reservas creadas se reflejan en su perfil en donde se ve el listado de todas sus reservas, los hoteles que visito, la cantidad de veces que visito c/ hotel, vuelos tomados y su credito.


NOTAS:

Si el usuario se equivoca tres veces se bloquea
Si es admin ingresa al form de admin y si no al comun
Se filtra en funcion de los text box y los combo box
Ej. si en destino se pone la ciudad chaco, solo aparecen los vuelos que tienen ese destino.
Eso funciona para casi todos los campos.
Algunos decidimos no filtrarlos como por ejemplo cantidad de personas.
Cuando se modifica un usuario, un hotel o un vuelo ese cambio se refleja en las reservas.
Si se crea/modifica un usuario o un hotel los mismos se reflejaran en los combo box habiendo cerrado sesion ya que el contenido de los combo box se carga/actualiza cuando se inicia sesion. 
El costo de las reservas de los hoteles se calcula de la siguiente manera:
Costo del hotel * cantidad de noches * cantidad de personas
El costo de las reservas de los vuelos se calcula de la siguiente manera:
Costo del vuelo * cantidad de personas
-Para elegir un vuelo valido a la hora de hacer una reserva se tiene que elegir la fecha de ese vuelo, si el vuelo de bs as a cordoba es el primero de octubre, hay que elegir esa fecha
-Si a un usuario se le modifica el estado de bloqueado, hay que tener eso en cuenta a la hora de buscar ya que se filtra por ese campo. En una primera instancia se muestran todos los que no estan bloqueados porque por defecto ese checkbox viene destildado. 
-Si se modifica el nombre de un hotel, este no se refleja en los combo box de reserva hotel (lo mismo para todo lo que es modificable y tiene combo box), entonces cuando se selecciona para cargar/modificar una reserva salta un MessageBox porque no se encuentra el hotel. Hay que cerrar y volver a abrir la app para que se actualicen los combo box.


COSAS PARA MEJORAR:

-Hay que deslogear al usuario cuando se borra a si mismo (tirar algun mensaje de advertencia para avisarle que se esta por auto-borrar)
-Que los cambios en usuarios, hoteles y vuelos impacten dinamicamente en los combo box para no tener q salir
-Agregar mensajes lindos cuando se hacen cosas. Osea, ponerle un poquito mas de onda al MessageBox
-El admin deberia poder alternar entre la perspectiva del usuario comun y el usuario administrador???
-Cuando se clickea el vuelo reservado en el perfil se tendria que abrir una ventana con toda la data de la reserva


