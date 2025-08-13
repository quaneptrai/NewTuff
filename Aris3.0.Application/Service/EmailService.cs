using Aris3._0.Application.Interface.Service;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;

public class EmailService : IEmailService
{
    private static readonly ConcurrentQueue<DateTime> _sentTimestamps = new();

    private static readonly int MaxEmails = 5;
    private static readonly TimeSpan Window = TimeSpan.FromMinutes(3);

    public async Task<string> SenEmail(string email)
    {
        lock (_sentTimestamps)
        {
            // Remove timestamps older than 3 minutes
            while (_sentTimestamps.TryPeek(out var time) && time < DateTime.UtcNow - Window)
            {
                _sentTimestamps.TryDequeue(out _);
            }

            if (_sentTimestamps.Count >= MaxEmails)
            {
                return $"Too many request please try again later !";
            }

            // Register this send attempt timestamp
            _sentTimestamps.Enqueue(DateTime.UtcNow);
        }
        try
        {
            // Load your JSON credentials
            var jsonText = await File.ReadAllTextAsync("C:\\Aris\\Aris3.0\\Key.json");
            var json = JObject.Parse(jsonText)["web"];

            string clientId = json.Value<string>("client_id");
            string clientSecret = json.Value<string>("client_secret");
            string refreshToken = json.Value<string>("refresh_token");

            // Create token response with your refresh token
            var token = new Google.Apis.Auth.OAuth2.Responses.TokenResponse
            {
                RefreshToken = refreshToken
            };

            // Setup the flow with your client secrets
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                }
            });

            // Create user credential with refresh token
            var credential = new UserCredential(flow, "user", token);

            // Refresh access token now (to make sure it's valid)
            bool success = await credential.RefreshTokenAsync(CancellationToken.None);
            if (!success)
                return "Failed to refresh access token";

            // Create Gmail API service
            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "My Gmail Sender"
            });
            string body = @"<div id="":ne"" class=""ii gt"" jslog=""20277; u014N:xr6bB; 1:WyIjdGhyZWFkLWY6MTg0MDI0NTM2ODE2NTM1OTM1MyJd; 4:WyIjbXNnLWY6MTg0MDI0NTM2ODE2NTM1OTM1MyIsbnVsbCxudWxsLG51bGwsMSwwLFsxLDAsMF0sNDcsMzA3LG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCwxLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLDAsMF0.""><div id="":nf"" class=""a3s aiL ""><div class=""adM"">
</div><div style=""max-width:720px;margin:0 auto;padding:32px 16px 64px 16px""><div class=""adM"">
  </div><p style=""text-align:center;margin:0"">
    <img width=""210px"" src=""https://ci3.googleusercontent.com/meips/ADKq_NYrB027g0VFPczdW4B0dHalOSIITtXiLsGwnoGNKWVghtlmXznqzNwEU8FnItmXNf_k1SZmRDNYB7RcMLOuF7eJML87imiCx_x2s4QVHR3fziB7u15RKg4GvwAA1KkH_Al-osK1pHMesW-EmHGb_LXTBFojs4un3ov6xx2hZanRsvgtno7rwGBpV6YpN0WcypCI0s8=s0-d-e1-ft#https://webstatic-sea.hoyoverse.com/upload/static-resource/2022/01/11/747ab8eb3a68ca50fbff6f74e1269d62_1698485033492101480.png"" class=""CToWUd"" data-bit=""iit"">
  </p>
  <div style=""margin-top:24px"">
    <div style=""text-align:center;padding-bottom:16px;color:#242629;font-size:28px;font-weight:700;line-height:32px"">
      [HoYoverse] Verification Code
    </div>
    <div style=""border-radius:20px;background:#fff;padding:20px;font-size:14px;font-weight:400;line-height:20px"">
      <div style=""margin-bottom:16px"">Dear User,</div>
      <div style=""margin-bottom:24px"">
        Please use the verification code below to proceed with the identity verification process.
      </div>
      <div style=""margin-bottom:24px;font-size:24px;font-weight:700;line-height:28px"">
        870850
      </div>
      <div>Thank you,<br>HoYoverse</div>
    </div>
  </div>
  <div style=""font-size:12px;font-weight:400;line-height:18px;margin-top:24px"">
    Please do not reply to this email as it is auto-generated.
  </div>
  <div style=""width:100%;text-align:center;color:#6b707b;font-size:12px;font-weight:400;line-height:16px;padding-top:12px;border-top:1px solid #edeef3;margin-top:24px"">
    Cognosphere Pte Ltd<br>
    Address: 1 One North Crescent Razer Sea HQ #06-01/02 Singapore 138538
  </div><div class=""yj6qo""></div><div class=""adL"">
</div></div><div class=""adL"">
</div></div></div>";
            // Build simple MIME message
            var mimeMessage = new StringBuilder();
            mimeMessage.AppendLine($"To: {email}");
            mimeMessage.AppendLine("Subject: [HoYoverse] Verification Code");
            mimeMessage.AppendLine("Content-Type: text/html; charset=utf-8");
            mimeMessage.AppendLine();
            mimeMessage.AppendLine(body);

            // Encode Base64 URL-safe string
            var rawMessage = Convert.ToBase64String(Encoding.UTF8.GetBytes(mimeMessage.ToString()))
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            var gmailMessage = new Message { Raw = rawMessage };

            // Send the message
            await service.Users.Messages.Send(gmailMessage, "me").ExecuteAsync();

            return "OK";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
