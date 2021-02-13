namespace Assets.Scripts.Unity.Input
{
    public interface IMouseInput : IReadOnlyMouseInput
    {
        new bool SimulateMouseWithTouches { get; set; }
    }
}