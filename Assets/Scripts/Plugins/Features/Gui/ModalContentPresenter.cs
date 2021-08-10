#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using EventArgs = System.EventArgs;
#else
using System.Windows;
using System.Windows.Controls;
#endif

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Macerus.Plugins.Features.Gui;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

using Assets.Scripts.Gui.Noesis;

namespace Assets.Scripts.Plugins.Features.Gui
{
    public sealed class ModalContentPresenter : IModalContentPresenter
    {
        private readonly IModalContentConverterFacade _modalContentConverter;
        private readonly IViewWelderFactory _viewWelderFactory;
        private readonly IModalWindow _modalWindow;
        private readonly IUiDispatcher _uiDispatcher;

        public ModalContentPresenter(
            IModalContentConverterFacade modalContentConverter,
            IViewWelderFactory viewWelderFactory,
            IModalWindow modalWindow,
            IUiDispatcher uiDispatcher)
        {
            _modalContentConverter = modalContentConverter;
            _viewWelderFactory = viewWelderFactory;
            _modalWindow = modalWindow;
            _uiDispatcher = uiDispatcher;
        }

        public async Task PresentAsync(
            object content,
            IEnumerable<IModalButtonViewModel> buttons)
        {
            _uiDispatcher.RunOnMainThread(() =>
            {
                var wpfButtonViewModels = buttons.Select(x => new ModalButtonNoesisViewModel(
                x.StringResourceId.ToString(),
                new DelegateCommand(_ => x.ButtonSelected())));

                var modalChild = _modalContentConverter.ConvertContentToWeldableView(content);
                var modalContainer = new ModalContainer(
                    _viewWelderFactory,
                    (UIElement)modalChild,
                    wpfButtonViewModels);
                modalContainer.Closed += ModalContainer_Closed;
                _viewWelderFactory
                    .Create<ISimpleWelder>(
                        _modalWindow,
                        modalContainer)
                    .Weld();
                _modalWindow.Visibility = Visibility.Visible;
            });            
        }

        private void ModalContainer_Closed(object sender, EventArgs e)
        {
            // FIXME: do we have some sort of support for breaking welds in the future?
            var grid = (Grid)_modalWindow;
            grid.Children.Remove((UIElement)sender);
            _modalWindow.Visibility = grid.Children.Cast<object>().Any(x => x != null)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
