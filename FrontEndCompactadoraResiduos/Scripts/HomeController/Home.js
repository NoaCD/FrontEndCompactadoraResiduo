

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

    console.log(mensaje);
    console.log(typeof (mensaje));

    if (estatus == "success") {
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
        if (mensaje == "") {
            Toast.fire({
                icon: 'error',
                title: 'No hay conexion con el API'
            })
        } else {
            Toast.fire({
                icon: 'error',
                title: mensaje
            })
        }




    }
}