#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Assets.Scripts.Plugins.Features.Inventory.HoverCards
{
    public sealed class BaseStatsHoverCardPartConverter : IDiscoverableHoverCardPartViewConverter
    {
        public bool CanHandle(IHoverCardPartViewModel viewModel) => viewModel is IBaseStatsHoverCardPartViewModel;

        public object Create(IHoverCardPartViewModel viewModel)
        {
            var castedViewModel = (IBaseStatsHoverCardPartViewModel)viewModel;

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            foreach (var entry in castedViewModel.NamesAndValues)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                var nameView = new TextBlock()
                {
                    Text = $"{entry.Item1}: ",
                    TextAlignment = TextAlignment.Right
                };
                Grid.SetColumn(nameView, 0);
                Grid.SetRow(nameView, grid.RowDefinitions.Count - 1);
                grid.Children.Add(nameView);

                var valueView = new TextBlock()
                {
                    Text = entry.Item2.ToString(CultureInfo.InvariantCulture),
                    TextAlignment = TextAlignment.Left
                };
                Grid.SetColumn(valueView, 1);
                Grid.SetRow(valueView, grid.RowDefinitions.Count - 1);
                grid.Children.Add(valueView);
            }

            return grid;
        }
    }
}