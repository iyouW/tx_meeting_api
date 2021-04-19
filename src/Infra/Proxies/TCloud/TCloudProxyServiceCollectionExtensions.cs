namespace Chat_Room_Api.Infra.Proxies.TCloud
{
    using Chat_Room_Api.Infra.Proxies.TCloud.Options;
    using Chat_Room_Api.Infra.Proxies.TCloud.Abstrations;
    using Chat_Room_Api.Infra.Proxies.TCloud.Services;

    using Chat_Room_Api.Infra.Proxies.TCloud.TIM;
    using Chat_Room_Api.Infra.Proxies.TCloud.TRTC;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Configuration;
    using Polly;

    using System;

    public static class TCloudProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddTCloudServiceBase(this IServiceCollection services,
            IConfigurationSection tcloudAppConfiguration,
            IConfigurationSection tcloudInvokeConfiguration)
        {
            services.Configure<TCloudAppOption>(tcloudAppConfiguration);
            services.Configure<TCloudInvokeOption>(tcloudInvokeConfiguration);

            services.TryAddTransient<ITCloudService, TCloudService>();

            return services;
        }

        public static IServiceCollection AddTIMProxy(this IServiceCollection services, IConfigurationSection timConfiguration)
        {
            services.Configure<TIMOption>(timConfiguration);

            services.AddHttpClient<ITIMProxyService, TIMProxyService>(c =>
            {
                c.BaseAddress = new System.Uri(timConfiguration.GetValue<string>("BaseUrl"));
            })
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.TryAddTransient<ITIMProxy, TIMProxy>();

            return services;
        }
        
        public static IServiceCollection AddTRTCProxy(this IServiceCollection services)
        {
            services.TryAddTransient<ITRTCProxy, TRTCProxy>();

            return services;
        }
    }
}