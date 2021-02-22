using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public interface IResourceOrbBehaviourStitcher
    {
        IReadOnlyResourceOrbBehaviour Stitch(IResourceOrbPrefab resourceOrbPrefab, IIdentifier currentStatDefinitionId, IIdentifier maximumStatDefinitionId);
    }
}