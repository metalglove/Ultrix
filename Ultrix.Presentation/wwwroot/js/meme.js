function sendLike(formId) {
    var form = getFormData($("#" + formId));
    var url = "/Like";
    if (form["IsLiked"] === "true")
        url = "/UnLike";

    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(form),
        success: function (data) {
            if (data.success === true)
            {
                if (data.liked === true) {
                    M.toast({ html: "Successfully Liked!" });
                    $("#" + form["MemeId"] + "LikeIcon").css("color", "blue"); // update icon
                    $("#" + form["MemeId"] + "DislikeIcon").css("color", "black"); // update icon
                    $("#" + form["MemeId"] + "IsLiked").val(true); // update IsLiked status
                    $("#" + form["MemeId"] + "IsDisliked").val(false); // update IsDisliked status
                } else {
                    M.toast({ html: "Successfully UnLiked!" });
                    $("#" + form["MemeId"] + "LikeIcon").css("color", "black"); // update icon
                    $("#" + form["MemeId"] + "IsLiked").val(false); // update IsLiked status
                }
            }
            else {
                M.toast({ html: "Something happened, try again later..." });
            }
        },
        error: function () {
            console.log("sendLike form resulted faulty..");
        }
    });
}

function sendDislike(formId) {
    var form = getFormData($("#" + formId));
    var url = "/Dislike";
    if (form["IsDisliked"] === "true")
        url = "/UnDislike";

    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(form),
        success: function (data) {
            if (data.success === true)
            {
                if (data.disliked === true) {
                    M.toast({ html: "Successfully Disliked!" });
                    $("#" + form["MemeId"] + "DislikeIcon").css("color", "blue"); // update icon
                    $("#" + form["MemeId"] + "LikeIcon").css("color", "black"); // update icon
                    $("#" + form["MemeId"] + "IsDisliked").val(true); // update IsDisliked status
                    $("#" + form["MemeId"] + "IsLiked").val(false); // update IsLiked status
                } else {
                    M.toast({ html: "Successfully UnDisliked!" });
                    $("#" + form["MemeId"] + "DislikeIcon").css("color", "black"); // update icon
                    $("#" + form["MemeId"] + "IsDisliked").val(false); // update IsDisliked status
                }
            }
            else {
                M.toast({ html: "Something happened, try again later..." });
            }
        },
        error: function () {
            console.log("sendDislike form resulted faulty..");
        }
    });
}
function getFormData($form) {
    var unIndexedArray = $form.serializeArray();
    var indexedArray = {};

    $.map(unIndexedArray, function (n, i) {
        indexedArray[n["name"]] = n["value"];
    });

    return indexedArray;
}