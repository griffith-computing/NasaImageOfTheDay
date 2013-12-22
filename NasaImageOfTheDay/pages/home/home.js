(function () {
    "use strict";

    WinJS.UI.Pages.define("/pages/home/home.html", {

        ready: function (element, options) {
            var dataList = new WinJS.Binding.List(options);
            this._homeListView = element.querySelector("#itemList").winControl;
            this._pushViewCommand = new Commands.PushViewCommand();
            this.setupEventListeners();
            this._data = WinJS.Binding.as({ items: dataList.dataSource });
            WinJS.Binding.processAll(element, this._data);
        },

        updateLayout: function (element, viewState, lastViewState) {
        },

        unload: function () {
            this.tearDownEventListeners();
        },

        handleHomeListItemInvoked: function (eventInfo) {
            if (!eventInfo || !eventInfo.detail) {
                return;
            }

            var itemIndex = eventInfo.detail.itemIndex;

            if (itemIndex == undefined) {
                return;
            }

            this.obtainSelectedItemAndPushDetailView(itemIndex);
        },

        setupEventListeners: function () {
            this._homeListView.addEventListener("iteminvoked", this.handleHomeListItemInvoked.bind(this));
        },

        tearDownEventListeners: function () {
            this._homeListView.removeEventListener("iteminvoked");
        },

        obtainSelectedItemAndPushDetailView: function (index) {
            var ds = this._homeListView.itemDataSource;
            ds.itemFromIndex(index).done(this.callPushViewCommandWithItem.bind(this));
        },

        callPushViewCommandWithItem: function (item) {
            this._pushViewCommand.setView("/pages/item/item.html").setData(item.data).execute();
        }

    });
})();