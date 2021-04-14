namespace Chat_Room_Api.Infra.Proxies.TCloud.Abstrations
{
    public interface ITCloudService
    {
        string GenerateUserSig(string userId, int sdkAppId, string secretKey, int? expire);
    }
}