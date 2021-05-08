#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

namespace Assets.Scripts.Plugins.Features.HeaderBar.Noesis
{
    public interface IHeaderBarNoesisViewModel
    {
    }
}