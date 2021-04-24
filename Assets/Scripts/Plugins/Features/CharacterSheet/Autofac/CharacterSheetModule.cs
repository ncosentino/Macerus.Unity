using Assets.Scripts.Plugins.Features.CharacterSheet.Noesis;
using Assets.Scripts.Plugins.Features.CharacterSheet.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.CharacterSheet.Autofac
{
    public sealed class CharacterSheetModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CharacterSheetView>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .UsingConstructor(typeof(ICharacterSheetNoesisViewModel));
            builder
                .RegisterType<CharacterSheetNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
