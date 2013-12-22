(function () {
    "use strict"

    WinJS.Namespace.define("Commands", {
        PopViewCommand: WinJS.Class.define(
            function PopViewCommand() {
                // constructor
            }, {
                // instance Method
                execute: function () {
                    WinJS.Navigation.back(1);
                }
            }, {
                // static Method
            }
        )
    });
})();