var isInCallback = false;
var hasReachedScrollEnd = false;

var scrollHandler = function () {
    if (hasReachedScrollEnd == false && ($(document).scrollTop() + $(window).height() + 700 >= $(document).height())) {
        loadData(url);
    }
}

function loadData(loadAnotherItemUrl) {
    return new Promise((resolve, reject) => {
        if (!isInCallback) {
            isInCallback = true;
            hasReachedScrollEnd = true;
            $("div#loading").show();
            Promise.all([
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl),
                getMeme(loadAnotherItemUrl)
            ]).then(values => {
                $("div.infinite-scroll").append(values.join(""));
                $("div#loading").hide();
                isInCallback = false;
                hasReachedScrollEnd = false;
            }, reason => {
                console.log(reason)
                isInCallback = false;
                hasReachedScrollEnd = false;
            });
        }
    });
}
function getMeme(loadAnotherItemUrl) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: 'GET',
            url: loadAnotherItemUrl,
            data: "",
            success: function (data, textstatus) {
                if (data != '') {
                    resolve(data);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                reject(errorThrown);
            }
        });
    });
}