namespace Assets.Scripts.Gui.Unity.ViewWelding.Api
{
    public interface IStackViewWeldingOptions : IReadOnlyStackViewWeldingOptions
    {
        new bool OrderFirst { get; set; }

        new bool OrderLast { get; set; }
    }
}