/********************
 * Funcion de la tabla Cargas
 * 
 * **************/
$(document).ready(function () {
    //Ajax de Datatables para las cargas
    var table = $('#tablaAlmacenes').DataTable({
        "ajax": "/Almacenes/TodosAlmacenes",
        "processing": false,
        "columns": [
            { "data": "id" },
            { "data": "clave" },
            { "data": "nombre" },
            { "data": "ubicacion" },
            { "data": "fechaCreacion" },
            { "data": "fechaModificacion" }
        ],
        //Configuracion de datatables lenguaje
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

    /*Agregamos enumeraciones a la primera columna */
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    $('#tablaAlmacenes tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    var filaSeleccionada = function () {
        var arrayCarga = table.row('.selected').data(); //Solo va a existir el array si se selecciona
        if (arrayCarga != null) {
            return true;
        } else {
            return false
        }
    }


    /*******************************************************************
    *  Metodo que controla el 
    *  boton ver de la carga
    * ******************************************************************/

    $("#btn-see-almacen").click(function () {

        if (filaSeleccionada() == true) {
            var arrayCarga = table.row('.selected').data(); //Solo va a existir el array si se selecciona

            var Data = {};
            var idAlmacen = arrayCarga["id"];

            var ruta = "/Almacenes/detalleAlmacen?id=" + idAlmacen;
            showModal("POST", ruta, null, null);//mandamos llamar el modal
        } else {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            //Tostada de error
            Toast.fire({
                icon: 'error',
                title: '¡Seleccione un elemento para observarlo!'
            });
        }

    });
    /*******************************************************************
*  Metodo que controla el 
*  boton ver de la carga
* ******************************************************************/

    $("#btn-add-almacen").click(function () {
        var ruta = "/Almacenes/VistaCrearAlmacen";
        showModal("POST", ruta, null, null);//mandamos llamar el modal
    });

    /*******************************************************************
    *  Metodo que controla el 
    *  boton ver de la carga
    * ******************************************************************/

    $("#btn-edit-almacen").click(function () {
        if (filaSeleccionada() == true) {
            var arrayCarga = table.row('.selected').data(); //Solo va a existir el array si se selecciona

            var Data = {};
            var idAlmacen = arrayCarga["id"];

            var ruta = "/Almacenes/editarAlmacen?id=" + idAlmacen;
            showModal("POST", ruta, null, null);//mandamos llamar el modal
        } else {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            //Tostada de error
            Toast.fire({
                icon: 'error',
                title: '¡Seleccione un elemento para observarlo!'
            });
        }

    });

});



