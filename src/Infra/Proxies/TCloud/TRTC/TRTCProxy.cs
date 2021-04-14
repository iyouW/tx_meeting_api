namespace Chat_Room_Api.Infra.Proxies.TCloud.TRTC
{
    using System.Threading.Tasks;
    using Chat_Room_Api.Infra.Proxies.TCloud.Options;

    using Microsoft.Extensions.Options;

    using TencentCloud.Common;
    using TencentCloud.Trtc.V20190722;
    using TencentCloud.Trtc.V20190722.Models;

    public class TRTCProxy : ITRTCProxy
    {
        private readonly TCloudAppOption _tcloudAppOption;
        private readonly TCloudInvokeOption _tcloudInvokeOption;

        public TRTCProxy(IOptionsMonitor<TCloudAppOption> tcloudAppOption, IOptionsMonitor<TCloudInvokeOption> tcloudInvokeOption)
        {
            _tcloudAppOption = tcloudAppOption.CurrentValue;
            _tcloudInvokeOption = tcloudInvokeOption.CurrentValue;
        }

        public async Task<RemoveUserResponse> RemoveUserAsync(ulong roomId, string[] userIds)
        {
            try
            {
                var credential = new Credential
                {
                    SecretId = _tcloudInvokeOption.SecretId,
                    SecretKey = _tcloudInvokeOption.SecretKey
                };
                var client = new TrtcClient(credential,"ap-guangzhou");
                var request = new RemoveUserRequest
                {
                    SdkAppId = (ulong)_tcloudAppOption.SdkAppId,
                    RoomId = roomId,
                    UserIds = userIds
                };
                return await client.RemoveUser(request);  
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}