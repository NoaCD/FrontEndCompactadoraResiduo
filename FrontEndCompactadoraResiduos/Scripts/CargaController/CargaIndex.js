/********************
 * Funcion de la tabla Cargas
 * 
 * **************/

$(document).ready(function () {
    ocultarBoton();
    var estado = 'almacen';

    ///esta funcion es para filtro de mostrar cargas dependiento del estado en que este
    $('#estado').change(function () {
        var activo = ($(this).prop('checked'));
        if (activo == true) {
            estado = "almacen";
            table.search(estado).draw();
        } else {
            estado = "enviado";
            table.search(estado).draw();
        }
    });

    //Ajax de Datatables para las cargas
    var table = $('#example').DataTable({
        responsive: true,
        "ajax": "/carga/apicargas",
        "processing": true,
        "columns": [
            { "data": "idCarga" },
            { "data": "folioCarga" },
            { "data": "nombreResiduo" },
            {
                "data": "nombreEmpleado",
                render: function (data, type, row) {
                    // esto es lo que se va a renderizar como html
                    return `<b>${row.nombreEmpleado}</b> ${row.apellidoPaterno}`;
                }

            },
            {
                "data": "pesoBrutoCarga",
                render: function (data, type, row) {
                    return `<b>${(row.pesoBrutoCarga - row.pesoContenedorCarga).toFixed(2) + ' Kg'}</b>`;

                }
            },
            {
                "data": function (data) {
                    //let fecha = moment(data.fechaCreacionCarga).locale('es-mx').format('LLLL');
                    let fecha = new Date(data.fechaCreacionCarga);
                    return fecha;
                }
            },
            {
                "data": "estadoAlmacenCorto",
                render: function (data, type, row) {
                    // esto es lo que se va a renderizar como html
                    if (row.estadoAlmacenCorto == "almacen") {
                        return `<span class="badge badge-pill badge-success">${row.estadoAlmacenCompleto}</span>`;
                    } else {
                        return `<span class="badge badge-pill badge-danger">${row.estadoAlmacenCompleto}</span>`;
                    }
                }
            }
        ],
        order: [[5, 'desc']],
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
    });

    //FILTROS 


    $('#example tbody').on('click', 'tr', function () {
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


    ///Calendario date picker

    // Date range vars
    minDateFilter = "";
    maxDateFilter = "";

    $("#daterange").daterangepicker();
    $("#daterange").on("apply.daterangepicker", function (ev, picker) {
        minDateFilter = Date.parse(picker.startDate);
        maxDateFilter = Date.parse(picker.endDate);


        $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
            var date = Date.parse(data[5]); //IMPORTANTE, EL data[posicion de la columna a filtrar, debe ser de tipo fecha la columna]
            console.log(date);
            if (
                (isNaN(minDateFilter) && isNaN(maxDateFilter)) ||
                (isNaN(minDateFilter) && date <= maxDateFilter) ||
                (minDateFilter <= date && isNaN(maxDateFilter)) ||
                (minDateFilter <= date && date <= maxDateFilter)
            ) {
                return true;
            }
            return false;
        });

        table.draw();
    });




    /*******************************************************************
     *  Metodo que controla el 
     *  boton ver de la carga
     * ******************************************************************/

    $("#btn-see-carga").click(function () {

        if (filaSeleccionada() == true) {
            var arrayCarga = table.row('.selected').data(); //Solo va a existir el array si se selecciona

            var Data = {};
            var id = arrayCarga["idCarga"];
            var datos = {
                iId: id
            }
            Data["datos"] = JSON.stringify(datos);
            console.log(Data);

            var ruta = "/Carga/DetalleCarga";
            console.log(Data);
            showModal("POST", ruta, Data, null);//mandamos llamar el modal
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
    /***********************************************************************************
    *
    * funcion para el boton de editar 
    * 
    * *********************************************************************************
    * */
    $('#btn-edit-carga').click(function () {
        if (filaSeleccionada() == true) {
            var arrayCarga = table.row('.selected').data(); //Solo va a existir el array si se selecciona
            var Data = {};
            var id = arrayCarga["idCarga"];
            let estado = arrayCarga["estadoAlmacenCorto"];
            var datos = {
                iId: id
            }

            if (estado == "enviado") {
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
                Toast.fire({
                    icon: 'info',
                    title: '¡Esta carga ha sido enviada no puedes eliminarla o modificarla!'
                });
                return;
            }

            Data["datos"] = JSON.stringify(datos);
      
            var ruta = "/Carga/EditarCarga";

            showModal("POST", ruta, Data, null);//mandamos llamar el modal

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

//Esta funcion oculta los botones que no puede hacer el usuario administraador
function ocultarBoton() {
  /*  $('#btn-edit-carga').hide();*/
    $('#btn-delete-carga').hide();
}