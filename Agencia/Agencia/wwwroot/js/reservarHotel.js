const hotel_buttons = document.querySelectorAll(".reservar-hotel");

hotel_buttons.forEach((button) => {
    button.addEventListener("click", function () {
        const hotel_id = $(this).data('hotel_id');
        const startDate = $(this).data('fecha_desde');
        const endDate = $(this).data('fecha_hasta');
        const formattedStartDate = new Date(startDate).toISOString();
        const formattedEndDate = new Date(endDate).toISOString();
        const total = $(this).data('suma_total');
        const sm_rooms = $(this).data('habitaciones_chicas');
        const md_rooms = $(this).data('habitaciones_medianas');
        const xl_rooms = $(this).data('habitaciones_grandes');
        const people = $(this).data('total_personas');

        fetch("/Hotel/Reservar", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                hotel_id: parseInt(hotel_id),
                start_date: formattedStartDate,
                end_date: formattedEndDate,
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
                window.location.href = "/Hotel/Home";
            })
            .catch((error) => {
                console.error(error);
            });
    });
});