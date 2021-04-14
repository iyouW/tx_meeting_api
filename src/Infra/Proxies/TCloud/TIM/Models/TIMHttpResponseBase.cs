namespace Chat_Room_Api.Infra.Proxies.TCloud.TIM.Models
{
    public class TIMHttpResponseBase
    {
        public string ActionStatus {get; set;}
        public string ErrorInfo {get; set;}
        public int ErrorCode {get; set;}
    }
}