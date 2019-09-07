using System;
using System.Threading;
using System.Threading.Tasks;

namespace HertzFreeriderNotificationService
{
    class Program
    {
        public static int NumberOfTripsOnline { get; set; } = 0;
        static void Main(string[] args)
        {
            var _sms = new SmsNotification();
            //_sms.SendMessage("Testing Mannnennn");
            var webScraping = new WebScraping();
            int number = 0;
            while (true)
            {
                Console.WriteLine(number);
                var numberOfTrips = webScraping.DownloadNewVersion();

                if (numberOfTrips > NumberOfTripsOnline)
                {
                    int difference = NumberOfTripsOnline - numberOfTrips;
                    NumberOfTripsOnline = numberOfTrips;

                    if (difference < 0)
                    {
                        difference *= -1;
                    }
                    if (difference > 1)
                    {
                        _sms.SendMessage($"New Free Trips on Freerider! \nCurrently: {difference} new trips");
                    }
                    else
                    {
                        _sms.SendMessage($"New Free Trips on Freerider! \nCurrently: {difference} new trip");
                    }
                }
                else if (numberOfTrips != NumberOfTripsOnline)
                {
                    NumberOfTripsOnline = numberOfTrips;
                }

                Thread.Sleep(15000);
                number++;

            }
        }
    }
}
