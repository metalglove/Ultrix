function createCollection() {
    var form = getFormData($("#CreateCollectionForm"));
    if (form["CollectionName"] == "") {
        M.toast({ html: "Please enter a collection name first." });
        return;
    }
    var addMemeUrl = "/CreateCollection";

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
            console.log("CreateCollection form resulted faulty..");
        }
    });
}
function deleteCollection(formId, collectionDiv) {
    var form = getFormData($("#" + formId));
    if (form["Id"] == "") {
        M.toast({ html: "Something happened try again later." });
        return;
    }
    var addMemeUrl = "/DeleteCollection";

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
            if (data.success)
                document.getElementById(collectionDiv).remove();

            M.toast({ html: data.message });
        },
        error: function () {
            console.log("DeleteCollection form resulted faulty..");
        }
    });
}
function DisplayErrors(errors) {
    for (var i = 0; i < errors.length; i++) {
        $("<label for='" + errors[i].Key + "' class='error'></label>")
            .html(errors[i].Value[0]).appendTo($("input#" + errors[i].Key).parent());
    }
}