namespace Chat_Room_Api.Controller
{
    using Chat_Room_Api.Application.Models;
    using Chat_Room_Api.Application.Services;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        
        private readonly IChatService _service;

        public ChatController(IChatService service)
        {
            _service = service;
        }

        [HttpPost("createGroup")]
        public Task<string> CreateGroup(CreateGroupRequest request)
        {
            return _service.CreateGroupAsync(request);
        }

        [HttpPost("removeGroup")]
        public Task<bool> RemoveGroup(RemoveGroupRequest request)
        {
            return _service.RemoveGroupAsync(request);
        }

        [HttpPost("removeMember")]
        public Task<bool> RemoveMember(RemoveMemberRequest request)
        {
            return _service.RemoveMemberAsync(request);
        }
    }
}