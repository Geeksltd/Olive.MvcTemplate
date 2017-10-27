var CollapsibleCheckBoxes = (function () {
    function CollapsibleCheckBoxes(control) {
        this.timeOutHandler = 0;
        this.summarizeMultipleItems = true;
        this.control = control;
        this.searchBoxContainer = control.find(".search-container");
        this.searchBox = this.searchBoxContainer.find(".textbox");
        this.captionContainer = control.find(".caption-container");
        this.caption = this.captionContainer.find(".textbox");
        this.optionsContainer = control.find(".options-container");
        this.itemsListContainer = control.find(".items-list .checkbox-list");
        this.selectionContainer = control.find(".selection-container");
        this.chevronIcon = this.captionContainer.find(".fa-chevron-down");
        if (control.attr("data-summarize-multiple") == "false")
            this.summarizeMultipleItems = false;
        this.initialise();
    }
    CollapsibleCheckBoxes.prototype.initialise = function () {
        var _this = this;
        var me = this;
        // select all/none handlers 
        this.optionsContainer.find('.select-all').off('click.collapsible').on('click.collapsible', function () { return _this.triggerChangeAll(true); });
        this.optionsContainer.find('.remove-all').off('click.collapsible').on('click.collapsible', function () { return _this.triggerChangeAll(false); });
        this.searchBox.attr("AUTOCOMPLETE", "off");
        this.optionsContainer.addClass("__LEAVED");
        this.caption.addClass("__BLURED");
        this.searchBox.unbind("blur").blur(function () { return _this.textBlur(); });
        this.searchBox.keydown(function (e) { if (e.keyCode == 13)
            e.preventDefault(); });
        this.searchBox.unbind("focus").focus(function () { return _this.textFocus(); });
        this.chevronIcon.unbind("click").click(function () { _this.textFocus(); _this.searchBox.focus(); });
        this.caption.unbind("focus").focus(function () { _this.textFocus(); _this.searchBox.focus(); });
        this.searchBox.unbind("keyup").keyup(function () { _this.FilterFunctions(); _this.resetPosition(); });
        this.optionsContainer.find("*").unbind("hover").hover(function () { return _this.panelIn(); });
        this.optionsContainer.unbind("hover").hover(function () { return _this.panelIn(); }, function () { return _this.panelOut(); });
        this.refreshDisplay();
        this.FilterFunctions();
    };
    CollapsibleCheckBoxes.prototype.triggerChangeAll = function (isCheck) {
        var checkboxes = this.optionsContainer.find("input[type='checkbox']");
        if (checkboxes.length == 0)
            return;
        checkboxes.each(function (index, checkbox) { return $(checkbox).prop('checked', isCheck); });
        checkboxes.first().trigger("change");
        this.refreshDisplay();
    };
    CollapsibleCheckBoxes.prototype.textBlur = function () {
        this.caption.addClass("__BLURED");
        if (this.optionsContainer.hasClass("__LEAVED"))
            this.HideItems();
    };
    CollapsibleCheckBoxes.prototype.textFocus = function () {
        clearTimeout(this.timeOutHandler);
        this.searchBoxContainer.show();
        this.caption.removeClass("__BLURED");
        this.ShowItems();
        this.captionContainer.hide();
    };
    CollapsibleCheckBoxes.prototype.panelOut = function () {
        this.optionsContainer.addClass("__LEAVED");
        if (this.caption.hasClass("__BLURED"))
            this.HideItems();
    };
    CollapsibleCheckBoxes.prototype.panelIn = function () {
        if (this.timeOutHandler)
            clearTimeout(this.timeOutHandler);
        this.optionsContainer.removeClass("__LEAVED");
    };
    CollapsibleCheckBoxes.prototype.resetPosition = function () {
        var absoluteTop = this.searchBox.screenOffset().top + this.searchBox.outerHeight();
        var absoluteBottom = absoluteTop + this.optionsContainer.outerHeight();
        var upwardShift = Math.max(absoluteBottom - $(window).height(), 0);
        var relativeTop = this.searchBox.outerHeight() - upwardShift + 3; // the container has -3px margin top
        this.optionsContainer.css({ width: this.searchBox.outerWidth(), top: relativeTop, left: this.searchBox.position().left });
    };
    CollapsibleCheckBoxes.prototype.ShowItems = function () {
        this.resetPosition();
        // hide all open panels
        $('[data-control=collapsible-checkboxes] .panel-optionsContainer').hide();
        if ($("input:checkbox", this.optionsContainer).length)
            this.optionsContainer.show();
        this.refreshDisplay();
    };
    CollapsibleCheckBoxes.prototype.HideItems = function () {
        var _this = this;
        setTimeout(function () {
            _this.captionContainer.show();
            _this.searchBoxContainer.hide();
            _this.optionsContainer.hide();
            _this.revertAllCheckBoxFromSelectedItems();
            _this.control.trigger("ccl.change");
        }, 200);
    };
    CollapsibleCheckBoxes.prototype.FilterFunctions = function () {
        var parts = $(this.searchBox).val().split(" ");
        var allCheckBoxes = $("input:checkbox", this.optionsContainer);
        allCheckBoxes.each(function (index, checkbox) {
            var jCheckbox = $(checkbox);
            var value = jCheckbox.next().text().trim();
            var matches = true;
            for (var i = 0; i < parts.length; i++) {
                if (value == null || value == undefined || value.toLowerCase().indexOf(parts[i].toLowerCase()) == -1) {
                    matches = false;
                    break;
                }
            }
            if (matches)
                jCheckbox.parent().show();
            else
                jCheckbox.parent().hide();
        });
    };
    CollapsibleCheckBoxes.prototype.addCheckBoxToSelectedItems = function (checkbox) {
        var control = $(checkbox);
        if (this.itemsListContainer.length > 0) {
            var parent = control.parent();
            var removeIcon = $("<i class='fa fa-remove'></i>");
            parent.attr("checkboxID", control.val()).addClass("item");
            removeIcon.appendTo(parent);
            control.hide();
            parent.appendTo(this.selectionContainer);
        }
    };
    CollapsibleCheckBoxes.prototype.revertAllCheckBoxFromSelectedItems = function () {
        var _this = this;
        var allCheckBoxes = $(".selected-items div[checkboxid] input[type='checkbox']", this.optionsContainer);
        allCheckBoxes.each(function (index, checkbox) {
            _this.revertCheckBoxFromSelectedItems(checkbox);
        });
    };
    CollapsibleCheckBoxes.prototype.revertCheckBoxFromSelectedItems = function (checkbox) {
        var control = $(checkbox);
        if (control.parent().is("div[checkboxid]")) {
            var parent = control.parent();
            parent.children("i").remove();
            parent.removeClass("item").removeAttrs("checkboxid");
            control.show();
            parent.appendTo(this.itemsListContainer);
        }
    };
    CollapsibleCheckBoxes.prototype.refreshDisplay = function () {
        var _this = this;
        var allCheckBoxes = this.optionsContainer.find("input[type='checkbox']");
        var allSelected = [];
        var pluralName = this.control.attr('data-plural-name');
        if (pluralName == undefined)
            pluralName = 'items';
        allCheckBoxes.each(function (i, cb) {
            var checkbox = $(cb);
            if (checkbox.is(":checked")) {
                allSelected.push($(checkbox).next().text().trim());
                _this.addCheckBoxToSelectedItems(cb);
            }
            else
                _this.revertCheckBoxFromSelectedItems(cb);
        });
        this.handleCheckboxChangeEvent();
        var caption = allSelected.join(", ") || "";
        this.control.attr("title", caption);
        if (allSelected.length > 1 && this.summarizeMultipleItems)
            caption = allSelected.length + ' ' + pluralName;
        if (caption.length == 0)
            caption = this.control.attr("placeholder");
        this.caption.val(caption);
        if (this.selectionContainer.children().length == 0)
            this.selectionContainer.hide();
        else
            this.selectionContainer.show();
    };
    CollapsibleCheckBoxes.prototype.handleCheckboxChangeEvent = function () {
        var _this = this;
        var checkbox = this.optionsContainer.find("input[type='checkbox']");
        checkbox.unbind("change").change(function () {
            _this.refreshDisplay();
            _this.searchBox.focus();
            _this.caption.attr("somethingIsChanged", "true");
        });
    };
    return CollapsibleCheckBoxes;
}());
