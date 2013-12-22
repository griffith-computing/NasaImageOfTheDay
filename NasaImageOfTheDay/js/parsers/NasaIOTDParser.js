(function () {
    "use strict"

    WinJS.Namespace.define("Parsers", {
        NasaIOTDParser: WinJS.Class.define(
            function NasaIOTDParser() {
                // constructor
            }, {
                // instanceMethods
                parse: function (result) {
                    var xml = $(result.response);
                    var channel = xml.find("channel");
                    var parsedResults = xml.find("item").map(function (index, element) {
                        var item = $(element);
                        var enclosure = item.find("enclosure");

                        var parsedResult = {};
                        parsedResult.title = item.find("title").text();
                        // TODO: fix me!!
                        parsedResult.link = item.find("link").text();
                        parsedResult.description = item.find("description").text();
                        parsedResult.pubDate = item.find("pubDate").text();
                        parsedResult.imageUrl = enclosure.attr("url");

                        return parsedResult;
                    });
                    return parsedResults
                }
            }, {
                // staticMethods
            }
        )
    });
})();