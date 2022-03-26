/**
 * 
 * @param {any} cTipo 
 * @param {any} cUrl
 * @param {any} Data
 * @param {any} Function
 * 
 */


function showModal(cTipo, cUrl, Data, Function) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: Data,
        dataType: "HTML",
        success: function (response) {
            $("#modal").html(response);
            $("#modal").modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            if (Function)
                window[Function]();
        }
    });
}

