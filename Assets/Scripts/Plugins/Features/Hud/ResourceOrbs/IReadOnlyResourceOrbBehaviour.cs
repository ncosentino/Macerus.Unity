
using Assets.Scripts.Unity;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public interface IReadOnlyResourceOrbBehaviour
    {
        IIdentifier CurrentStatDefinitionId { get; }
        
        IGameObjectManager GameObjectManager { get; }
        
        IIdentifier MaximumStatDefinitionId { get; }
        
        IResourceOrbPrefab ResourceOrbPrefab { get; }
        
        IStatCalculationService StatCalculationService { get; }
        
        ITimeProvider TimeProvider { get; }
    }
}
