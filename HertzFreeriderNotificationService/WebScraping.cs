using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace HertzFreeriderNotificationService
{
    public class WebScraping
    {
        public bool DownloadNewVersion()
        {
            bool returnbool = false;
            var webInterface = new HtmlWeb();

            var regexMatchOnFalun = new Regex("target[=][\"]stationInfo[\"][>]Falun[<][/]");

            return returnbool;
        }
    }
}
