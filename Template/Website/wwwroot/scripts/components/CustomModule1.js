// Note: This module should be loaded via requirejs.
// On the page that this is used (or the layout cshtml file) use:
//    loadModule('/scripts/CustomModule1', m => m.default.Run());
define(["require", "exports"], function (require, exports) {
    Object.defineProperty(exports, "__esModule", { value: true });
    var CustomModule1 = /** @class */ (function () {
        function CustomModule1() {
        }
        CustomModule1.Run = function () {
            // TODO: ... 
        };
        return CustomModule1;
    }());
    exports.default = CustomModule1;
});
//# sourceMappingURL=CustomModule1.js.map