
$(document).ready(function() {
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
        $(this).toggleClass('selected');
    });

    $('#btn-see').click(function () {
        alert(table.rows('.selected').data().length + ' row(s) selected');
    });

    //Obtener el index al hacer un click


});