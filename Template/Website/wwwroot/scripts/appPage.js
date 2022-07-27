var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
define(["require", "exports", "olive/olivePage", "olive/plugins/confirmBox"], function (require, exports, olivePage_1, confirmBox_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    var AppPage = /** @class */ (function (_super) {
        __extends(AppPage, _super);
        function AppPage() {
            var _this = _super.call(this) || this;
            _this.toggleArchive = function (element) {
                var $element = $(element);
                var toggleArchiveRequest = function () {
                    $.post("/toggle-archive-item", { obj: $element.data("id") }, function (result) {
                        if (result.hasOwnProperty("Error") && result.Error)
                            alert(result.Error);
                        else if (result.ArchiveStatus == true) {
                            $element.closest(".r-grid-row").addClass("item-archived");
                            $element.addClass("item-archived");
                        }
                        else {
                            $element.closest(".r-grid-row").removeClass("item-archived");
                            $element.removeClass("item-archived");
                        }
                    });
                };
                var confirmMessage = $element.data("confirm-message");
                var isArchived = $element.hasClass("item-archived");
                if (confirmMessage)
                    new confirmBox_1.ConfirmBox(null)
                        .showConfirm(confirmMessage.replace("[#NEXT_STATUS_ACTION#]", isArchived ? "unarchive" : "archive"), toggleArchiveRequest);
                else
                    toggleArchiveRequest();
            };
            _this.toggleSidebarMenu = function () {
                //if (this.isThirdLevelMenuVisible) {
                //    $(".side-bar .side-sub-menu").remove();
                //    $(".side-bar .slide-down-menu").show();
                //    this.isThirdLevelMenuVisible = false;
                //    $(".menu-toggler").addClass("burger-icon").removeClass("back-icon");
                //    return;
                //}
                //TODO dont use toggle as Some browser can't support toggleClass() method
                $(".menu-toggler").toggleClass("collapsed");
                $(".left-panel").toggleClass("show");
                $(".right-panel").toggleClass("sidebar-collapsed");
            };
            return _this;
            // Any code you write here will run only once when the page is loaded.
        }
        AppPage.prototype.initialize = function () {
            var _this = this;
            _super.prototype.initialize.call(this);
            // This function is called upon every Ajax update as well as the initial page load.
            // Any custom initiation goes here.
            //Override the 'enableCustomCheckbox' and 'enableCustomRadio' with empty methods to use the original controls.
            $(".menu-toggler").off("click.menutoggler").on("click.menutoggler", function (event) { event.preventDefault(); _this.toggleSidebarMenu(); });
        };
        return AppPage;
    }(olivePage_1.default));
    exports.default = AppPage;
    window["page"] = new AppPage();
});
//# sourceMappingURL=appPage.js.map