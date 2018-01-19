
import OlivePage from 'olive-page';
import Config from 'olive-config';

export default class AppPage extends OlivePage {
    // Here you can override any of the base standard functions.
    // e.g: To use a different AutoComplete library, simply override handleAutoComplete(input).

    constructor() {
        super();
        Config.DISABLE_BUTTONS_DURING_AJAX = true;
    }

    initialize() {
        super.initialize();

        // This function is called upon every Ajax update as well as the initial page load.
        // Any custom initiation goes here.
    }

    executeAction(action: any, trigger: any): boolean {

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

        return super.executeAction(action, trigger);
    }
}

window["page"] = new AppPage();