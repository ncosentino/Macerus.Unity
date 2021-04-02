using Noesis;

namespace Assets.Scripts.Gui.Noesis
{
    public static class NoesisComponentInitializer
    {
        public static void InitializeComponentXaml(FrameworkElement frameworkElement)
        {
            var xamlPath = XamlResolve.XamlPathForComponentLoad(frameworkElement.GetType());
            GUI.LoadComponent(
                frameworkElement,
                xamlPath);
        }
    }
}
