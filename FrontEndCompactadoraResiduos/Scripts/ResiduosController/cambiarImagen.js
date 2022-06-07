
/***
 * 
 * INSTANCIAMOS EL PLUGIN DROPZONE PARA LAS IMAGENES
 * */
Dropzone.autoDiscover = false;
try {
    var myDropzone = new Dropzone("#dropzone", {
        url: "/Residuos/UpdateImagen", //Url que apunta a un controlador
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

        dictFileTooBig: "El archivo es demasiado grande no puede excederse de 1 MB",

        init: function () {
            var submitButton = document.querySelector("#btn-imgupdate-residuo")
            myDropzone = this;//clausula
            submitButton.addEventListener("click", function (e) {
                e.preventDefault();
                e.stopPropagation();
                myDropzone.processQueue();



            });
            //Cuando se envie que envia estos documentos igual
            this.on("sending", function (file, xhr, formData) {
                formData.append("iId", $("#id").val());


            });

            this.on("success", function (file, responseText) {

                msjok();
            });
            this.on('error', function (file, errorMessage) {
                msjError(errorMessage);
                myDropzone.removeFile(file);

            });

        }
    });
} catch (e) {
    //  alert('Dropzone.js does not support older browsers!');
}

/**
 * MFuncion para mostrar error
 * @param {any} errorMessage el mensjae string
 */
function msjError(errorMessage = "") {
    Swal.fire(
        errorMessage,
        'Presiona para continuar',
        'info',
    );
}
///Mensaje de OK 
function msjok() {
    Swal.fire(
        'Imagen ha sido actualizado con exito!!!',
        'Presiona para continuar',
        'success',
    ).then(function () {
        window.location.replace("/Residuos/Index");
    });
}

