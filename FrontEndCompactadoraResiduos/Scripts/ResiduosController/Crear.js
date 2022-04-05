$(document).ready(function () {

    init(); //funcion para iniciar le programa
    validarForm('#form-create-residuo'); //validamos le formulario

});



/***
 * 
 * INSTANCIAMOS EL PLUGIN DROPZONE PARA LAS IMAGENES
 * */
Dropzone.autoDiscover = false;
try {
    var myDropzone = new Dropzone("#dropzone", {
        paramName: "file", // The name that will be used to transfer the file
        maxFilesize: 1, // MB
        maxFiles: 1,
        acceptedFiles: "image/jpeg,image/png,image/jpg,image/gif",
        addRemoveLinks: true,
        dictDefaultMessage:
            '<span class="bigger-150 bolder"> Arrastra aqui tu </span> IMAGEN \
                    <span class="smaller-80 grey"> o haz CLICK AQUI! </span> <br /> \
                    <i class="large material-icons">insert_photo</i>'
        ,
        dictResponseError: 'Error mientras e subio la Imagen!',
        dictInvalidInputType: 'Solo se adminte archos de imagen',
        dictFileTooBig: "El archivo es demasiado grande no puede excederter de 1 MB"

        //change the previewTemplate to use Bootstrap progress bars

    });
 
} catch (e) {
    //  alert('Dropzone.js does not support older browsers!');
}






/*FUNCIONA QUE SE EJECUTA DE PRIMERO*/
function init() {
    $("#btn-save-residuo").click(function (e) {
        e.preventDefault();
        if ($("#form-create-residuo").valid() == true) {
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
                icon: 'error',
                title: '¡Completa todos los campos, por favor!'
            });
        }
    })
}

/**
 *Enviar datos POR IFORM FILE | FORM DATA a nuestro cliente
 * El cliente enviara esta data al API
 * EL API RECIBE ESTE FORMULARRIO POR FORM DATA
 * 
 * */
function enviarDatos() {

    var cNombre = $("#nombre").val();
    var cDescripcion = $("#descripcion").val();
    var cCodigo = $("#codigo").val();
        
    var fImagen = document.getElementById("imagenResiduo").files[0];

    console.log(fImagen);

    /*PROCEDEMOS A CREAR UN FORM DATA*/
    var formData = new FormData();

    formData.append("cNombre", cNombre);
    formData.append("cDescripcion", cDescripcion);
    formData.append("cCodigo", cCodigo);
    formData.append("fImagen", fImagen);

    //var fImagen = $(document.getElementById("fileResiduo"));


  
    var ruta = "/Residuos/GuardarResiduo"
    enviarControlador("POST", ruta, formData, false);
}


/**
 * Funcion para enviar la data a un controlador por FORM DATA
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
        processData: false,
        contentType: false,
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
                    'El residuo se ha creado con exito!!!',
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

            'descripcion': {
                required: true,
                minlength: 2,
                maxlength: 200
            },

            'codigo': {
                required: true,
                minlength: 2,
                maxlength: 10
            },
            //'nombreUsuario': {
            //    required: true,
            //    minlength: 5,
            //    maxlength: 25
            //},

        },
        messages: {
            'nombre': {
                required: 'El nombre es obligatorio',
                maxlength: 'maximo 30 caracteres'
            },

            'descripcion': {
                required: 'La descripcion es obligatorio',
                minlength: 'Debe ser mayor que 2 letras'
            },
            'codigo': {
                required: 'El codigo es obligatorio',
                minlength: 'Debe ser mayor que 2 letras'
            },
            //'nombreUsuario': {
            //    required: 'Es requerida',
            //    minlength: 'Debe ser mayo a 5 digitos',
            //    maxlength: 'No debe ser mayor a 20'
            //},

        }
    });
}