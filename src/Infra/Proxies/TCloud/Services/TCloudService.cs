namespace Chat_Room_Api.Infra.Proxies.TCloud.Services
{
    using Chat_Room_Api.Infra.Proxies.TCloud.Abstrations;
    using tencentyun;

    public class TCloudService : ITCloudService
    {
        public string GenerateUserSig(string userId, int sdkAppId, string secretKey, int? expire)
        {
            var generator = new TLSSigAPIv2(sdkAppId, secretKey);
            if(expire.HasValue)
            {
                return generator.GenSig(userId, expire.Value);
            }
            else
            {
                return generator.GenSig(userId);
            }
        }
    }
}