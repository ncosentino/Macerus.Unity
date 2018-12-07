namespace Assets.Scripts.Plugins.Features.UnityViewWelding.Api
{
    public interface IStackViewWeldingOptions : IReadOnlyStackViewWeldingOptions
    {
        new bool OrderFirst { get; set; }

        new bool OrderLast { get; set; }
    }
}