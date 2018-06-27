using ProjectXyz.Framework.ViewWelding.Api;

namespace Assets.Scripts.Plugins.Features.UnityViewWelding.Api
{
    public interface ISimpleViewWelder : IViewWelder
    {
        void Weld();

        void Weld(ISimpleViewWeldingOptions simpleViewWeldingOptions);
    }
}