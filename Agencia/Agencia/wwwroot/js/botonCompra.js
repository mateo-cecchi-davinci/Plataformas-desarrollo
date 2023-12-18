const buttons = document.querySelectorAll(".buy-button");

buttons.forEach((button) => {
    button.addEventListener("click", function () {
        const hotel = JSON.parse($(this).data('hotel'));
        const vuelo = JSON.parse($(this).data('vuelo'));
        const startDate = $(this).data('fecha_desde');
        const endDate = $(this).data('fecha_hasta');
        const total = $(this).data('suma_total');
        const sm_rooms = $(this).data('habitaciones_chicas');
        const md_rooms = $(this).data('habitaciones_medianas');
        const xl_rooms = $(this).data('habitaciones_grandes');
        const people = $(this).data('total_personas');

        fetch("/Reserva/Proceso", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                hotel: hotel,
                vuelo: vuelo,
                start_date: startDate,
                end_date: endDate,
                total: parseFloat(total),
                sm_rooms: parseInt(sm_rooms),
                md_rooms: parseInt(md_rooms),
                xl_rooms: parseInt(xl_rooms),
                people: parseInt(people),
            }),
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                window.location.href = "/Home";
            })
            .catch((error) => {
                console.error(error);
            });
    });
});