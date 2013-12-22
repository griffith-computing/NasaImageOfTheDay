(function () {
    "use strict"

    WinJS.Namespace.define("Commands", {
        PushViewCommand: WinJS.Class.define(
            function PushViewCommand() {
                // constructor
                this._data = null;
                this._view = null;
            }, {
                setView: function (value) {
                    this._view = value;
                    return this;
                },
                // instance Method
                setData: function (value) {
                    this._data = value;
                    return this;
                },
                execute: function () {
                    if (this._data == null || this._view == null)
                        return;

                    WinJS.Navigation.navigate(this._view, this._data);
                }
            }, {
                // static Method
            }
        )
    });
})();