var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
define(["require", "exports", "olive-page", "olive-config"], function (require, exports, olive_page_1, olive_config_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    var AppPage = /** @class */ (function (_super) {
        __extends(AppPage, _super);
        // Here you can override any of the base standard functions.
        // e.g: To use a different AutoComplete library, simply override handleAutoComplete(input).
        function AppPage() {
            var _this = _super.call(this) || this;
            olive_config_1.default.DISABLE_BUTTONS_DURING_AJAX = true;
            return _this;
        }
        AppPage.prototype.initialize = function () {
            _super.prototype.initialize.call(this);
            // This function is called upon every Ajax update as well as the initial page load.
            // Any custom initiation goes here.
        };
        AppPage.prototype.executeAction = function (action, trigger) {
            // You can define any custom actions here.
            // --------------- EXAMPLE --------------------------------
            if (action.MySpecialAction) {
                // do something to handle it.
                return true;
            }
            // You can also change the default framework behaviour for standard actions.
            // --------------- EXAMPLE --------------------------------
            //if (action.BrowserAction == "ShowPleaseWait") {
            //    // Handle it my way...
            //}
            return _super.prototype.executeAction.call(this, action, trigger);
        };
        return AppPage;
    }(olive_page_1.default));
    exports.default = AppPage;
    window["page"] = new AppPage();
});
//# sourceMappingURL=AppPage.js.map
