
$(document).ready(function () {

    init(); //funcion para iniciar le programa
    validarForm('#form-actualizar-usuario'); //validamos le formulario

});


function init() {
    $("#btn-actualizar-user").click(function (e) {
        e.preventDefault();
        if ($("#form-actualizar-usuario").valid() == true) {
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
        data: Data,
        dataType: "JSON",
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
                    'Usuario editado correctamente',
                    'Presiona para continuar',
                    'success',
                ).then(function () {
                    window.location.replace("/Usuarios/CatalogoUsuarios");
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
        iId: $("#idUsuario").val(),
        iId_Estatus: $("#idEstatus").val(),
        iId_TipoUsuario: $("#idTipoUsuario").val(),
        iNumeroEmpleado: $("#numeroEmpleado").val(),
        cNombre: $("#nombre").val(),
        cApellidoPaterno: $("#apellidoPaterno").val(),
        cApellidoMaterno: $("#apellidoMaterno").val(),
  


    }

    Data["datos"] = JSON.stringify(datos);
    var ruta = "/Usuarios/ActualizarUsuario"
    enviarControlador("POST", ruta, Data, false);
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
                number:true
            },
            'idTipoUsuario': {
                required: true,
                number: true
            },
            'idEstatus': {
                required: true,
                number: true
            },
            'nombre': {
                required: true,
                minlength: 2,
                maxlength: 30
            },
            'apellidoPaterno': {
                required: true,
                minlength: 2,
                maxlength: 30
            },

            'apellidoMaterno': {
                required: true,
                minlength: 2,
                maxlength: 30
            },
     
            'numeroEmpleado': {
                required: true,
                number: true,

            },
           
        },
        messages: {
            'idUsuario': {
                required: 'Es requerido',
                number: 'Tiene que ser numero'
            },
            'idTipoUsuario': {
                required: 'Es requerido',
                number: 'Tiene que ser numero'
            },
            'idEstatus': {
                required: 'Es requerido',
                number: 'Tiene que ser numero'
            },
            'nombre': {
                required: 'Es requerido',
                minlength: 'Minimo 2 caracteres',
                maxlength: 'Maximo 30 caracteres'
            },
            'apellidoPaterno': {
                required: 'Es requerido',
                minlength: 'Minimo 2 caracteres',
                maxlength: 'Maximo 30 caracteres'
            },

            'apellidoMaterno': {
                required: 'Es requerido',
                minlength: 'Minimo 2 caracteres',
                maxlength: 'Maximo 30 caracteres'
            },

            'numeroEmpleado': {
                required: 'Este campo es requerido',
                number: 'Tiene que ser un numero entero',
                

            },
        }
    });
}