namespace Chat_Room_Api.Application.Models
{
    public class CreateGroupRequest
    {
        public string GroupId {get; set;}
        public string GroupName {get; set;}
        public string Owner { get; set; }
    }
}