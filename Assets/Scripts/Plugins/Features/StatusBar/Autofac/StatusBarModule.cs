using Assets.Scripts.Plugins.Features.StatusBar.Noesis;
using Assets.Scripts.Plugins.Features.StatusBar.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.StatusBar.Autofac
{
    public sealed class StatusBarModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatusBarView>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .UsingConstructor(typeof(IStatusBarNoesisViewModel));
            builder
                .RegisterType<StatusBarNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AbilityToNoesisViewModelConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
