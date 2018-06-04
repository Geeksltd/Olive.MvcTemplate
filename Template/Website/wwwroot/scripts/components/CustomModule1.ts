
// JS TO LOAD THIS ----> loadModule('/scripts/components/CustomModule1', m => m.default.run());
// OR, IN AN M# PAGE ----> LoadJavascriptModule("/scripts/components/CustomModule1", "run()");

// Dependencies:
import AppPage from "../AppPage"

export default class CustomModule1 {

    static get page(): AppPage { return window["page"]; }

    public static run(): void {
        console.log("Hello world! I am Custom-Module-1.");

        // Note: You can use << this.page >> to hook into the page lifecycle events,
        // especially in relation to Ajax redirections.
    }
}