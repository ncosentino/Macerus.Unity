using Noesis;

namespace Assets.Scripts.Gui.Noesis
{
    public interface INoesisGuiPrefab : IGuiPrefab
    {
        Viewbox ViewBox { get; }
    }
}
