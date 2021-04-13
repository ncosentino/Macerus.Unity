#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Gui.Noesis
{
    public interface IResourceImageSourceFactory
    {
        ImageSource CreateForResourceId(IIdentifier resourceId);
    }
}