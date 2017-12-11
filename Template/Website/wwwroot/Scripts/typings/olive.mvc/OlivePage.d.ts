declare class OlivePage {
    DISABLE_BUTTONS_DURING_AJAX: boolean;

    initialize();
    executeAction(action: any, trigger: any): boolean;
}