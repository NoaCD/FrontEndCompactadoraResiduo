

$(document).ready(function () {
    var table = $('#tableResiduos').DataTable(
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


    $('#tableResiduos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });


    var filaSeleccionada = function () {
        var arrayUsuario = table.row('.selected').data(); //Solo va a existir el array si se selecciona
        if (arrayUsuario != null) {
            return true;
        } else {
            return false
        }
    }



    /*********************************
       * Declaramos una tostada flotante 
       * para que la podamos usar luego
       *********************************/
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




    /**************************************************************
 * Controla el boton agregar
 * abre un modal que lleva a un form para enviar datos
 * *************************************************************/
    $("#btn-add-residuo").click(function () {
        var ruta = "/Residuos/CrearResiduo";
        showModal("POST", ruta, null, null);//mandamos llamar el modal


    });




    /***********************************************************************************
 *
 * funcion para el boton de editar 
 * 
 * *********************************************************************************
 * */
    $("#btn-edit-residuo").click(function () {
        if (filaSeleccionada() == true) {
            var arrayResiduo = table.row('.selected').data(); //Solo va a existir el array si se selecciona
            var Data = {};
            var id = arrayResiduo[0];
            var datos = {
                iId: id
            }
            Data["datos"] = JSON.stringify(datos);
           

            var ruta = "/Residuos/EditarResiduo";
            showModal("POST", ruta, Data, null);//mandamos llamar el modal

        } else {

            Toast.fire({
                icon: 'error',
                title: '¡Seleccione un elemento para editarlo!'
            });
        }

    });






    /*Agregamos enumeraciones a la primera columna */
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();


});


