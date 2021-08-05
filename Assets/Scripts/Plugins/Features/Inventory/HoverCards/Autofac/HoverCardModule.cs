using Autofac;

namespace Assets.Scripts.Plugins.Features.Inventory.HoverCards.Autofac
{
    public sealed class HoverCardModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HoverCardViewFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SingleNameHoverCardPartConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MultiNameHoverCardPartConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BaseStatsHoverCardPartConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();            
        }
    }
}
