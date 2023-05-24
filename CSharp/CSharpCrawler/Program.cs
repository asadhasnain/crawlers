using System;
using System.Net;
using HtmlAgilityPack;

public class Program
{
    public static void Main()
    {
        // URL of the initial page to crawl
        string url = "https://participant.facilitymanagerplus.com/AvailableStudies.aspx";
        CrawlWebsite(url);
    }

    public static void CrawlWebsite(string url)
    {
        // Create a WebClient to send a GET request to the URL
        WebClient client = new WebClient();
        string htmlContent = client.DownloadString(url);

        // Create an HtmlDocument object to parse the HTML
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        // Find all the study cards on the page
        HtmlNodeCollection studyCards = doc.DocumentNode.SelectNodes("//div[contains(@class, 'project-detail')]");

        // Iterate over each study card and extract the desired information
        foreach (HtmlNode card in studyCards)
        {
            // Extract the study title
            string title = card.SelectSingleNode(".//h3").InnerText.Trim();
            string description = card.SelectSingleNode(".//p").InnerText.Trim();
            HtmlNodeCollection dollar = card.SelectNodes(".//i[contains(@class, 'fa fa-dollar')]");
            Console.WriteLine("Study Title: " + title);
            Console.WriteLine("Description: " + description);
            if(dollar != null)
                Console.WriteLine("Dollars: " + dollar.Count);
            else
                Console.WriteLine("Dollars: " + 0);

            HtmlNodeCollection projectSpecs = card.SelectNodes(".//li");

            foreach (HtmlNode spec in projectSpecs)
            {
                if (spec.InnerText.Trim() != "Photo ID Required")
                {
                    Console.WriteLine(spec.InnerText.Trim());
                }
            }

            string linkTitle = card.SelectSingleNode(".//a").InnerText.Trim();
            string baseUrl = "https://participant.facilitymanagerplus.com/";
            Console.WriteLine("linkTitle: " + baseUrl + card.SelectSingleNode(".//a").GetAttributeValue("href", ""));
            Console.WriteLine();
        }
    }
}
