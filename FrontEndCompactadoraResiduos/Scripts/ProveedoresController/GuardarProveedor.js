/************************************************************
 * Js que controla la vista de crear Proveedor
 * envia los datos necesarios para crear a los Proveedores
 * 
 * ********************************************************/



$(document).ready(function () {

    init(); //funcion para iniciar le programa
    validarForm('#form-create-proveedor'); //validamos le formulario

});

/**
 * Al hacer click en guardar
 * */
function init() {
    $("#btn-save-proveedor").click(function (e) {
        e.preventDefault();
        if ($("#form-create-proveedor").valid() == true) {
            //Una vez validado enviamos el objeto al controlador
            enviarDatos();
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
                icon: 'info',
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
        nombre: $("#nombre").val(),
        direccion: $("#direccion").val(),
        rfc: $("#rfc").val(),
        descripcion: $("#descripcion").val(),
    }

    Data["datos"] = JSON.stringify(datos);
    console.log(Data);
    var ruta = "/Proveedores/GuardarProveedor"
   
    enviarControlador("POST", ruta, Data, false);
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

                Swal.fire(
                    response.mensaje,
                    'Presiona para continuar',
                    response.estatus,
                ).then(function () {
                    window.location.replace("/Proveedores/Index");
                });
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
                maxlength: 50
            },

            'direccion': {
                required: false,
                maxlength: 100
            },

            'rfc': {
                required: false,
                maxlength: 14
            },
            'descripcion': {
                required: false,
                maxlength: 240
            },

        },
        messages: {
            'nombre': {
                required: 'Este campo es obligatorio',
                maxlength: '50 caracteres es lo maximo'
            },
            'direccion': {
                maxlength: '100 caracteres es lo maximo'
            },
            'rfc': {
                maxlength: '14 caracteres es lo maximo'
            },
            'descripcion': {
                maxlength: '240 son los caracteres maximos'
            },
        }
    });
}