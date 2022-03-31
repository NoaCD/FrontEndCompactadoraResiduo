/************************************************************
 * Js que controla la vista de crear usuario
 * 
 * 
 * 
 * ********************************************************/











/*
 * Cargamos la tostada
 */
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

$(document).ready(function () {

    init(); //funcion para iniciar le programa
    validarForm('#form-create-user'); //validamos le formulario

});

/**
 * Al hacer click 
 * */
function init() {
    $("#btn-save-user").click(function (e) {
        e.preventDefault();
        if ($("#form-create-user").valid() == true) {
            //Una vez validado enviamos el objeto al controlador
            enviarDatos();
        } else {
            Toast.fire({
                icon: 'error',
                title: '¡Completa todos los campos, por favor!'
            });
        }
    })
}


/**
 *Enviar datos 
 * */
function enviarDatos() {

    var Data = {};
    var datos = {
        cNombre: $("#nombre").val(),
        cApellidoPaterno: $("#apellidoPaterno").val(),
        cApellidoMaterno: $("#apellidoMaterno").val(),
        iNumeroEmpleado: $("#numeroEmpleado").val(),
        cNombreUsuario: $("#nombreUsuario").val(),
        cContrasenia: $("#contrasenia").val(),
        iId_TipoUsuario: $("#tipoUsuario").val(),
    }

    Data["datos"] = JSON.stringify(datos);
    var ruta = "/Usuarios/GuardarUsuario"
    enviarControlador("POST", ruta , Data, false);
}


/**
 * Funcion para crear un usuario
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
                    title: 'El usuario' + ' '+ response.data +' ' + 'se ha creado Exitosamente'
                })
                setTimeout(() => {
                    //Esperamos 30 segundoas para que se cambie de vista para que se vea el mensaje de ok
                    window.location.replace("/Usuarios/CatalogoUsuarios");
                
                }, 2500);
            
            }
            if (Funcion)
                window[Funcion]();
        }
    });
}





/**
 * Funcion para validar el formulario
 * 
 * @param {any} formulario
 */
function validarForm(formulario) {

    $(formulario).validate({
        rules: {
            'nombre': {
                required: true,
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
            'nombreUsuario': {
                required: true,
                minlength: 5,
                maxlength: 25
            },
            'numeroEmpleado': {
                required: true,
                number: true,
            },
            'contrasenia': {
                required: true,
                minlength: 8,
                maxlength: 15
            }
        },
        messages: {
            'nombre': 'El nombre es obligatorio',

            'apellidoPaterno': {
                required: 'El apellido es obligatorio',
                minlength: 'Debe ser mayor que 2 letras'
            },
            'apellidoMaterno': {
                required: 'El apellido es obligatorio',
                minlength: 'Debe ser mayor que 2 letras'
            },
            'nombreUsuario': {
                required: 'Es requerida',
                minlength: 'Debe ser mayo a 5 digitos',
                maxlength: 'No debe ser mayor a 20'
            },
            'contrasenia': {
                required: 'Es requerida',
                minlength: 'Debe ser mayor que 8 digitos',
                maxlength: 'No debe ser mayor a 15 digitos'
            },
            'numeroEmpleado': {
                number: "Solo admmite numeros",
                required:"Es requerido"

            }
        }
    });
}