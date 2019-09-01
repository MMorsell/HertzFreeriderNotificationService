using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HertzFreeriderNotificationService
{
    public class SmsNotification
    {
        public void SendMessage(string content)
        {

            string accountSid = GetAccountSid();
            string authToken = GetAuthToken();
            string number = GetPhoneNumber();

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: content,
                from: new Twilio.Types.PhoneNumber("+46790644265"),
                to: new Twilio.Types.PhoneNumber(number)
            );
        }

        private string GetAccountSid()
        {
            string fileName = "accountsid.txt";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullName = System.IO.Path.Combine(desktopPath, fileName);
            string key;


            using (StreamReader steamReader = new StreamReader(fullName))
            {
                key = steamReader.ReadToEnd();
            }

            return key;
        }

        private string GetAuthToken()
        {
            string fileName = "authtoken.txt";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullName = System.IO.Path.Combine(desktopPath, fileName);
            string key;


            using (StreamReader steamReader = new StreamReader(fullName))
            {
                key = steamReader.ReadToEnd();
            }

            return key;
        }

        private string GetPhoneNumber()
        {
            string fileName = "number.txt";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullName = System.IO.Path.Combine(desktopPath, fileName);
            string key;


            using (StreamReader steamReader = new StreamReader(fullName))
            {
                key = steamReader.ReadToEnd();
            }

            return key;
        }
    }
}
