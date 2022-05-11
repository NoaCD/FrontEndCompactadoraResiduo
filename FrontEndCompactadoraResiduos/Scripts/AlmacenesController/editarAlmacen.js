/************************************************************
 * Js que controla la vista de editar Proveedor
 * envia los datos necesarios para Editar a los usuarios
 * 
 * ********************************************************/


$(document).ready(function () {
    init(); //funcion para iniciar le programa
    validarForm('#form-edit-almacen'); //validamos le formulario
});

/**
 * Al hacer click en guardar
 * */
function init() {
    $("#btn-save-almacen").click(function (e) {
        e.preventDefault();
        if ($("#form-edit-almacen").valid() == true) {
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
        id: $("#idAlmacen").val(),
        nombre: $("#nombre").val(),
        clave: $("#clave").val(),
        ubicacion: $("#ubicacion").val(),
        Default: $("#Default").val(),
    }

    Data["datos"] = JSON.stringify(datos);
    console.log(Data);
    var ruta = "/Almacenes/GuardarEdicionAlmacen"
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
                    window.location.replace("/Almacenes/Index");
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
                maxlength: 200
            },

            'clave': {
                required: false,
                maxlength: 34
            },

            'ubicacion': {
                required: false,
                maxlength: 100
            }

        },
        messages: {
            'nombre': {
                required: 'Este campo es obligatorio',
                maxlength: '200 caracteres es lo maximo'
            },
            'clave': {
                maxlength: '34 caracteres es lo maximo'
            },
            'ubicacion': {
                maxlength: '100 caracteres es lo maximo'
            }
        }
    });
}