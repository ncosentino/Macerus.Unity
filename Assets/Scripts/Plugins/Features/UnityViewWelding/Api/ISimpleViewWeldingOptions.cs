namespace Assets.Scripts.Plugins.Features.UnityViewWelding.Api
{
    public interface ISimpleViewWeldingOptions : IReadOnlySimpleViewWeldingOptions
    {
        new bool OrderFirst { get; set; }

        new bool OrderLast { get; set; }
    }
}