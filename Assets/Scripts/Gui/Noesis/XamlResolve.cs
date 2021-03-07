using System;

namespace Assets.Scripts.Gui.Noesis
{
    public static class XamlResolve
    {
        public static string ExpectedXamlPath(Type type)
        {
            var path = $"{type.Namespace.Replace(".", "/")}/{type.Name}.xaml";
            return path;
        }
    }
}
