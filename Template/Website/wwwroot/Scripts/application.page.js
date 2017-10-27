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
var ApplicationPage = (function (_super) {
    __extends(ApplicationPage, _super);
    // Here you can override any of the base standard functions.
    // e.g: To use a different AutoComplete library, simply override handleAutoComplete(input).
    function ApplicationPage() {
        var _this = _super.call(this) || this;
        _this.DISABLE_BUTTONS_DURING_AJAX = true;
        return _this;
    }
    ApplicationPage.prototype.initialize = function () {
        _super.prototype.initialize.call(this);
        // This function is called upon every Ajax update as well as the initial page load.
        // Any custom initiation goes here.
    };
    ApplicationPage.prototype.executeAction = function (action, trigger) {
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
    return ApplicationPage;
}(BaseApplicationPage));
page = new ApplicationPage();
