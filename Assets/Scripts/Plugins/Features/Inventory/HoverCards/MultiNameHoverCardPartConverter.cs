#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Assets.Scripts.Plugins.Features.Inventory.HoverCards
{
    public sealed class MultiNameHoverCardPartConverter : IDiscoverableHoverCardPartViewConverter
    {
        public bool CanHandle(IHoverCardPartViewModel viewModel) => viewModel is IMultiNameHoverCardPartViewModel;

        public object Create(IHoverCardPartViewModel viewModel)
        {
            var stackPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
            };

            var label = new Label();
            label.Content = ((IMultiNameHoverCardPartViewModel)viewModel).Name;
            stackPanel.Children.Add(label);

            label = new Label();
            label.Content = ((IMultiNameHoverCardPartViewModel)viewModel).Subname;
            stackPanel.Children.Add(label);

            return stackPanel;
        }
    }
}
