(function () {
    "use strict";

    WinJS.UI.Pages.define("/pages/item/item.html", {

        ready: function (element, options) {
            //WinJS.Navigation.back(1);
            this._popViewCommand = new Commands.PopViewCommand();
            this._data = WinJS.Binding.as({ imageUrl: options.imageUrl, title: options.title, description: options.description });
            WinJS.Binding.processAll(element, this._data);
        },

        updateLayout: function (element, viewState, lastViewState) {
        },

        unload: function () {
        }
    });
})();