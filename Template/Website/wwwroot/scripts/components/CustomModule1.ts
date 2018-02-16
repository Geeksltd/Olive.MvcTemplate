
// JS TO LOAD THIS ----> loadModule('/scripts/components/CustomModule1', m => m.default.Run());
// OR, IN AN M# PAGE ----> LoadJavascriptModule("/scripts/components/CustomModule1", "Run()");

// Dependencies:
import AppPage from "../AppPage"

export default class CustomModule1 {

    static get page(): AppPage { return window["page"]; }

    public static Run(): void {
        console.log("Hello world! I am Custom-Module-1.");

        // Note: You can use << this.page >> to hook into the page lifecycle events,
        // especially in relation to Ajax redirections.
    }
}