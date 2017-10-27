class CollapsibleCheckBoxes {
    control: JQuery;
    searchBox: JQuery;
    searchBoxContainer: JQuery;
    caption: JQuery;
    captionContainer: JQuery;
    optionsContainer: JQuery;
    itemsListContainer: JQuery;
    selectionContainer: JQuery;
    chevronIcon: JQuery;
    timeOutHandler = 0;
    summarizeMultipleItems = true;

    constructor(control) {
        this.control = control;

        this.searchBoxContainer = control.find(".search-container");
        this.searchBox = this.searchBoxContainer.find(".textbox");

        this.captionContainer = control.find(".caption-container");
        this.caption = this.captionContainer.find(".textbox");

        this.optionsContainer = control.find(".options-container");
        this.itemsListContainer = control.find(".items-list .checkbox-list");
        this.selectionContainer = control.find(".selection-container");
        this.chevronIcon = this.captionContainer.find(".fa-chevron-down");

        if (control.attr("data-summarize-multiple") == "false") this.summarizeMultipleItems = false;

        this.initialise();
    }

    initialise() {

        var me = this;

        // select all/none handlers 
        this.optionsContainer.find('.select-all').off('click.collapsible').on('click.collapsible', () => this.triggerChangeAll(true));
        this.optionsContainer.find('.remove-all').off('click.collapsible').on('click.collapsible', () => this.triggerChangeAll(false));

        this.searchBox.attr("AUTOCOMPLETE", "off");
        this.optionsContainer.addClass("__LEAVED");
        this.caption.addClass("__BLURED");

        this.searchBox.unbind("blur").blur(() => this.textBlur());
        this.searchBox.keydown((e) => { if (e.keyCode == 13) e.preventDefault(); });
        this.searchBox.unbind("focus").focus(() => this.textFocus());
        this.chevronIcon.unbind("click").click(() => { this.textFocus(); this.searchBox.focus(); });

        this.caption.unbind("focus").focus(() => { this.textFocus(); this.searchBox.focus(); });
        this.searchBox.unbind("keyup").keyup(() => { this.FilterFunctions(); this.resetPosition(); });

        this.optionsContainer.find("*").unbind("hover").hover(() => this.panelIn());

        this.optionsContainer.unbind("hover").hover(() => this.panelIn(), () => this.panelOut());

        this.refreshDisplay();
        this.FilterFunctions();
    }

    triggerChangeAll(isCheck: boolean) {
        var checkboxes = this.optionsContainer.find("input[type='checkbox']");

        if (checkboxes.length == 0) return;

        checkboxes.each((index, checkbox) => $(checkbox).prop('checked', isCheck));

        checkboxes.first().trigger("change");
        this.refreshDisplay();
    }

    textBlur() {
        this.caption.addClass("__BLURED");
        if (this.optionsContainer.hasClass("__LEAVED")) this.HideItems();
    }

    textFocus() {
        clearTimeout(this.timeOutHandler);

        this.searchBoxContainer.show();
        this.caption.removeClass("__BLURED");

        this.ShowItems();
        this.captionContainer.hide();
    }

    panelOut() {
        this.optionsContainer.addClass("__LEAVED");

        if (this.caption.hasClass("__BLURED")) this.HideItems();
    }

    panelIn() {
        if (this.timeOutHandler) clearTimeout(this.timeOutHandler);
        this.optionsContainer.removeClass("__LEAVED");
    }

    resetPosition() {
        var absoluteTop = this.searchBox.screenOffset().top + this.searchBox.outerHeight();
        var absoluteBottom = absoluteTop + this.optionsContainer.outerHeight();
        var upwardShift = Math.max(absoluteBottom - $(window).height(), 0);
        var relativeTop = this.searchBox.outerHeight() - upwardShift + 3; // the container has -3px margin top

        this.optionsContainer.css({ width: this.searchBox.outerWidth(), top: relativeTop, left: this.searchBox.position().left });
    }

    ShowItems() {
        this.resetPosition();

        // hide all open panels
        $('[data-control=collapsible-checkboxes] .panel-optionsContainer').hide();

        if ($("input:checkbox", this.optionsContainer).length) this.optionsContainer.show();

        this.refreshDisplay();
    }

    HideItems() {
        setTimeout(() => {
            this.captionContainer.show();
            this.searchBoxContainer.hide();
            this.optionsContainer.hide();
            this.revertAllCheckBoxFromSelectedItems();
            this.control.trigger("ccl.change");

        }, 200);
    }

    FilterFunctions() {
        var parts = $(this.searchBox).val().split(" ");
        var allCheckBoxes = $("input:checkbox", this.optionsContainer);

        allCheckBoxes.each((index, checkbox) => {
            var jCheckbox = $(checkbox);
            var value = jCheckbox.next().text().trim();
            var matches = true;
            for (var i = 0; i < parts.length; i++) {
                if (value == null || value == undefined || value.toLowerCase().indexOf(parts[i].toLowerCase()) == -1) {
                    matches = false; break;
                }
            }
            if (matches) jCheckbox.parent().show();
            else jCheckbox.parent().hide();
        });
    }

    addCheckBoxToSelectedItems(checkbox) {
        var control = $(checkbox);
        if (this.itemsListContainer.length > 0) {
            var parent = control.parent();
            var removeIcon = $("<i class='fa fa-remove'></i>");
            parent.attr("checkboxID", control.val()).addClass("item");
            removeIcon.appendTo(parent);
            control.hide();
            parent.appendTo(this.selectionContainer);
        }
    }

    revertAllCheckBoxFromSelectedItems() {
        var allCheckBoxes = $(".selected-items div[checkboxid] input[type='checkbox']", this.optionsContainer);
        allCheckBoxes.each((index, checkbox) => {
            this.revertCheckBoxFromSelectedItems(checkbox);
        });
    }

    revertCheckBoxFromSelectedItems(checkbox) {
        var control = $(checkbox);

        if (control.parent().is("div[checkboxid]")) {
            var parent = control.parent();
            parent.children("i").remove();
            parent.removeClass("item").removeAttrs("checkboxid");
            control.show();
            parent.appendTo(this.itemsListContainer);
        }
    }

    refreshDisplay() {
        var allCheckBoxes = this.optionsContainer.find("input[type='checkbox']");
        var allSelected = [];

        var pluralName = this.control.attr('data-plural-name');
        if (pluralName == undefined) pluralName = 'items';

        allCheckBoxes.each((i, cb) => {
            var checkbox = $(cb);
            if (checkbox.is(":checked")) {
                allSelected.push($(checkbox).next().text().trim());
                this.addCheckBoxToSelectedItems(cb);
            }
            else this.revertCheckBoxFromSelectedItems(cb);
        });

        this.handleCheckboxChangeEvent();

        var caption = allSelected.join(", ") || "";
        this.control.attr("title", caption);

        if (allSelected.length > 1 && this.summarizeMultipleItems) caption = allSelected.length + ' ' + pluralName;

        if (caption.length == 0) caption = this.control.attr("placeholder");

        this.caption.val(caption);

        if (this.selectionContainer.children().length == 0) this.selectionContainer.hide();
        else this.selectionContainer.show();
    }

    handleCheckboxChangeEvent() {

        var checkbox = this.optionsContainer.find("input[type='checkbox']");

        checkbox.unbind("change").change(() => {
            this.refreshDisplay();
            this.searchBox.focus();
            this.caption.attr("somethingIsChanged", "true");
        });
    }
}