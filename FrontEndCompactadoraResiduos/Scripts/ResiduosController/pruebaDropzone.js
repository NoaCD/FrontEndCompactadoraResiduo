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
        dictFileTooBig: "El archivo es demasiado grande no puede excederter de 1 MB",

        //change the previewTemplate to use Bootstrap progress bars

    });


} catch (e) {
    //  alert('Dropzone.js does not support older browsers!');
}

