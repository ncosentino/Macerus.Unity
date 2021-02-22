
using Assets.Scripts.Unity;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public interface IResourceOrbBehaviour : IReadOnlyResourceOrbBehaviour
    {
        new IIdentifier CurrentStatDefinitionId { get; set; }
        
        new IGameObjectManager GameObjectManager { get; set; }
        
        new IIdentifier MaximumStatDefinitionId { get; set; }
        
        new IResourceOrbPrefab ResourceOrbPrefab { get; set; }
        
        new IStatCalculationService StatCalculationService { get; set; }
        
        new ITimeProvider TimeProvider { get; set; }
    }
}
