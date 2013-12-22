(function () {
    "use strict"

    WinJS.Namespace.define("Services", {
        NasaIOTDService: WinJS.Class.define(
            function NasaIOTDService() {
                // constructor
                this.parser = new Parsers.NasaIOTDParser();
                this.request = { url: "http://www.nasa.gov/rss/dyn/image_of_the_day.rss" };
            }, {
                // instanceMembers
                getNasaImageOfTheDay: function () {
                    var promise = WinJS.xhr(this.request);
                    return promise.then(this.parser.parse);
                }
            }, {
                // staticMembers
            }
    )
    });
})();