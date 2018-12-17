// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded",
    function () {
        const dropdownTriggeredElements = document.querySelectorAll(".dropdown-trigger");
        $('.dropdown-trigger').click(function () {
            M.Dropdown.init(dropdownTriggeredElements,
                {
                    constrainWidth: false,
                    belowOrigin: true,
                    closeOnClick: false
                });
        });
    });

function getFormData($form) {
    var unIndexedArray = $form.serializeArray();
    var indexedArray = {};

    $.map(unIndexedArray, function (n, i) {
        indexedArray[n["name"]] = n["value"];
    });

    return indexedArray;
}