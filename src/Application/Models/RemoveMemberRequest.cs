namespace Chat_Room_Api.Application.Models
{
    public class RemoveMemberRequest
    {
        public ulong RoomId { get; set; }
        public string[] MemberIds {get; set;}
    }
}