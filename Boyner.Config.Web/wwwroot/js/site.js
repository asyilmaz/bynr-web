// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var BoynerJS = {
    ConfirmDialog: function (title, message, callbackOK, callbackCancel) {
        var dialog = bootbox.dialog({
            message: message,
            title: title,
            onEscape: true,
            buttons:
            {
                "Cancel":
                {
                    "label": "Cancel",
                    "className": "btn-sm",
                    "callback": function () { $(".modal-dialog :button").hide(); if (callbackCancel) callbackCancel($(dialog)); }
                },
                "OK":
                {
                    "label": "Delete",
                    "className": "btn-sm btn-primary",
                    "callback": function () { $(".modal-dialog :button").hide(); if (callbackOK) callbackOK($(dialog)); }
                }
            }
        });
    }
};