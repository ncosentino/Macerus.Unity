namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IGameObjectBehaviorInterceptorRegistrar
    {
        void Register(IGameObjectBehaviorInterceptor gameObjectBehaviorInterceptor);
    }
}