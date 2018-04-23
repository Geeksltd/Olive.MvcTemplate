requirejs.config({
    baseUrl: '/lib',
    paths: {
        // JQuery:
        "jquery": "jquery/dist/jquery",
        "jquery-ui": "jqueryui/jquery-ui",
        "jquery-validate": "jquery-validation/dist/jquery.validate",
        "jquery-validate-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive",

        // Jquery plugins:
        "chosen": "chosen/chosen.jquery",
        "alertify": "alertifyjs/dist/js/alertify",
        "smartmenus": "smartmenus/src/jquery.smartmenus",
        "file-upload": "jquery-file-upload/js/jquery.fileupload",
        "jquery-typeahead": "jquery-typeahead/dist/jquery.typeahead.min",
        "combodate": "combodate/src/combodate",

        // Bootstrap
        "popper": "popper.js/dist/umd/popper",
        "bootstrap": "bootstrap/dist/js/bootstrap",
        "validation-style": "jquery-validation-bootstrap-tooltip/jquery-validate.bootstrap-tooltip",
        "file-style": "bootstrap-filestyle/src/bootstrap-filestyle",
        "spinedit": "bootstrap-spinedit/js/bootstrap-spinedit",
        "password-strength": "pwstrength-bootstrap/dist/pwstrength-bootstrap-1.2.7",
        "slider": "seiyria-bootstrap-slider/dist/bootstrap-slider.min",
        "moment": "moment/min/moment.min",
        "moment-locale": "moment/locale/en-gb",
        "datepicker": "eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker"
    },
    map: {
        "*": {
            "jquery-ui/ui/widget": "jquery-ui",
            "popper.js": "popper",
            '../moment': 'moment',
            'olive': "olive.mvc/dist",
            "app": "../scripts"
        }
    },
    shim: {
        "bootstrap": ["jquery", "popper"],
        "jquery-validate": ['jquery'],
        "validation-style": ['jquery', "jquery-validate", "bootstrap"],
        "combodate": ['jquery'],
        "jquery-typeahead": ['jquery'],
        "file-upload": ['jquery', 'jquery-ui'],
        "file-style": ["file-upload"],
        "chosen": ['jquery'],
        "smartmenus": ['jquery'],
        "jquery-validate-unobtrusive": ['jquery-validate'],
        'backbone.layoutmanager': ['backbone'],
        "spinedit": ['jquery'],
        "password-strength": ['jquery'],
        "moment-locale": ['moment'],
        "olive/extensions/jQueryExtensions": {
            deps: ['jquery', "jquery-validate-unobtrusive"],
            exports: '_'
        },
        "olive/olivePage": ["alertify", "olive/extensions/jQueryExtensions", "combodate"]
    }
});

requirejs(["app/appPage", "olive/olivePage",
    // JQuery:
    "jquery", "jquery-ui", "jquery-validate", "jquery-validate-unobtrusive", "olive/extensions/jQueryExtensions",
    // JQuery plugins:
    "chosen", "alertify", "smartmenus", "file-upload", "jquery-typeahead",
    // Bootstrap and plugins:
    "popper", "bootstrap", "moment", "moment-locale", "datepicker",
    "spinedit", "password-strength", "slider", "file-style", "validation-style"
]);

window.loadModule = function (path, onLoaded) {
    if (path.indexOf("/") === 0) path = "./.." + path; // To fix baseUrl
    requirejs([path], m => { if (onLoaded) onLoaded(m) });
};

// Wait until Olive scripts are fully loaded before submitting any form
for (let i = 0; i < document.forms.length; i++) {
    document.forms[i].onsubmit = function (e) {
        if (window["IsOliveMvcLoaded"] === undefined) return false;
    };
}