using Noesis;

namespace Assets.Scripts.Gui.Noesis
{
    public interface INoesisGuiPrefab : IGuiPrefab
    {
        ContentControl ContainerControl { get; }
    }
}
