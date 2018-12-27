function sendLike(formId) {
    var form = getFormData($("#" + formId));
    var likeUrl = "/Like";
    if (form["IsLiked"] === "true")
        likeUrl = "/UnLike";

    $.ajax({
        type: "POST",
        url: likeUrl,
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
    var dislikeUrl = "/Dislike";
    if (form["IsDisliked"] === "true")
        dislikeUrl = "/UnDislike";

    $.ajax({
        type: "POST",
        url: dislikeUrl,
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

function shareMemeToFriend(formId) {
    var form = getFormData($("#" + formId));
    if (form["MutualId"] == undefined) {
        M.toast({ html: "Select a friend from the friends select menu first." });
        return;
    }
    var shareMemeUrl = "/ShareMemeToFriend";

    $.ajax({
        type: "POST",
        url: shareMemeUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(form),
        success: function (data) {
            if (data.success === true) {
                M.toast({ html: "Successfully Shared to " + data.to + "!" });
            }
            else {
                M.toast({ html: "Something happened, try again later..." });
            }
        },
        error: function () {
            console.log("shareMemeToFriend form resulted faulty..");
        }
    });
}

function markAsSeen(formId) {
    var form = getFormData($("#" + formId));
    if (form["Id"] == undefined) {
        M.toast({ html: "Something happend try again later." });
        return;
    }
    var markMemeAsSeenUrl = "/MarkMemeAsSeen";

    $.ajax({
        type: "POST",
        url: markMemeAsSeenUrl,
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
            console.log("MarkMemeAsSeen form resulted faulty..");
        }
    });
}