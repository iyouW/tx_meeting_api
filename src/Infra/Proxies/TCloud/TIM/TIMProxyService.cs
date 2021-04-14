namespace Chat_Room_Api.Infra.Proxies.TCloud.TIM
{
    using Chat_Room_Api.Infra.Proxies.TCloud.Abstrations;
    using Chat_Room_Api.Infra.Proxies.TCloud.Options;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Logging;

    public class TIMProxyService : ITIMProxyService
    {
        private HttpClient _client;
        private TCloudAppOption _tcloudAppOption;
        private TIMOption _option;
        private ILogger<TIMProxyService> _logger;
        private ITCloudService _tcloudService;

        public TIMProxyService(HttpClient client, IOptionsMonitor<TCloudAppOption> tcloudAppOption,
            IOptionsMonitor<TIMOption> option, ITCloudService tcloudService, ILogger<TIMProxyService> logger)
        {
            _client = client;
            _tcloudAppOption = tcloudAppOption.CurrentValue;
            _option = option.CurrentValue;
            _tcloudService = tcloudService;
            _logger = logger;
        }

        public async Task<T> PostAsync<T>(string serviceName, string actionName, object param, Dictionary<string, string>? headers = null)
        {
            var url = BuildUrl(serviceName, actionName);
            var json = JsonSerializer.Serialize(param);
            var content = new StringContent(json, Encoding.UTF8,"application/json");
            var resp = await _client.PostAsync(url, content);
            resp.EnsureSuccessStatusCode();
            var stream = await resp.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        private string BuildUrl(string serviceName, string actionName)
        {
            string BuildQueryString(string userId, string userSig, int sdkAppId, string secretKey, int random)
            {
                return $"sdkappid={sdkAppId}&identifier={userId}&usersig={userSig}&random={random}&contenttype=json";
            }

            var random = new Random().Next(int.MaxValue);
            var sdkAppId = _tcloudAppOption.SdkAppId;
            var secretKey = _tcloudAppOption.SecretKey;
            var userId = _option.Admin;
            var userSig = _tcloudService.GenerateUserSig(userId,sdkAppId,secretKey, null);
            var queryString = BuildQueryString(userId,userSig,sdkAppId,secretKey,random);

            return $"{serviceName}/{actionName}?{queryString}";
        }
    }

    internal static class Log
    {
        private static readonly Action<ILogger,string, Exception> _logRequest = LoggerMessage.Define<string>(LogLevel.Debug,new EventId(1,""),"");

        private static readonly Action<ILogger,string,Exception> _logResponse = LoggerMessage.Define<string>(LogLevel.Debug, new EventId(2,""),"");

        internal static void LogRequest(ILogger logger, string message)
        {
            _logRequest(logger,message,null);
        }

        internal static void LogResponse(ILogger logger, string message)
        {
            _logResponse(logger, message, null);
        }
    }
}