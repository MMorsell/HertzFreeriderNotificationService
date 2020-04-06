using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HertzFreeriderWorkerService
{
    public class Worker : BackgroundService
    {
        private static readonly string url = "https://www.hertzfreerider.se/unauth/list_transport_offer.aspx";
        private readonly ILogger<Worker> _logger;
        private EmailNotification _notification;
        public int NumberOfTrips { get; set; }
        public int TripsFound { get; set; }

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _notification = new EmailNotification();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service has started!");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await GetNewUpdate();
                
                if (TripsFound > NumberOfTrips)
                {
                    int difference = TripsFound - NumberOfTrips;
                    NumberOfTrips = TripsFound;

                    if (difference < 0)
                    {
                        difference *= -1;
                    }
                    if (difference > 1)
                    {
                        _logger.LogInformation("Sending notification of over 1 trip");
                        await _notification.SendMessage("New trips on Freerider!", $"{difference} new trips at {url}", _logger);
                    }
                    else
                    {
                        _logger.LogInformation("Sending notification of 1 trip");
                        await _notification.SendMessage("New trips on Freerider!", $"{difference} new trip at {url}", _logger);
                    }
                }
                else if (NumberOfTrips != TripsFound)
                {
                    NumberOfTrips = TripsFound;
                }

                await Task.Delay(90000, stoppingToken);
            }
        }
        private async Task GetNewUpdate()
        {
            var regexMatchOnFalun = new Regex("target[=][\"]stationInfo[\"][>]Falun[<][/]");
            var webInterface = new HtmlWeb();

            var htmlDocument = await webInterface.LoadFromWebAsync(url);

            var innerBlockHtmlNodes = htmlDocument.DocumentNode.SelectNodes("//tr[@class = 'highlight']");


            int totalNumberOfMatches = 0;

            foreach (var node in innerBlockHtmlNodes)
            {
                if (regexMatchOnFalun.IsMatch(node.InnerHtml))
                {
                    totalNumberOfMatches++;
                }
            }

            TripsFound = totalNumberOfMatches;
        }
    }
}
