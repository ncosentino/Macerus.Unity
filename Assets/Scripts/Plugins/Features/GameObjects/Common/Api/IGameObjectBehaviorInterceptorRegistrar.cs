namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IGameObjectBehaviorInterceptorRegistrar
    {
        void Register(IGameObjectBehaviorInterceptor gameObjectBehaviorInterceptor);
    }
}