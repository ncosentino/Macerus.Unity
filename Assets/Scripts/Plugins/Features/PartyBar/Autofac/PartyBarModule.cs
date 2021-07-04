using Assets.Scripts.Plugins.Features.PartyBar.Noesis;
using Assets.Scripts.Plugins.Features.PartyBar.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.PartyBar.Autofac
{
    public sealed class PartyBarModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PartyBarView>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .UsingConstructor(typeof(IPartyBarNoesisViewModel));
            builder
                .RegisterType<PartyBarNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PartyBarPortraitNoesisViewModelConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
