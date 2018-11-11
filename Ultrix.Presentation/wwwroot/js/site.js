// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded",
    function() {
        const dropdownTriggeredElements = document.querySelectorAll(".dropdown-trigger");
        M.Dropdown.init(dropdownTriggeredElements,
            {
                constrainWidth: false,
                belowOrigin: true,
                closeOnClick: false
            });
    });
