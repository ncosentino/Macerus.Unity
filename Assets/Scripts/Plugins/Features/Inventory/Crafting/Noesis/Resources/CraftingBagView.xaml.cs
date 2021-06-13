#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#endif

using Assets.Scripts.Plugins.Features.Inventory.Noesis;

namespace Assets.Scripts.Plugins.Features.Inventory.Crafting.Noesis.Resources
{
    public partial class CraftingBagView :
        UserControl,
        ICraftingBagView
    {
        public CraftingBagView(IItemSlotCollectionNoesisViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

#if NOESIS
        private void InitializeComponent()
        {
            NoesisComponentInitializer.InitializeComponentXaml(this);
        }
#endif
    }

#if !NOESIS
    internal sealed class TestBagItemSlotCollectionNoesisViewModel : IItemSlotCollectionNoesisViewModel
    {
        public IReadOnlyCollection<IItemSlotNoesisViewModel> ItemSlots => new IItemSlotNoesisViewModel[0];

        public ImageSource BackgroundImageSource => null;

        public ICommand StartDragItem => null;

        public ICommand EndDragItem => null;

        public ICommand DropItem => null;

        public bool IsDragOver { get; set; }
    }
#endif
}
