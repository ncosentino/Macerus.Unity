using Assets.Scripts.Gui.UnityViewWelding.Api;
using Assets.Scripts.Gui.UnityViewWelding.Welders;

using Autofac;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

using UnityEngine;

namespace Assets.Scripts.Gui.UnityViewWelding.Autofac
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
                        (p, c, t) => p is GameObject && c is GameObject & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((GameObject)p).transform, ((GameObject)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is GameObject && typeof(ISimpleWelder).IsAssignableFrom(t),
                        (p, c) => new SimpleTransformWelder(((GameObject)p).transform, ((GameObject)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is GameObject && typeof(IInsetWelder).IsAssignableFrom(t),
                        (p, c) => new InsetGameObjectWelder((GameObject)p, (GameObject)c));
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is GameObject && typeof(IMarginWelder).IsAssignableFrom(t),
                        (p, c) => new MarginGameObjectWelder((GameObject)p, (GameObject)c));

                    // game objects & transforms
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is Transform & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((GameObject)p).transform, ((Transform)c)));
                    welderRegistrar.Register(
                        (p, c, t) => p is Transform && c is GameObject & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((Transform)p), ((GameObject)c).transform));

                    // transforms
                    welderRegistrar.Register(
                        (p, c, t) => p is Transform && c is Transform & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((Transform)p), ((Transform)c)));
                    welderRegistrar.Register(
                        (p, c, t) => p is Transform && c is Transform && typeof(ISimpleWelder).IsAssignableFrom(t),
                        (p, c) => new SimpleTransformWelder((Transform)p, (Transform)c));

                    // behaviours and game objects
                    welderRegistrar.Register(
                        (p, c, t) => p is GameObject && c is Behaviour & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((GameObject)p).transform, ((Behaviour)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is GameObject & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((Behaviour)p).transform, ((GameObject)c).transform));

                    // behaviours and transforms
                    welderRegistrar.Register(
                        (p, c, t) => p is Transform && c is Behaviour & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((Transform)p), ((Behaviour)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is Transform & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((Behaviour)p).transform, ((Transform)c)));

                    // behaviours and behaviours
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is Behaviour & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((Behaviour)p).transform, ((Behaviour)c).transform));
                    welderRegistrar.Register(
                        (p, c, t) => p is Behaviour && c is Behaviour & t.IsAssignableFrom(typeof(IStackViewWelder)),
                        (p, c) => new TransformStackViewWelder(((Behaviour)p).transform, ((Behaviour)c).transform));
                });
        }
    }
}
