$(document).ready(function () {
    btnIniciar();
    ValidarDatos("#FrmAcceso");
})

/**Evento para el botón entrar**/
function btnIniciar() {
    $("#btnIniciar").click(function (e) {
        e.preventDefault();
        if ($("#FrmAcceso").valid() == true) {
            enviarDatos();
        } else {
            const Toast = Swal.mixin({
                Toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                icon: 'error'
            })

            Toast.fire({
                type: 'error',
                title: 'ingrese un correo y su contraseña'
            })
        }
    })
}

/**
 * Enviar los datos del formulario al controlador
 * **/
function enviarDatos() {
    var ruta = rutaurl();
    var Data = {};
    var datos = {
        cNombreUsuario: $("#cUser").val(),
        cContrasenia: $("#cPassword").val(),
    }
    Data["datos"] = JSON.stringify(datos);
    LlamarMetodo("POST", ruta + "Login/Acceso", Data, false);
}
/**
 * Generando ruta url
 * */
function rutaurl() {
    var ruta = window.location.host;
    var protocolo = window.location.protocol;
    return protocolo + "//" + ruta + "/"
}
/**
 * Llama el metodo al cual se desea inicializar
 * @param {any} cTipo
 * @param {any} cUrl
 * @param {any} Data
 * @param {any} Funcion
 */
function LlamarMetodo(cTipo, cUrl, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: Data,
        dataType: "JSON",
        success: function (response) {
            if (response.data != true) {
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
                    title: 'Bienvenido ' + response.mensaje
                })
                var ruta = rutaurl()
                window.location.replace(ruta + "Home/index");
            }
            if (Funcion)
                window[Funcion]();
        }
    });
}

/**
 *
 * @param {any} IdFormulario id del formulario a validad
 * Valida el formulario
 */
function ValidarDatos(IdFormulario) {
    $(IdFormulario).validate({
        errorClass: "is-invalid",
        validCalss: "is-valid",
        rules: {
            cUser: {
                required: true,
                email: true
            },
            cPassword: {
                required: true
            },
            messajes: {
                cUser: {
                    required: "Ingrese un correo.",
                    email: "Ingrese un correo Valido"
                },
                cPassword: {
                    required: "Ingrese su contraseña.",
                    logitu: "Ingrese una contraseña "
                }
            }
        }
    })
}