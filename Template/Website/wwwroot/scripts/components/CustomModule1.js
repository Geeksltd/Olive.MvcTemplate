// JS TO LOAD THIS ----> loadModule('/scripts/components/CustomModule1', m => m.default.run());
// OR, IN AN M# PAGE ----> LoadJavascriptModule("/scripts/components/CustomModule1", "run()");
define(["require", "exports"], function (require, exports) {
    Object.defineProperty(exports, "__esModule", { value: true });
    class CustomModule1 {
        static get page() { return window["page"]; }
        static run() {
            console.log("Hello world! I am Custom-Module-1.");
            // Note: You can use << this.page >> to hook into the page lifecycle events,
            // especially in relation to Ajax redirections.
        }
    }
    exports.default = CustomModule1;
});
//# sourceMappingURL=CustomModule1.js.map