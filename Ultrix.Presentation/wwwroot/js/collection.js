function addMemeToCollection(formId) {
    var form = getFormData($("#" + formId));
    var url = "/AddMemeToCollection";

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
            if (data.success === true) {
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