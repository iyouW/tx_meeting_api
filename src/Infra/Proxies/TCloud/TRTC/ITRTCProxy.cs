namespace Chat_Room_Api.Infra.Proxies.TCloud.TRTC
{
    using TencentCloud.Trtc.V20190722.Models;
    using System.Threading.Tasks;
    
    public interface ITRTCProxy
    {
        Task<RemoveUserResponse> RemoveUserAsync(ulong roomId, string[] userIds);
    }
}