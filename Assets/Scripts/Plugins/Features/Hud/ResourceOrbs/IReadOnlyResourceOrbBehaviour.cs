using Assets.Scripts.Unity;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public interface IReadOnlyResourceOrbBehaviour
    {
        IIdentifier CurrentStatDefinitionId { get; }
        
        IReadOnlyMapGameObjectManager MapGameObjectManager { get; }
        
        IIdentifier MaximumStatDefinitionId { get; }
        
        IResourceOrbPrefab ResourceOrbPrefab { get; }
        
        IStatCalculationService StatCalculationService { get; }
        
        ITimeProvider TimeProvider { get; }
    }
}
