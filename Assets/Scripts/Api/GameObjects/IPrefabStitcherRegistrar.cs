using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IPrefabStitcherRegistrar
    {
        void Register(
            IIdentifier prefabResourceId,
            PrefabStitchDelegate stitchCallback);
    }
}