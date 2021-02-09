namespace Assets.Scripts.Unity
{
    public interface ITimeProvider
    {
        float SecondsSinceLastFrame { get; }

        float SecondsSinceStartOfGame { get; }
    }
}