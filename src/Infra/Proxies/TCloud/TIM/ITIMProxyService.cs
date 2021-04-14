namespace Chat_Room_Api.Infra.Proxies.TCloud.TIM
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ITIMProxyService
    {
        Task<T> PostAsync<T>(string serviceName, string actionName, object param, Dictionary<string, string> headers = null);
    }
}