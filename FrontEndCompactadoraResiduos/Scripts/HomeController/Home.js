

/*****************************************
 *Js de la pagina principa de HOME par cargar el dashoboard
 * 
 * 
 * ********************************************/

$(document).ready(function () {

    init(); //funcion para iniciar le programa
 

});


function init() {

    var mensaje = $("#mensaje").val();
    var estatus = $("#estatus").val();
    if (estatus == "error") {
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
            title: mensaje
        })

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
            icon: 'success',
            title: mensaje
        })
    }
}