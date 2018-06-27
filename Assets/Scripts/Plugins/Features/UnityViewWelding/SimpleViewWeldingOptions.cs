using Assets.Scripts.Plugins.Features.UnityViewWelding.Api;

namespace Assets.Scripts.Plugins.Features.UnityViewWelding
{
    public sealed class SimpleViewWeldingOptions : ISimpleViewWeldingOptions
    {
        public bool OrderFirst { get; set; }

        public bool OrderLast { get; set; }
    }
}