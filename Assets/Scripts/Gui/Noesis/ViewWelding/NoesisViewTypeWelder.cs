#if UNITY_5_3_OR_NEWER
#define NOESIS
using System;

using Assets.Scripts.Unity.Resources;

using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Gui.Noesis.ViewWelding
{
    public sealed class NoesisViewTypeWelder : ISimpleWelder
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly NoesisView _parent;
        private readonly Type _childType;

        public NoesisViewTypeWelder(
            IResourceLoader resourceLoader,
            NoesisView parent,
            Type childType)
        {
            _resourceLoader = resourceLoader;
            _parent = parent;
            _childType = childType;
        }

        public IWeldResult Weld()
        {
            var xamlPath = XamlResolve.RelativeResourcesXamlPath(_childType);
            var xaml = _resourceLoader.Load<NoesisXaml>(xamlPath);
            _parent.Xaml = xaml;
            _parent.LoadXaml(true);

            return new WeldResult(_parent, _parent.Content);
        }
    }
}
#endif