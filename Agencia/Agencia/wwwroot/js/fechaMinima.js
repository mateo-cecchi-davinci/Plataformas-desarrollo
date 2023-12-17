document.addEventListener("DOMContentLoaded", function () {
    const fechaMin = document.getElementById('fechaDesde');
    const fechaMax = document.getElementById('fechaHasta');

    fechaMin.addEventListener('input', function () {
        const fechaDesde_seleccionada = new Date(fechaMin.value);
        const fechaHasta_minima = new Date(fechaDesde_seleccionada);

        fechaHasta_minima.setDate(fechaDesde_seleccionada.getDate() + 1);

        const fechaHasta_minima_formateada = fechaHasta_minima.toISOString().split('T')[0];

        fechaMax.min = fechaHasta_minima_formateada;

        fechaMax.disabled = false;
        fechaMax.value = '';
    });
});