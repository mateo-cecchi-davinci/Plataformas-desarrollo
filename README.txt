

Admin:
user: cris@gmail.com
pass: 123

Comun:
user: pepe@gmail.com
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
Al principio del Form1 se cargan registros en todas las entidades.
Se filtra en funcion de los text box y los combo box
Ej. si en destino se pone la ciudad chaco, solo aparecen los vuelos que tienen ese destino.
Eso funciona para casi todos los campos.
Algunos decidimos no filtrarlos como por ejemplo cantidad de personas.
Cuando se modifica un usuario, un hotel o un vuelo ese cambio se refleja en las reservas.
Si se crea/modifica un usuario o un hotel los mismos se reflejaran en los combo box habiendo cerrado sesion ya que el contenido de los combo box se carga/actualiza cuando se inicia sesion. 
Falta la logica para mostrar en el perfil del usuario comun sus hoteles visitados y sus vuelos tomados.
Para el desarrollo de este proyecto se tuvieron que cambiar algunas cosas como por ejemplo la lista de vuelos del objeto Ciudad en el cual usamos vuelosOrigen y vuelosDestino. Se crearon algunos metodos como mostrarCredito, modificarFechaVuelo y agregarCiudad.


COMENTARIOS:

No nos quedo claro como es el tema de la disponibilidad de los hoteles ya que eso varia dependiendo del tiempo en que se hospeda una persona.

Ejemplo:
Si un usuario se hospeda del 1 al 5, durante esos dias se le tendria que restar 1 a la capacidad del hotel y al termino de ese lapso de tiempo se le tendria que sumar la cantidad restada por ese usuario.

La cantidad restada es relativa porque el usuario puede seleccionar una cantidad de personas x, entonces a la hora de sumar dadose el termino del lapso de tiempo de la reserva se deberia sumar el valor de x que esta asociado a ese usuario en especifico.
Esta logica implica tener que manejar la capacidad en funcion de las fechas de las reservas y la fecha actual.
A traves de la fecha actual se podria validar si ese hotel entra en el rango de "reservado". A partir de ese punto a la capacidad se le resta uno y al usuario se le agregaria el hotel en la lista "hotelesVisitados".
Siguiendo la linea de tiempo con una variable que almacena la fecha actual se valida si esa reserva deberia seguir con el estado de "reservado", cuando ya no lo tiene, ahi se le vuelve a sumar la capacidad restada al hotel. Esa capacidad se debe almacenar por usuario ya que c/u ocupa un espacio fisico en el hotel. Deberian tener una lista "camasOcupadas" o "espacioOcupado"?

La forma en que manejamos la capacidad de los hoteles fue a traves de la creacion de reservas por parte de los usuarios comunes. Simplemente le restamos el numero que seleccionan. Pero eso esta mal. Ese valor restado deberia ser relativo en funcion de la fecha que elige.

El mayor problema con el desarrollo de este proyecto fue no tener una base de datos. Sin eso, manejar las relaciones es muy dificil. Errores como el anteriormente mencionado se solucionarian facilmente con un "stored procedure" que se corre todos los dias en un determinado horario.

