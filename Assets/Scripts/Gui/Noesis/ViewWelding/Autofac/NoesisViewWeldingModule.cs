#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Unity.Resources;

using Noesis;
#else
using System.Windows.Controls;
#endif

using System;
using System.Windows;

using Autofac;

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
                        (p, c, t) => p is Grid && c is UIElement && t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new GridControlWelder((Grid)p, (UIElement)c));
                    welderRegistrar.Register(
                        (p, c, t) => p is StackPanel && c is UIElement && t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new StackPanelWelder((StackPanel)p, (UIElement)c));
                    welderRegistrar.Register(
                        (p, c, t) => p is ContentControl && c is object && t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new ContentControlWelder((ContentControl)p, c));
                    welderRegistrar.Register(
                        (p, c, t) => p is Viewbox && c is UIElement && t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new ViewboxWelder((Viewbox)p, (UIElement)c));
#if NOESIS
                    var lazyResourceLoader = x.Resolve<Lazy<IResourceLoader>>();
                    welderRegistrar.Register(
                        (p, c, t) => p is NoesisView && c is Type type && typeof(FrameworkElement).IsAssignableFrom(type) && t.IsAssignableFrom(typeof(ISimpleWelder)),
                        (p, c) => new NoesisViewTypeWelder(lazyResourceLoader.Value, (NoesisView)p, (Type)c));
#endif
                });
        }
    }
}
