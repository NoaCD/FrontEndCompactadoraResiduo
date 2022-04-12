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
        url: "/Residuos/GuardarResiduo", //Url que apunta a un controlador
        autoProcessQueue: false, // para que no se procese y hasta que escuche el boton submit all envia
        paramName: "file", // The name that will be used to transfer the file
        maxFilesize: 1, // MB tamaño en megas del archivo
        maxFiles: 1, // Maximo de ya
        acceptedFiles: "image/jpeg,image/png,image/jpg,image/gif",
        addRemoveLinks: true,
        dictDefaultMessage:
            '<span class="bigger-150 bolder"> Arrastra aqui tu </span> IMAGEN \
                    <span class="smaller-80 grey"> o haz CLICK AQUI! </span> <br /> \
                    <i class="large material-icons">insert_photo</i>'
        ,
        dictResponseError: 'Error mientras e subio la Imagen!',
        dictInvalidFileType: 'Solo se adminte archos de imagen',

        dictFileTooBig: "El archivo es demasiado grande no puede excederter de 1 MB",

        init: function() {
            //    //myDropzone.processQueue();
            //var submitButton = document.querySelector("#form-create-residuo")
            //myDropzone = this;//clausula
            //submitButton.addEventListener("click", function (e) {
            //    e.preventDefault();
            //    e.stopPropagation();
               


            //});

            //Cuando se envie que envia estos documentos igual
            this.on("sending", function (file, xhr, formData) {
                formData.append("cNombre", $("#nombre").val());
                formData.append("cDescripcion", $("#descripcion").val());
                formData.append("cCodigo", $("#codigo").val());


            });
            this.on("queuecomplete", function () { msjok(); });

        }
    });
}catch (e) {
    //  alert('Dropzone.js does not support older browsers!');
}


///Mensaje de OK 
function msjok() {
    Swal.fire(
        'El residuo se ha creado con exito!!!',
        'Presiona para continuar',
        'success',
    ).then(function () {
        window.location.replace("/Residuos/Index");
    });
}


/*FUNCIONA QUE SE EJECUTA DE PRIMERO*/
function init() {
    $("#btn-save-residuo").click(function (e) {
        e.preventDefault();
        if ($("#form-create-residuo").valid() == true) {
            //Una vez validado enviamos el objeto al controlador
             myDropzone.processQueue(); //va al controlador y devuelve algo
                        
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
                minlength: 2,
                maxlength: 30,
                
            },

            'descripcion': {
                required: true,
                minlength: 2,
                maxlength: 190
            },

            'codigo': {
                required: true,
                minlength: 2,
                maxlength: 25
            }
        },
        messages: {
            'nombre': {
                required: 'El nombre es obligatorio',
                minlength: 'Minimo 2 caracteres',
                maxlength: 'maximo 30 caracteres'
            },

            'descripcion': {
                required:  'La descripcion es obligatorio',
                minlength: 'Debe ser mayor que 2 letras',
                maxlength: 'No de excederse de 195 caracteres'
            },
            'codigo': {
                required : 'El codigo es obligatorio',
                minlength: 'Debe ser mayor que 2 letras',
                maxlength: 'No debe excederse de 25 caracteres'
            }


        }
    });
}