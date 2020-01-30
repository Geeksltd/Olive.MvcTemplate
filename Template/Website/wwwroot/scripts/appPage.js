define(["require", "exports", "olive/olivePage", "olive/plugins/confirmBox"], function (require, exports, olivePage_1, confirmBox_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    class AppPage extends olivePage_1.default {
        constructor() {
            super();
            this.toggleArchive = (element) => {
                let $element = $(element);
                var toggleArchiveRequest = () => {
                    $.post("/toggle-archive-item", { obj: $element.data("id") }, (result) => {
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
            this.toggleSidebarMenu = () => {
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
            // Any code you write here will run only once when the page is loaded.
        }
        initialize() {
            super.initialize();
            // This function is called upon every Ajax update as well as the initial page load.
            // Any custom initiation goes here.
            //Override the 'enableCustomCheckbox' and 'enableCustomRadio' with empty methods to use the original controls.
            $(".menu-toggler").off("click.menutoggler").on("click.menutoggler", (event) => { event.preventDefault(); this.toggleSidebarMenu(); });
        }
    }
    exports.default = AppPage;
    window["page"] = new AppPage();
});
//# sourceMappingURL=appPage.js.map