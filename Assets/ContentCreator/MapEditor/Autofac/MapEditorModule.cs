using System;
using System.Collections.Generic;

using Assets.ContentCreator.MapEditor.Behaviours;

using Autofac;

using ProjectXyz.Shared.Data.Serialization;

namespace Assets.ContentCreator.MapEditor.Autofac
{
    public sealed class MapEditorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DynamicAnimationBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PrefabResourceIdConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EditorPrefabResourceIdConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ImplicitEditorPrefabResourceIdConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ImplicitIdentifierBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TemplateIdentifierBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TypeIdentifierBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<IdentifierBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PositionBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SizeBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BoxColliderBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CircleColliderBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TriggerOnCombatEndBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DoorInteractableBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpawnTemplatePropertiesBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EditorNameBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
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
        }
    }
}
