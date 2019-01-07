function follow(formId) {
    var form = getFormData($("#" + formId));
    var followUrl = "/Follow";

    $.ajax({
        type: "POST",
        url: followUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(form),
        success: function (data) {
            M.toast({ html: data.message });
            if (!data.success) {
                displayErrors(data);
            }
        },
        error: function () {
            console.log("Follow form resulted faulty..");
        }
    });
}

function unFollow(formId) {
    var form = getFormData($("#" + formId));
    var unFollowUrl = "/UnFollow";

    $.ajax({
        type: "POST",
        url: unFollowUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(form),
        success: function (data) {
            M.toast({ html: data.message });
            if (!data.success) {
                displayErrors(data);
            }
        },
        error: function () {
            console.log("UnFollow form resulted faulty..");
        }
    });
}