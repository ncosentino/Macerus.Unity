using Assets.Scripts.Unity;

using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public interface IReadOnlyResourceOrbBehaviour
    {
        IIdentifier CurrentStatDefinitionId { get; }
        
        IReadOnlyMapGameObjectManager MapGameObjectManager { get; }
        
        IIdentifier MaximumStatDefinitionId { get; }
        
        IResourceOrbPrefab ResourceOrbPrefab { get; }

        IStatCalculationServiceAmenity StatCalculationServiceAmenity { get; }
        
        ITimeProvider TimeProvider { get; }
    }
}
