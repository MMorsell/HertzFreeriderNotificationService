using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HertzFreeriderWorkerService
{
    
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        public HttpClient _client { get; set; }
        public NotificationService(ILogger<NotificationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            string _bearer = System.IO.File.ReadAllText("/run/secrets/notification_bearer");

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", _bearer);
        }
        public async Task SendMessage(NotificationModel payload)
        {
            string json = JsonConvert.SerializeObject(payload);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"http://192.168.1.112:8123/api/events/NotificationRelay", content);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Notification sent");
            }
            else
            {
                _logger.LogError("Error when sending notification");
            }
        }
    }
}
