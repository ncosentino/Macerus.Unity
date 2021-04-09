
using ProjectXyz.Framework.ViewWelding.Api;

namespace Assets.Scripts.Gui.Unity.ViewWelding.Api
{
    public interface IStackViewWelder : IViewWelder
    {
        void Weld();

        void Weld(IStackViewWeldingOptions simpleViewWeldingOptions);
    }
}