﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.Modularity;
using Volo.Abp.Security;

namespace Volo.Abp.IdentityServer
{
    [DependsOn(typeof(AbpIdentityServerDomainSharedModule))]
    [DependsOn(typeof(AbpDddModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpIdentityDomainModule))]
    [DependsOn(typeof(AbpSecurityModule))]
    public class AbpIdentityServerDomainModule : AbpModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<ClientAutoMapperProfile>(validate: true);
            });

            services.AddAssemblyOf<AbpIdentityServerDomainModule>();

            AddIdentityServer(services);
        }

        private static void AddIdentityServer(IServiceCollection services)
        {
            var identityServerBuilder = services.AddIdentityServer();

            //TODO: Remove in-memory stores once EF Core stores are fully completed.
            identityServerBuilder.AddDeveloperSigningCredential()
                .AddAbpIdentityServer();

            services.ExecutePreConfiguredActions(identityServerBuilder);
        }
    }
}
