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
        "typeahead": "jquery-typeahead/dist/jquery.typeahead.min",
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

        // Olive
        "olive-ext-jquery": "olive.mvc/dist/extensions/jquery",
        "olive-page": "olive.mvc/dist/olivepage",
        "olive-config": "olive.mvc/dist/config",
        "app-page": "../scripts/AppPage",
    },
    map: {
        "*": {
            "jquery-ui/ui/widget": "jquery-ui",
            "popper.js": "popper",
            '../moment': 'moment',
            'olive': "olive.mvc/dist"
        }
    },
    shim: {
        "bootstrap": ["jquery", "popper"],
        "jquery-validate": ['jquery'],
        "validation-style": ['jquery', "jquery-validate", "bootstrap"],
        "combodate": ['jquery'],

        "typeahead": ['jquery'],
        "file-upload": ['jquery', 'jquery-ui'],
        "file-style": ["file-upload"],
        "chosen": ['jquery'],
        "smartmenus": ['jquery'],
        "jquery-validate-unobtrusive": ['jquery-validate'],
        'backbone.layoutmanager': ['backbone'],
        "spinedit": ['jquery'],
        "password-strength": ['jquery'],
        "moment-locale": ['moment'],
        "olive-ext-jquery": {
            deps: ['jquery', "jquery-validate-unobtrusive"],
            exports: '_'
        },
        "olive-page": ["alertify", "olive-ext-jquery", "combodate"]
    }
});

var mainScripts = ["app-page", "olive-page",
    // JQuery:
    "jquery", "jquery-ui", "jquery-validate", "jquery-validate-unobtrusive", "olive-ext-jquery",

    // JQuery plugins:
    "chosen", "alertify", "smartmenus", "file-upload", "typeahead",

    // Bootstrap and plugins:
    "bootstrap", "moment", "moment-locale", "datepicker",
    "spinedit", "password-strength", "slider", "file-style",

    // "validation-style" ------> Error
];

requirejs(["popper.js"], function (p) {
    requirejs(mainScripts,
        function (AppPage) {
            window.page = new AppPage.default();
            loadAppScripts();
        });
});

function loadAppScripts() {
    // TODO:
    // For each custom app-specific script requirement:
    //    > Add a TS file inside ../scripts/somponents
    //    > Add a line similar to below to invoke it.
    //        requirejs(["../Scripts/Components/CustomModule1"], type => type.default.Run());
}