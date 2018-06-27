using Assets.Scripts.Plugins.Features.UnityViewWelding.Api;
using Autofac;
using ProjectXyz.Framework.ViewWelding.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.UnityViewWelding
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterBuildCallback(x =>
                {
                    var welderRegistrar = x.Resolve<IViewWelderFactoryRegistrar>();

                    // game objects
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is GameObject & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((GameObject)p).transform, ((GameObject)c).transform));

                    // game objects & transforms
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is Transform & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((GameObject)p).transform, ((Transform)c)));
                    welderRegistrar.Register(
                        (p, c, t) => p is Transform && c is GameObject & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((Transform)p), ((GameObject)c).transform));

                    // transforms
                    welderRegistrar.Register(
                        (p, c, t) => p is Transform && c is Transform & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((Transform)p), ((Transform)c)));

                    // behaviours and game objects
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is Behaviour & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((GameObject)p).transform, ((Behaviour)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is GameObject & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((Behaviour)p).transform, ((GameObject)c).transform));

                    // behaviours and transforms
                    welderRegistrar.Register(
                        (p, c, t) => p is Transform && c is Behaviour & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((Transform)p), ((Behaviour)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is Transform & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((Behaviour)p).transform, ((Transform)c)));

                    // behaviours and behaviours
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is Behaviour & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((Behaviour)p).transform, ((Behaviour)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is Behaviour & t.IsAssignableFrom(typeof(ISimpleViewWelder)),
                        (p, c) => new SimpleTransformViewWelder(((Behaviour)p).transform, ((Behaviour)c).transform));
                });
        }
    }
}
