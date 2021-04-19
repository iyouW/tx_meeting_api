namespace Chat_Room_Api.Infra.Proxies.TCloud.TRTC
{
    using System.Threading.Tasks;
    using Chat_Room_Api.Infra.Proxies.TCloud.Options;

    using Microsoft.Extensions.Options;

    using TencentCloud.Common;
    using TencentCloud.Trtc.V20190722;
    using TencentCloud.Trtc.V20190722.Models;

    using System;

    using Polly;

    public class TRTCProxy : ITRTCProxy
    {
        private readonly TCloudAppOption _tcloudAppOption;
        private readonly TCloudInvokeOption _tcloudInvokeOption;

        private readonly Credential _credential;
        private string _region = "ap-guangzhou";
        private TrtcClient _client => new TrtcClient(_credential, _region);

        private IAsyncPolicy _policy; 

        public TRTCProxy(IOptionsMonitor<TCloudAppOption> tcloudAppOption, IOptionsMonitor<TCloudInvokeOption> tcloudInvokeOption)
        {
            _tcloudAppOption = tcloudAppOption.CurrentValue;
            _tcloudInvokeOption = tcloudInvokeOption.CurrentValue;
            _credential = new Credential
            {
                SecretId = _tcloudInvokeOption.SecretId,
                SecretKey = _tcloudInvokeOption.SecretKey
            };
            _policy = Policy.Handle<Exception>().WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000));
        }

        public async Task<RemoveUserResponse> RemoveUserAsync(ulong roomId, string[] userIds)
        {
            
            var request = new RemoveUserRequest
            {
                SdkAppId = (ulong)_tcloudAppOption.SdkAppId,
                RoomId = roomId,
                UserIds = userIds
            };
            return await _policy.ExecuteAsync(() => _client.RemoveUser(request));  
        }
    }
}