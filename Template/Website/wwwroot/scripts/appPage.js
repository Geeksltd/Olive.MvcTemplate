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
define(["require", "exports", "olive/olivePage"], function (require, exports, olivePage_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    var AppPage = /** @class */ (function (_super) {
        __extends(AppPage, _super);
        function AppPage() {
            return _super.call(this) || this;
            // Any code you write here will run only once when the page is loaded.
        }
        AppPage.prototype.initialize = function () {
            _super.prototype.initialize.call(this);
            // This function is called upon every Ajax update as well as the initial page load.
            // Any custom initiation goes here.
        };
        return AppPage;
    }(olivePage_1.default));
    exports.default = AppPage;
    window["page"] = new AppPage();
});
//# sourceMappingURL=appPage.js.map