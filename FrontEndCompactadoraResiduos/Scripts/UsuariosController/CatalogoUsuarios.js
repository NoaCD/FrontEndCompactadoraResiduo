/***************************************
 * 
 * 
 * 
 * 
 * ***************************************/


$(document).ready(function () {
    var table = $('#tableUsers').DataTable(
        {
            //Configuracion de datatables

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
    $('#btn-edit-user').click(function () {

        var arrayUsuario = table.row('.selected').data();
        // Verificamos quue este seleccionado una fila
        if (arrayUsuario.length > 0) {

            var Data = {};
            var id = arrayUsuario[0];
            var datos = {
                iId : id
            }
            Data["datos"] = JSON.stringify(datos);
            console.log(Data);


            var ruta =  "/Usuarios/VerUsuario";
         
            showModal("POST", ruta, Data, null);//mandamos llamar el modal

        } else {
            Swal.fire(
                'Good job!',
                'You clicked the button!',
                'success'
            );
        }
      
    });


    /*Agregamos enumeraciones a la primera columna */
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

});


