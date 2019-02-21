requirejs.config({
    baseUrl: '/lib',
    urlArgs: "v1.1", // Increment with every release to refresh browser cache.
    paths: {
        // JQuery:
        "jquery": "jquery/dist/jquery",
        "jquery-ui/ui/widget": "jquery-ui/ui/widget",
        "jquery-ui/ui/focusable": "jquery-ui/ui/focusable",
        "jquery-validate": "jquery-validation/dist/jquery.validate",
        "jquery-validate-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive",

        // Jquery plugins:
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
        "datepicker": "eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker",
        "bootstrap-select": "bootstrap-select/dist/js/bootstrap-select"
    },
    map: {
        "*": {
            "popper.js": "popper",
            '../moment': 'moment',
            'olive': "olive.mvc/dist",
            "app": "../scripts",
            "jquery-sortable": "jquery-ui/ui/widgets/sortable"
        }
    },
    shim: {
        "bootstrap": ["jquery", "popper"],
        "bootstrap-select": ['jquery', 'bootstrap'],
        "jquery-validate": ['jquery'],
        "validation-style": ['jquery', "jquery-validate", "bootstrap"],
        "combodate": ['jquery'],
        "jquery-typeahead": ['jquery'],
        "file-upload": ['jquery', 'jquery-ui/ui/widget'],
        "file-style": ["file-upload"],
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
    "jquery", "jquery-ui/ui/widget", "jquery-ui/ui/focusable", "jquery-validate", "jquery-validate-unobtrusive", "olive/extensions/jQueryExtensions",
    // JQuery plugins:
    "alertify", "smartmenus", "file-upload", "jquery-typeahead",
    // Bootstrap and plugins:
    "popper", "bootstrap", "moment", "moment-locale", "datepicker",
    "spinedit", "password-strength", "slider", "file-style", "validation-style", "bootstrap-select"
]);

window.loadModule = function (path, onLoaded) {
    if (path.indexOf("/") === 0) path = "./.." + path; // To fix baseUrl
    requirejs([path], function (m) { if (onLoaded) onLoaded(m) });
};

// Wait until Olive scripts are fully loaded before submitting any form
for (let i = 0; i < document.forms.length; i++) {
    document.forms[i].onsubmit = function (e) {
        if (window["IsOliveMvcLoaded"] === undefined) return false;
    };
}
