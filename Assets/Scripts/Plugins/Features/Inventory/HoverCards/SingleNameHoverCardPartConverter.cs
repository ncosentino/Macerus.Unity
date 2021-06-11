#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Assets.Scripts.Plugins.Features.Inventory.HoverCards
{
    public sealed class SingleNameHoverCardPartConverter : IDiscoverableHoverCardPartViewConverter
    {
        public bool CanHandle(IHoverCardPartViewModel viewModel) => viewModel is ISingleNameHoverCardPartViewModel;

        public object Create(IHoverCardPartViewModel viewModel)
        {
            var label = new Label();
            label.Content = ((ISingleNameHoverCardPartViewModel)viewModel).Name;
            return label;
        }
    }
}
