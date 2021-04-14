namespace Chat_Room_Api.Application.Services
{
    using Chat_Room_Api.Application.Models;
    using Chat_Room_Api.Infra.Proxies.TCloud.TRTC;
    using Chat_Room_Api.Infra.Proxies.TCloud.TIM;

    using System.Threading.Tasks;

    public class ChatService : IChatService
    {
        private readonly ITRTCProxy _trtcProxy;
        private readonly ITIMProxy _timProxy;

        public ChatService(ITRTCProxy trtcProxy, ITIMProxy timProxy)
        {
            _trtcProxy = trtcProxy;
            _timProxy = timProxy;
        }


        public async Task<string> CreateGroupAsync(CreateGroupRequest request)
        {
            var res = await _timProxy.CreateGroupAsync(request.GroupId,request.GroupName,request.Owner);
            return res.GroupId;
        }

        public async Task<bool> RemoveGroupAsync(RemoveGroupRequest request)
        {
            var res = await _timProxy.RemoveGroupAsync(request.GroupId);
            return true;
        }

        public async Task<bool> RemoveMemberAsync(RemoveMemberRequest request)
        {
            await _trtcProxy.RemoveUserAsync(request.RoomId, request.MemberIds);
            return true;
        }
    }
}