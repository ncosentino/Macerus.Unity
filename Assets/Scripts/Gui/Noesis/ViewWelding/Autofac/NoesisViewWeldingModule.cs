using Autofac;

using Noesis;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Gui.Noesis.ViewWelding.Autofac
{
    public sealed class NoesisViewWeldingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterBuildCallback(x =>
                {
                    var welderRegistrar = x.Resolve<IViewWelderFactoryRegistrar>();

                    welderRegistrar.Register(
                        (p, c, t) => p is IGuiPrefab && c is object & t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new NoesisGuiPrefabWelder(x.Resolve<IViewWelderFactory>(), (INoesisGuiPrefab)p, c));
                    welderRegistrar.Register(
                        (p, c, t) => p is ContentControl && c is object & t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new ContentControlWelder((ContentControl)p, c));
                    welderRegistrar.Register(
                        (p, c, t) => p is Viewbox && c is UIElement & t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new ViewboxWelder((Viewbox)p, (UIElement)c));
                });
        }
    }
}
