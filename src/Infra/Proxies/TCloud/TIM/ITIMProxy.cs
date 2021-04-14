namespace Chat_Room_Api.Infra.Proxies.TCloud.TIM
{
    using Chat_Room_Api.Infra.Proxies.TCloud.TIM.Models;
    using Chat_Room_Api.Infra.Proxies.TCloud.TIM.Models.Group;
    
    using System.Threading.Tasks;
    
    public interface ITIMProxy
    {
        Task<CreateGroupResponse> CreateGroupAsync(string groupId, string groupName, string owner);
        Task<TIMHttpResponseBase> RemoveGroupAsync(string groupId);
    }
}