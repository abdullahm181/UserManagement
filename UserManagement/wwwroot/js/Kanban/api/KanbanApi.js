
export default class KanbanAPI {
    static Methode(TypeMethode, requestUri, BodyData = null, callback) {
        var data;
        const Get = $.ajax({
            type: `${TypeMethode}`,
            url: `/${requestUri}`,
            data: BodyData ? BodyData : null,
            beforeSend: function () { // Before we send the request, remove the .hidden class from the spinner and default to inline-block.
                $('#loader').removeClass('hidden')
            },
            success: function (resp) {

                if (/<[a-z]+\d?(\s+[\w-]+=("[^"]*"|'[^']*'))*\s*\/?>|&#?\w+;/i.test(resp)) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error read the response',
                        text: 'Pless reach out the web owner!',
                    });
                }
                data = resp;
                callback(data);
            },
            complete: function () { // Set our complete callback, adding the .hidden class and hiding the spinner.
                setTimeout(function () { $('#loader').addClass('hidden') }, 300)
                
            },
        }).fail((error) => {
            console.log(error);
            Swal.fire({
                icon: 'error',
                title: error.status,
                text: error.statusText,
            });
        });
        return data;
    }
    
}