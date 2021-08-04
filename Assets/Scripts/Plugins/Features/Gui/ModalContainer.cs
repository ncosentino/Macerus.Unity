#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using EventArgs = System.EventArgs;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

using System;
using System.Collections.Generic;

using ProjectXyz.Framework.ViewWelding.Api;

using Assets.Scripts.Gui.Noesis;

namespace Assets.Scripts.Plugins.Features.Gui
{
    public sealed class ModalContainer : UserControl
    {
        private readonly Grid _grid;

        public ModalContainer(
            IViewWelderFactory viewWelderFactory,
            object content,
            IEnumerable<IModalButtonNoesisViewModel> buttons)
        {
            _grid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Gray,
            };
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Populate(content, buttons);

            Content = _grid;
        }

        public event EventHandler<EventArgs> Closed;

        private void Populate(
            object content,
            IEnumerable<IModalButtonNoesisViewModel> buttons)
        {
            foreach (var button in buttons)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                var wpfButton = new Button()
                {
                    Content = button.Text,
                    Command = new DelegateCommand(b =>
                    {
                        button.Command.Execute(b);
                        Closed?.Invoke(this, EventArgs.Empty);
                    })
                };
                _grid.Children.Add(wpfButton);

                Grid.SetColumn(wpfButton, _grid.ColumnDefinitions.Count - 1);
                Grid.SetRow(wpfButton, 1);
            }

            // FIXME: we need grid view welding here
            //_viewWelderFactory
            //    .Create<ISimpleWelder>(
            //        modalContainer,
            //        modalChild)
            //    .Weld();
            var uiContent = (UIElement)content;
            _grid.Children.Add(uiContent);
            Grid.SetColumnSpan(uiContent, _grid.ColumnDefinitions.Count);
            Grid.SetColumn(uiContent, 0);
            Grid.SetRow(uiContent, 0);
        }
    }
}
