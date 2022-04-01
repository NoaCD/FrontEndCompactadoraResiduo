/***************************************
 * 
 * Javascript que controla la tabla de los usuarios
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
    $("#btn-add-user").click(function () {
        var ruta = "/Usuarios/CrearUsuario";
        showModal("POST", ruta, null, null);//mandamos llamar el modal


    });


    /*******************************************************************
     *  Metodo que controla el 
     *  boton ver 
     * ******************************************************************/

    $("#btn-see-user").click(function () {

        if (filaSeleccionada() == true) {
            var arrayUsuario = table.row('.selected').data(); //Solo va a existir el array si se selecciona
            var Data = {};
            var id = arrayUsuario[0];
            var datos = {
                iId: id
            }
            Data["datos"] = JSON.stringify(datos);
            console.log(Data);

            var ruta = "/Usuarios/VerUsuario";
            console.log(Data);
            showModal("POST", ruta, Data, null);//mandamos llamar el modal


        } else {
            //Tostada de error
            Toast.fire({
                icon: 'error',
                title: '¡Seleccione un elemento para observarlo!'
            });
        }

    });

    /***********************************************************************************
     *
     * funcion para el boton de editar 
     * 
     * *********************************************************************************
     * */
    $('#btn-edit-user').click(function () {
        if (filaSeleccionada() == true) {
            var arrayUsuario = table.row('.selected').data(); //Solo va a existir el array si se selecciona
            var Data = {};
            var id = arrayUsuario[0];
            var datos = {
                iId: id
            }
            Data["datos"] = JSON.stringify(datos);
            console.log(Data);

            var ruta = "/Usuarios/EditarUsuario";
            showModal("POST", ruta, Data, null);//mandamos llamar el modal

        } else {

            Toast.fire({
                icon: 'error',
                title: '¡Seleccione un elemento para editarlo!'
            });
        }

    });

    /***********************
     * Metodo que controla el 
     * boton eliminar
     ************************/

    $('#btn-delete-user').click(function () {
        if (filaSeleccionada() == true) {
            Swal.fire({
                title: '¿Estas seguro que deseas eliminar a este usuario?',
                text: "¡No podrás revertir esto!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: "Cancelar",
                confirmButtonText: 'Si, si estoy seguro!'
            }).then((result) => {
                if (result.isConfirmed) {
                    //Funcion eliminar en la API
                    var arrayUsuario = table.row('.selected').data(); //Solo va a existir el array si se selecciona
                    var Data = {};
                    var id = arrayUsuario[0];
                    var datos = {
                        iId_User: id
                    }
                    Data["datos"] = JSON.stringify(datos);
                    console.log(Data);

                    var ruta = "/Usuarios/EliminarUsuario"
                    var postRuta = "/Usuarios/CatalogoUsuarios"
                    globalEnviarControlador("POST", ruta, Data, false, postRuta);
                   

                }
            })
        } else {
            Toast.fire({
                icon: 'error',
                title: '¡Seleccione un elemento para eliminarlo!'
            });
        }



    })

    /*Agregamos enumeraciones a la primera columna */
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

});


