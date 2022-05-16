

/**
 * 
 * @param {any} cTipo get/post/delete
 * @param {any} cUrl donde va a enviar la data
 * @param {any} Data json 
 * @param {any} Funcion 
 * @param {any} ruta cuando retorne un success donde va a enviar la data
 */
function globalEnviarControlador(cTipo, cUrl, Data, Funcion, ruta) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: Data,
        dataType: "JSON",
        success: function (response) {
            if (response.estatus == "ok") {
                Swal.fire(
                    response.mensaje,
                    'Presiona para continuar',
                    'success',
                ).then(function () {
                    window.location.replace(ruta);
                });
            }
            else {
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
            if (Funcion)
                window[Funcion]();
        }
    });
}
