using Noesis;

using ProjectXyz.Api.Framework;


namespace Assets.Scripts.Gui.Noesis
{
    public interface IResourceImageSourceFactory
    {
        ImageSource CreateForResourceId(IIdentifier resourceId);
    }
}