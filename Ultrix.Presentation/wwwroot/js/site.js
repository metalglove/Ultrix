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

        //$('#errorMsg').hide();
        //$('#loginForm').on("submit", function (event) {
        //    $.ajax({
        //        type: "POST",
        //        url: "/Login",
        //        contentType: "application/json; charset=utf-8",
        //        headers: {
        //            RequestVerificationToken:
        //                $('input:hidden[name="__RequestVerificationToken"]').val()
        //        },
        //        data: JSON.stringify(getFormData($(this))),
        //        success: function (data) {
        //            console.log(data);
        //            if (data.authenticated === true) {
        //                if (data.returnurl !== null)
        //                    window.location.href = data.returnurl;
        //                else
        //                    window.location.href = '/';
        //            }
        //            else {
        //                $('#errorMsg').show();
        //            }
        //        },
        //        error: function (xmlHttpRequest, textStatus, errorThrown) {
        //            console.log("Loginform resulted faulty..");
        //        }
        //    });
        //    event.preventDefault();
        //});
    });
    
//function getFormData($form) {
//    var unindexed_array = $form.serializeArray();
//    var indexed_array = {};

//    $.map(unindexed_array, function (n, i) {
//        indexed_array[n['name']] = n['value'];
//    });

//    return indexed_array;
//}