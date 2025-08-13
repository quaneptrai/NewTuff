using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Aris3._0Fe.Services
{
    public class SendEmail
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SendEmail> _logger;

        public SendEmail(HttpClient httpClient, ILogger<SendEmail> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<string> SendVerificationEmailAsync(string email)
        {
            try
            {
                var response = await _httpClient.PostAsync($"https://localhost:7248/api/gmail/{email}",null);
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request failed while sending email.");
                return $"Request failed: {ex.Message}";
            }
        }
    }
}
