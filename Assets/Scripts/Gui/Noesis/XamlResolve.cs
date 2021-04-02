using System;

using NexusLabs.Contracts;

namespace Assets.Scripts.Gui.Noesis
{
    public static class XamlResolve
    {
        public static string RelativeResourcesXamlPath(Type type)
        {
            Contract.Requires(
                type.Namespace.Contains("Resources"),
                $"Expecting that '{type.FullName}' is found within a namespace " +
                $"that includes 'Resources'. This is to facilitate Unity " +
                $"resource loading. See " +
                $"https://docs.unity3d.com/ScriptReference/Resources.html.");
            var pathPrefix = type.Namespace
                .Substring(type.Namespace.IndexOf("Resources") + "Resources".Length)
                .Replace(".", "/");
            var path = $"{pathPrefix}{(string.IsNullOrEmpty(pathPrefix) ? "" : "/")}{type.Name}";
            return path;
        }

        public static string XamlPathForComponentLoad(Type type)
        {
            var path = $"{type.Namespace.Replace(".", "/")}/{type.Name}.xaml";
            return path;
        }
    }
}
