const { ajax } = require("jquery");

$(document).ready(function () {
    var table = $('#tableUsers').DataTable(
        {
            language:
            {
                "decimal": "",
                "emptyTable": "No hay información",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Entradas",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            },

        }
    );

    $('#tableUsers tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    /* Boton dispara un evento para hacer una peticion
     *
     */
    $('#btn-see').click(function () {
        var arrayUsuario = table.row('.selected').data();

        obtenerUsuario(_oUsuario); //funcion llamar a un usuario
    });


    /*Agregamos enumeraciones a la primera columna */
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();


});

/*COnvertimos el array a un objeto de*/
var obtenerUsuario = function (_oUsuario) {

  

};














function LlamarMetodo(cTipo, cUrl, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: Data,
        dataType: "JSON",
        success: function (response) {
            if (response != true) {
                const Toast = Swal.mixin({
                    Toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                    icon: 'error'
                })
                Toast.fire({
                    type: response.cEstatus,
                    title: response.cMensaje
                })
            }
            else {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,

                })
                Toast.fire({
                    icon: 'success',
                    type: 'success',
                    title: 'Bienvenido a GioContX'
                })
                window.location.replace(ruta + "Inicio/Inicio");
            }
            if (Funcion)
                window[Funcion]();
        }
    });
}
