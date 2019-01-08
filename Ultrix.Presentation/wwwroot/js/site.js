// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded",
    function () {
        $(".dropdown-trigger").dropdown({
            constrainWidth: false,
            belowOrigin: true,
            closeOnClick: false
        });
        //const dropdownTriggeredElements = document.querySelectorAll(".dropdown-trigger");
        //$(".dropdown-trigger").click(function () {
        //    M.Dropdown.init(dropdownTriggeredElements,
        //        {
        //            constrainWidth: false,
        //            belowOrigin: true,
        //            closeOnClick: false
        //        });
        //});
    });

function getFormData($form) {
    var unIndexedArray = $form.serializeArray();
    var indexedArray = {};

    $.map(unIndexedArray, function (n, i) {
        indexedArray[n["name"]] = n["value"];
    });

    return indexedArray;
}

function displayErrors(data) {
    for (let i = 0; i < Object.keys(data.errors).length; i++) {
        const errorHeader = Object.keys(data.errors)[i];
        let errorMessages = "";
        for (let j = 0; j < data.errors[errorHeader].length; j++) {
            errorMessages += `<li>${data.errors[errorHeader][j]}</li>`;
        }
        M.toast({ html: `<div><b>${errorHeader}</b><ul>${errorMessages}</ul></div>` });
    }
}