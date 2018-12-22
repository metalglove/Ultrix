function addMemeToCollection(formId) {
    var form = getFormData($("#" + formId));
    if (form["collectionId"] == undefined) {
        M.toast({ html: "Select a collection from the collections select menu first." });
        return;
    }
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
            M.toast({ html: data.message });
        },
        error: function () {
            console.log("AddMemeToCollection form resulted faulty..");
        }
    });
}