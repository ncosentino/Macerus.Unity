namespace Assets.Scripts.Api.GameObjects
{
    public interface IPrefabStitcherRegistrar
    {
        void Register(
            string prefabResourceId,
            PrefabStitchDelegate stitchCallback);
    }
}