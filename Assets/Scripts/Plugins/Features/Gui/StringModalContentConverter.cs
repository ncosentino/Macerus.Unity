#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.Gui;

namespace Assets.Scripts.Plugins.Features.Gui
{
    public sealed class StringModalContentConverter : IDiscoverableModalContentConverter
    {
        public bool CanConvert(object content) => content is string;

        public object ConvertContentToWeldableView(object content)
        {
            var castedContent = (string)content;
            var view = new TextBlock()
            {
                Text = castedContent,
            };
            return view;
        }
    }
}
