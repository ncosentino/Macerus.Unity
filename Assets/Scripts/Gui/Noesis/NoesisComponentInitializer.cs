using System;

using NexusLabs.Contracts;

using Noesis;

namespace Assets.Scripts.Gui.Noesis
{
    public static class NoesisComponentInitializer
    {
        public static void InitializeComponentXaml(FrameworkElement frameworkElement)
        {
            var xamlPath = XamlResolve.XamlPathForComponentLoad(frameworkElement.GetType());
            Contract.Requires(
                xamlPath.EndsWith(".xaml", System.StringComparison.OrdinalIgnoreCase),
                $"Expecting '{xamlPath}' to end with a *.xaml extension. The " +
                $"path may have been generated incorrectly.");

            try
            {
                GUI.LoadComponent(
                    frameworkElement,
                    xamlPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Exception caught while loading xaml asset. If you're " +
                    $"confident your asset exists (should have it corresponding to " +
                    $"'{xamlPath}'), then Noesis may have not regenerated it. " +
                    $"You could try checking its existence in the project by " +
                    $"'showing all files'. See inner exception for more details.",
                    ex);
            }
        }
    }
}
