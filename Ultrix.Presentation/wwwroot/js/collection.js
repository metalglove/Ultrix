function addMemeToCollection(formId) {
    var form = getFormData($("#" + formId));
    var addMemeUrl = "/AddMemeToCollection";

    $.ajax({
        type: "POST",
        url: addMemeUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(form),
        success: function (data) {
            if (data.success === true) {
                if (data.added === true) {
                    M.toast({ html: "Successfully Added to " + data.collectionName + "!" });
                } else {
                    M.toast({ html: "Something happened, try again later..." });
                }
            }
            else {
                M.toast({ html: "Something happened, try again later..." });
            }
        },
        error: function () {
            console.log("AddMemeToCollection form resulted faulty..");
        }
    });
}