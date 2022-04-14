$(document).ready(function () {
  var t =  $('#example').DataTable({
        "ajax": "/Carga/ApiCargas",
        "processing":true,
        "columns": [
            { "data": "numeroEmpleado" },
            { "data": "nombreResiduo" },
            { "data": "idEmpleado" },
            { "data": "idCarga" },
            { "data": "nombreResiduo" },
            { "data": "fechaCreacionCarga" }
         
        ]
  });
    t.on('order.dt search.dt', function () {
        let i = 1;

        t.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
            this.data(i++);
        });
    }).draw();
});
