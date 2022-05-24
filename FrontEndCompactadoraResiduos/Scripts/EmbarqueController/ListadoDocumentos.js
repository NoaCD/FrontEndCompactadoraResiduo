
$(document).ready(function () {
    CrearDatatable();
    botonreimprimir();

});


function CrearDatatable() {
    var table = $('#tableReport').DataTable({
        ajax: '/Embarque/todosEmbarques',
        columns: [
            {
                className: 'dt-control',
                orderable: false,
                data: null,
                defaultContent: '',
            },
            { data: 'folioEmbarque' },
            { data: 'area' },
            { data: 'fechaEmbarque' },
        ],
        order: [[2, 'desc']],
    });


  
    filtrarFechas(table);

    // Add event listener for opening and closing details
    $('#tableReport tbody').on('click', 'td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });

}

function filtrarFechas(table) {
    ///Calendario date picker

    // Date range vars
    minDateFilter = "";
    maxDateFilter = "";

    $("#daterange").daterangepicker();
    $("#daterange").on("apply.daterangepicker", function (ev, picker) {
        minDateFilter = Date.parse(picker.startDate);
        maxDateFilter = Date.parse(picker.endDate);


        $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
            var date = Date.parse(data[3]); //IMPORTANTE, EL data[posicion de la columna a filtrar, debe ser de tipo fecha la columna]
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
}

function botonreimprimir() {
    $("#imprimir1").click(function (e) {

        alert("hola");
        e.preventDefault();

        /*imprimiproceso("POST",url,null,'botonestarjetamodal');
         from="#nuevatarjeta";
         validar(form);
         
         */

    });
}

function imprimir(id) {
    window.open('/Embarque/MostrarPDFDesign/' + id);
}

/* Formatting function for row details - modify as you need */
function format(d) {
    var proveedorBasura = '';
    var totalPacas;
    var trs = ''; //just a variable to construct
    totalPacas = d.informacionEmbarque.length;
    idEmbarque = d.id;
    d.informacionEmbarque.forEach((element, index, array) => {
        trs += '<tr><th scope="row"></th><th scope="row">' + (index + 1) + '</th><td>' + element.folioCarga + '</td><td>' + element.nombreResiduo + '</td><td> ' + (element.pesoBrutoCarga - element.pesoContenedorCarga) + ' KG.' + ' </td> ';
        proveedorBasura = element.nombreProveedorBasura;
    });


    var hola = "hola";
    // `d` is the original data object for the row
    return (
        '<div class="table-responsive">' +
        '<button class="btn btn-success float-right" type="button" onClick="imprimir('+d.id+')"> Imprimir este reporte</button>' +
        '<table class"table">' +
        '<thead class="thead-dark"> + <tr> <th scope="col"></th><th scope="col">#</th> <th scope="col">Folio Carga</th> <th scope="col">Nombre Residuo</th><th scope="col">Peso Neto</th> </tr> </thead >'
        + '<tbody>' +
        trs +
        '</tbody>' +
        '</table>' + '</div>'
        + '<br/>' +
        '<div class="d-flex justify-content-between">' +
        '<h4>Recolectado por: <em>' + proveedorBasura + '</em> </h4>' +
        '<h4>Total cargas enviadas: <em>' + totalPacas + '</em></h4>'
        + '</div>'
    );
}


