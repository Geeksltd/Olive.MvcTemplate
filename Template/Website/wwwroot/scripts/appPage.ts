
import OlivePage from 'olive/olivePage';
import Config from 'olive/config';

export default class AppPage extends OlivePage {

    constructor() {

        super();
        // Any code you write here will run only once when the page is loaded.
    }

    initialize() {
        super.initialize();
        // This function is called upon every Ajax update as well as the initial page load.
        // Any custom initiation goes here.
    }
}

window["page"] = new AppPage();