using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace HertzFreeriderNotificationService
{
    public class WebScraping
    {

        public int DownloadNewVersion()
        {
            int numberOfTrips = 0;

            var regexMatchOnFalun = new Regex("target[=][\"]stationInfo[\"][>]Falun[<][/]");
            var webInterface = new HtmlWeb();

            var htmlDocument = webInterface.Load("https://www.hertzfreerider.se/unauth/list_transport_offer.aspx");

            var innerBlockHtml = htmlDocument.DocumentNode.SelectSingleNode("//div[@class = 'block']");



            var matches = regexMatchOnFalun.Matches(innerBlockHtml.InnerHtml);

            numberOfTrips = matches.Count;

            return numberOfTrips;
        }
    }
}
