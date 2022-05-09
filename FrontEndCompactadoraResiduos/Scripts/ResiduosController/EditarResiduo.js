
$(document).ready(function () {

    init(); //funcion para iniciar le programa
    validarForm('#form-actualizar-residuo'); //validamos le formulario
});

function init() {
    $("#btn-actualizar-residuo").click(function (e) {
        e.preventDefault();
        if ($("#form-actualizar-residuo").valid() == true) {
            //Una vez validado enviamos el objeto al controlador
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
function enviarResiduo(cTipo, cUrl, Data, Funcion) {
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
                    window.location.replace("/Residuos/Index");
                });
            }
            if (Funcion)
                window[Funcion]();
        }
    });
}


/**
 *Enviar datos POR IFORM FILE | FORM DATA a nuestro cliente
 * El cliente enviara esta data al API
 * EL API RECIBE ESTE FORMULARRIO POR FORM DATA
 * 
 * */
function enviarDatos() {

    var Data = {};
    var datos = {
        iId: $("#iId").val(),
        cNombre: $("#nombre").val(),
        cDescripcion: $("#descripcion").val(),
        cCodigo: $("#codigo").val(),


    }

    Data["datos"] = JSON.stringify(datos);
    var ruta = "/Residuos/ActualizarResiduo"
    enviarResiduo("POST", ruta, Data, false);

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
                minlength: 2,
                maxlength: 30
            },

            'descripcion': {
                required: true,
                minlength: 2,
                maxlength: 195
            },

            'codigo': {
                required: true,
                minlength: 2,
                maxlength: 20
            },

        },
        messages: {
            'nombre': {
                required: 'El nombre es obligatorio',
                maxlength: 'maximo 30 caracteres'
            },

            'descripcion': {
                required: 'La descripcion es obligatorio',
                minlength: 'Debe ser mayor que 2 letras',
                maxlength: ''
            },
            'codigo': {
                required: 'El codigo es obligatorio',
                minlength: 'Debe ser mayor que 2 caracter',
                maxlength: 'No debe ser mayor a 20 caracter '
            },
        }
    });
}