using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HertzFreeriderWorkerService
{
    public class EmailNotification
    {
        public async Task SendMessage(string subject, string message, ILogger<Worker> _logger)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>();

            var content = new FormUrlEncodedContent(values);

            //var response = await client.PostAsync($"http://LOCALHOST:9000?SUBJECT={subject}&MESSAGE={message}", content);
            var response = await client.PostAsync($"http://192.168.1.183:9000?SUBJECT={subject}&MESSAGE={message}", content);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Email sent");
            }
            else
            {
                _logger.LogError("Error when sending Email");
            }
        }
    }
}
