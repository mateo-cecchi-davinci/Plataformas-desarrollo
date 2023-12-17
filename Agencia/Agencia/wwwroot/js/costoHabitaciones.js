document.addEventListener("DOMContentLoaded", function () {
    const selectHotel = document.getElementById('hotel');
    const personas_por_habitacion = document.getElementById('total_people_rooms');
    const fechaDesde = document.getElementById('fechaDesde');
    const fechaHasta = document.getElementById('fechaHasta');
    const costoHabitacion = document.getElementById('costo');
    const habitacionesChicas = document.getElementById('habitaciones_chicas');
    const habitacionesMedianas = document.getElementById('habitaciones_medianas');
    const habitacionesGrandes = document.getElementById('habitaciones_grandes');

    selectHotel.addEventListener('change', async function () {
        const fechaInicio = new Date(fechaDesde.value) ? new Date(fechaDesde.value) : '';
        const fechaFin = new Date(fechaHasta.value) ? new Date(fechaHasta.value) : '';

        if (fechaInicio != '' && fechaFin != '') {
            const totalPeopleRooms = JSON.parse(personas_por_habitacion.value);
            const diferenciaEnTiempo = fechaFin.getTime() - fechaInicio.getTime();
            const diferenciaEnDias = Math.ceil(diferenciaEnTiempo / (1000 * 3600 * 24));
            await obtenerCostoSeleccionado(this.value, totalPeopleRooms, diferenciaEnDias, fechaInicio, fechaFin);
        }
    });

    fechaHasta.addEventListener('change', async function () {
        const fechaInicio = new Date(fechaDesde.value) ? new Date(fechaDesde.value) : '';
        const fechaFin = new Date(fechaHasta.value) ? new Date(fechaHasta.value) : '';

        if (fechaInicio != '' && fechaFin != '') {
            const hotelId = selectHotel.value;
            const totalPeopleRooms = JSON.parse(personas_por_habitacion.value);
            const diferenciaEnTiempo = fechaFin.getTime() - fechaInicio.getTime();
            const diferenciaEnDias = Math.ceil(diferenciaEnTiempo / (1000 * 3600 * 24));
            await obtenerCostoSeleccionado(hotelId, totalPeopleRooms, diferenciaEnDias, fechaInicio, fechaFin);
        }
    });

    personas_por_habitacion.addEventListener('change', async function () {
        const fechaInicio = new Date(fechaDesde.value) ? new Date(fechaDesde.value) : '';
        const fechaFin = new Date(fechaHasta.value) ? new Date(fechaHasta.value) : '';

        if (fechaInicio != '' && fechaFin != '') {
            const hotelId = selectHotel.value;
            const totalPeopleRooms = JSON.parse(personas_por_habitacion.value);
            const diferenciaEnTiempo = fechaFin.getTime() - fechaInicio.getTime();
            const diferenciaEnDias = Math.ceil(diferenciaEnTiempo / (1000 * 3600 * 24));
            await obtenerCostoSeleccionado(hotelId, totalPeopleRooms, diferenciaEnDias, fechaInicio, fechaFin);
        }
    });

    function obtenerCostoSeleccionado(hotelId, totalPeopleRooms, diferenciaEnDias, fechaInicio, fechaFin) {
        const totalPeopleRoomsString = JSON.stringify(totalPeopleRooms);
        const url = `/ReservaHabitacion/ObtenerCosto?id=${hotelId}&totalPeopleRoomsString=${totalPeopleRoomsString}&diferenciaEnDias=${diferenciaEnDias}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`;
        return fetch(url)
            .then(response => {
                return response.json();
            })
            .then(data => {
                const { costo, habitacionesChicasSeleccionadas, habitacionesMedianasSeleccionadas, habitacionesGrandesSeleccionadas } = data;
                console.log("costo: " + costo);
                console.log("habitaciones_chicas: " + habitacionesChicasSeleccionadas);
                console.log("habitaciones_medianas: " + habitacionesMedianasSeleccionadas);
                console.log("habitaciones_grandes: " + habitacionesGrandesSeleccionadas);

                costoHabitacion.value = costo;
                habitacionesChicas.value = habitacionesChicasSeleccionadas;
                habitacionesMedianas.value = habitacionesMedianasSeleccionadas;
                habitacionesGrandes.value = habitacionesGrandesSeleccionadas;

                return data; 
            })
            .catch(error => {
                console.error('Error al obtener el costo:', error);
                throw error;
            });
    }
});