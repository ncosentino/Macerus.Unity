using Autofac;

namespace Assets.Scripts.Plugins.Features.Combat.Autofac
{
    public sealed class CombatModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CombatController>()
                .AutoActivate() // FIXME: this always feels like a smell to have to do
                .AsSelf();
        }
    }
}
