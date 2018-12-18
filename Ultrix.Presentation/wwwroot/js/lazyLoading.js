var isInCallback = false;
var hasReachedScrollEnd = false;

var scrollHandler = async function () {
    if (hasReachedScrollEnd === false && ($(document).scrollTop() + $(window).height() + 700 >= $(document).height())) {
        await loadData(memeUrl);
    }
}

async function loadData(loadAnotherItemUrl) {
    return await new Promise(async (resolve, reject) => {
        if (!isInCallback) {
            isInCallback = true;
            hasReachedScrollEnd = true;
            $("div#loading").show();
            await Promise.all([
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
            ]).then(values => {
                $("div.infinite-scroll").append(values.join(""));
                $("div#loading").hide();
                isInCallback = false;
                hasReachedScrollEnd = false;
            }, reason => {
                console.log(reason);
                isInCallback = false;
                hasReachedScrollEnd = false;
            });
        }
    });
}
function getMeme(loadAnotherItemUrl) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            url: loadAnotherItemUrl,
            data: "",
            success: function (data, textstatus) {
                if (data !== "") {
                    resolve(data);
                }
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                reject(errorThrown);
            }
        });
    });
}