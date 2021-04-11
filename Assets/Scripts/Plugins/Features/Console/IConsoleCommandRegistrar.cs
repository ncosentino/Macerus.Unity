namespace Assets.Scripts.Plugins.Features.Console
{
    public interface IConsoleCommandRegistrar
    {
        void RegisterDiscoverableCommandsFromInstance(object instance);
    }
}