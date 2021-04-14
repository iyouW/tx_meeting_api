namespace Chat_Room_Api.Application.Services
{
    using Chat_Room_Api.Application.Models;

    using System.Threading.Tasks;

    public interface IChatService
    {
        Task<string> CreateGroupAsync(CreateGroupRequest request);
        Task<bool> RemoveGroupAsync(RemoveGroupRequest request);
        Task<bool> RemoveMemberAsync(RemoveMemberRequest request);
    }
}