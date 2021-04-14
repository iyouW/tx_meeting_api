namespace Chat_Room_Api.Infra.Proxies.TCloud.TIM
{
    using Chat_Room_Api.Infra.Proxies.TCloud.TIM.Models;
    using Chat_Room_Api.Infra.Proxies.TCloud.TIM.Models.Group;

    using System.Threading.Tasks;

    public class TIMProxy : ITIMProxy
    {
        private readonly ITIMProxyService _service;

        public TIMProxy(ITIMProxyService service)
        {
            _service = service;
        }

        public Task<CreateGroupResponse> CreateGroupAsync(string groupId, string groupName, string owner)
        {
            var param = new 
            {
                OwnerAccount = owner,
                GroupId = groupId,
                Name = groupName,
                Type = "ChatRoom"
            };
            return _service.PostAsync<CreateGroupResponse>("group_open_http_svc","create_group",param);
        }

        public Task<TIMHttpResponseBase> RemoveGroupAsync(string groupId)
        {
            var param = new
            {
                GroupId = groupId
            };
            return _service.PostAsync<TIMHttpResponseBase>("group_open_http_svc","destroy_group",param);
        }
    }
}