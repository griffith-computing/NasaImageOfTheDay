using System.Xml.Linq;
using NasaImageOfTheDay.Models;

namespace NasaImageOfTheDay.Services;

public class NasaImageService
{
    private const string FeedUrl = "https://www.nasa.gov/rss/dyn/image_of_the_day.rss";

    private readonly HttpClient _httpClient;

    public NasaImageService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "NasaImageOfTheDayApp/1.0");
    }

    public async Task<IEnumerable<NasaImageItem>> GetImagesAsync()
    {
        var response = await _httpClient.GetStringAsync(FeedUrl);

        return ParseRssFeed(response);
    }

    private static IEnumerable<NasaImageItem> ParseRssFeed(string xml)
    {
        var document = XDocument.Parse(xml);
        XNamespace media = "http://search.yahoo.com/mrss/";

        var items = document
            .Descendants("item")
            .Select(item =>
            {
                var enclosure = item.Element("enclosure");
                var imageUrl = enclosure?.Attribute("url")?.Value
                               ?? item.Element(media + "content")?.Attribute("url")?.Value
                               ?? string.Empty;

                return new NasaImageItem
                {
                    Title = item.Element("title")?.Value ?? string.Empty,
                    Description = StripHtml(item.Element("description")?.Value ?? string.Empty),
                    Link = item.Element("link")?.Value ?? string.Empty,
                    PubDate = item.Element("pubDate")?.Value ?? string.Empty,
                    ImageUrl = imageUrl
                };
            })
            .Where(i => !string.IsNullOrEmpty(i.ImageUrl));

        return items;
    }

    private static string StripHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
            return html;

        // Remove HTML tags
        return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", string.Empty).Trim();
    }
}
