using System;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;

using Autofac;

namespace Assets.ContentCreator.MapEditor.Autofac
{
    public sealed class MapEditorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            foreach (var type in AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract &&
                    (typeof(IDiscoverableBehaviorConverter).IsAssignableFrom(x) ||
                    typeof(IDiscoverableGameObjectToBehaviorConverter).IsAssignableFrom(x) ||
                    typeof(IDiscoverableBehaviorToBaseGameObjectConverter).IsAssignableFrom(x))))
            {
                builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
            }

            builder
                .RegisterType<SceneToMapConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BehaviorConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameObjectToBehaviorConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BehaviorToBaseGameObjectConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GameObjectConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<Editor.MapEditor>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
