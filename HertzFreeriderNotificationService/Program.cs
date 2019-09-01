using System;

namespace HertzFreeriderNotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var _sms = new SmsNotification();
            _sms.SendMessage("Testing Mannnennn");
        }
    }
}
