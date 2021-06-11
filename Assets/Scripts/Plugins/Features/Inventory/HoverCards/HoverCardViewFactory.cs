#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Plugins.Features.Inventory.HoverCards
{
    public sealed class HoverCardViewFactory : IHoverCardViewFactory
    {
        private readonly IHoverCardPartViewConverterFacade _cardPartViewConverter;
        private readonly IViewWelderFactory _viewWelderFactory;

        public HoverCardViewFactory(
            IHoverCardPartViewConverterFacade cardPartViewConverter,
            IViewWelderFactory viewWelderFactory)
        {
            _cardPartViewConverter = cardPartViewConverter;
            _viewWelderFactory = viewWelderFactory;
        }

        public object Create(IHoverCardViewModel viewModel)
        {
            var stackPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
            };

            foreach (var view in viewModel.Parts.Select(p => _cardPartViewConverter.Create(p)))
            {
                _viewWelderFactory
                    .Create<ISimpleWelder>(
                        stackPanel,
                        view)
                    .Weld();
            }

            var hoverCardView = new ContentControl()
            {
                Content = stackPanel,
                //Width = 200,
                //Height = 200,
            };
            return hoverCardView;
        }
    }
}
