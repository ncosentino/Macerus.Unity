using Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis;
using Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Autofac
{
    public sealed class CombatTurnOrderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CombatTurnOrderView>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .UsingConstructor(typeof(ICombatTurnOrderNoesisViewModel));
            builder
                .RegisterType<CombatTurnOrderNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatPortraitNoesisViewModelConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
