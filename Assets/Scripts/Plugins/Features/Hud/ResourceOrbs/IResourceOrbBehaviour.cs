
using Assets.Scripts.Unity;

using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public interface IResourceOrbBehaviour : IReadOnlyResourceOrbBehaviour
    {
        new IIdentifier CurrentStatDefinitionId { get; set; }
        
        new IReadOnlyMapGameObjectManager MapGameObjectManager { get; set; }
        
        new IIdentifier MaximumStatDefinitionId { get; set; }
        
        new IResourceOrbPrefab ResourceOrbPrefab { get; set; }
        
        new IStatCalculationServiceAmenity StatCalculationServiceAmenity { get; set; }
        
        new ITimeProvider TimeProvider { get; set; }
    }
}
