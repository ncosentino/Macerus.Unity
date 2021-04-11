using System.Linq;
using System.Reflection;

using Assets.Scripts;

using Autofac;

using IngameDebugConsole;

namespace Assets.Scripts.Plugins.Features.Console
{
    public sealed class ConsoleCommandRegistrar : IConsoleCommandRegistrar
    {
        public void RegisterDiscoverableCommandsFromInstance(object instance)
        {
            foreach (var methodToRegister in instance
                .GetType()
                .GetMethods(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic)
                .Select(x => new
                {
                    Attribute = x.GetCustomAttributes<DiscoverableConsoleCommandAttribute>().FirstOrDefault(),
                    MethodName = x.Name,
                })
                .Where(x => x.Attribute != null))
            {
                AddCommand(
                    methodToRegister.MethodName,
                    methodToRegister.Attribute.Description,
                    instance);
            }
        }

        private void AddCommand(
            string name,
            string description,
            object instance)
        {
            DebugLogConsole.AddCommandInstance(
                name,
                description,
                name,
                instance);
        }
    }
}
