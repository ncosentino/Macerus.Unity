#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using Color = Noesis.Color;
#else
using System.Windows.Media;
using Color = System.Windows.Media.Color;
#endif

using System;
using System.ComponentModel;
using System.Collections.Generic;

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.Inventory.Api;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class EquipmentSlotNoesisViewModel :
        NotifierBase,
        IEquipGridItemSlotNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        static EquipmentSlotNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<EquipmentSlotNoesisViewModel>();
        }

        public EquipmentSlotNoesisViewModel(
            IItemSlotViewModel viewModelToWrap,
            ImageSource iconImageSource,
            Brush backgroundBrush,
            float iconOpacity,
            Color iconColor,
            int gridRow,
            int gridRowSpan,
            int gridColumn,
            int gridColumnSpan)
        {
            ViewModelToWrap = viewModelToWrap;
            IconImageSource = iconImageSource;
            GridRow = gridRow;
            GridRowSpan = gridRowSpan;
            GridColumn = gridColumn;
            GridColumnSpan = gridColumnSpan;
            BackgroundBrush = backgroundBrush;
            IconColor = iconColor;
            IconOpacity = iconOpacity;

            ViewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        public object Id => ViewModelToWrap.Id;

        public bool HasItem => ViewModelToWrap.HasItem;

        public ImageSource IconImageSource { get; }

        public Brush BackgroundBrush { get; }

        public Color IconColor { get; }

        public float IconOpacity { get; }

        public string SlotLabel => ViewModelToWrap.SlotLabel;

        public bool ShowLabel => ViewModelToWrap.ShowLabel;

        [NotifyForWrappedProperty(nameof(IItemSlotViewModel.IsDragOver))]
        public bool IsDragOver
        {
            get => ViewModelToWrap.IsDragOver;
            set => ViewModelToWrap.IsDragOver = value;
        }

        [NotifyForWrappedProperty(nameof(IItemSlotViewModel.IsDropAllowed))]
        public bool IsDropAllowed
        {
            get => ViewModelToWrap.IsDropAllowed != false;
            set => ViewModelToWrap.IsDropAllowed = value;
        }

        [NotifyForWrappedProperty(nameof(IItemSlotViewModel.IsFocused))]
        public bool IsFocused
        {
            get => ViewModelToWrap.IsFocused;
            set => ViewModelToWrap.IsFocused = value;
        }

        public int GridRow { get; }

        public int GridRowSpan { get; }

        public int GridColumn { get; }

        public int GridColumnSpan { get; }

        internal IItemSlotViewModel ViewModelToWrap { get; }

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (!_lazyNotifierMapping.Value.TryGetValue(
                e.PropertyName,
                out var propertyName))
            {
                return;
            }

            OnPropertyChanged(propertyName);
        }
    }
}
