$(document).ready(function () {

    init(); //funcion para iniciar le programa
    validarForm('#form-actualizar-carga'); //validamos le formulario

});


function init() {
    $("#btn-actualizar-carga").click(function (e) {
        e.preventDefault();
        if ($("#form-actualizar-carga").valid() == true) {
            //Una vez validado enviamos el objeto al controlador
            console.log("Es valido")
            enviarDatos();
        } else {
            console.log("No es valido")
            Toast.fire({
                icon: 'error',
                title: '¡Completa todos los campos, por favor!'
            });
        }
    })
}


/**
 * Envia al controlador para guardar los cambios
 * @param {any} cTipo Metodo por el cual se va a ejecutar
 * @param {any} cUrl
 * @param {any} Data
 * @param {any} Funcion
 */
function enviarControlador(cTipo, cUrl, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        dataType: "json",
        data: Data,
        success: function (response) {
            if (response.estatus == "error") {
                const Toast = Swal.mixin({
                    Toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                    icon: 'error'
                })
                Toast.fire({
                    type: response.estatus,
                    title: response.mensaje
                })
            }
            else {
                Swal.fire(
                    'Carga editada correctamente',
                    'Presiona para continuar',
                    'success',
                ).then(function () {
                    window.location.replace("/Carga/Index");
                });
            }
            if (Funcion)
                window[Funcion]();
        }
    });
}


/**
 * Funcion para enviar los inputs al controlador
 * 
 ** */
function enviarDatos() {
    var Data = {};
    var datos = {
        iId: $("#idCarga").val(),
        iId_Residuo : $("#idResiduo").val(),
        iId_User  : $("#idUsuario").val(),
        dPesoBruto : $("#pesoBruto").val(),
        dPesoContenedor : $("#pesoContenedor").val(),
        cComentario: $("#comentarioCarga").val(),
        eliminar: $('input:checkbox[name=eliminarCheck]:checked').val()
    }
    console.log(datos);

    var ruta = "/Carga/ActualizarCarga";
    Data["datos"] = JSON.stringify(datos);
    enviarControlador("PUT", ruta, Data, false);
}



/**
 * Funcion para validar el formulario
 * 
 * @param {any} formulario
 */
function validarForm(formulario) {

    $(formulario).validate({
        rules: {
            'idUsuario': {
                required: true,
                number: true
            },
            'idCarga': {
                required: true,
                number: true
            },
            'idResiduo': {
                required: true,
                number: true
            },

            'pesoBruto': {
                required: true,
                number: true
            },

 
        },
        messages: {
            'idUsuario': {
                required: 'Es requerido',
                number: 'Tiene que ser numero'
            },
            'idCarga': {
                required: 'Es requerido',
                number: 'Tiene que ser numero'
            },
            'idResiduo': {
                required: 'Es requerido',
                number: 'Tiene que ser numero'
            },
            'nombre': {
                required: 'Es requerido',
                minlength: 'Minimo 2 caracteres',
                maxlength: 'Maximo 30 caracteres'
            },
            'pesoBruto': {
                required: 'Es requerido',
                minlength: 'Minimo 2 caracteres',
                maxlength: 'Maximo 30 caracteres'
            },

        }
    });
}