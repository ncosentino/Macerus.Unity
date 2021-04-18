#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using NoesisApp;
using System.Windows.Input;
#else
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
#endif

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class DropItemBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty IsDragOverProperty = DependencyProperty.Register(
            nameof(IsDragOver),
            typeof(bool),
            typeof(DropItemBehavior),
            new PropertyMetadata(false));

        public static readonly DependencyProperty DropCommandProperty = DependencyProperty.Register(
            nameof(DropCommand),
            typeof(ICommand),
            typeof(DropItemBehavior),
            new PropertyMetadata(null));

        public bool IsDragOver
        {
            get { return (bool)GetValue(IsDragOverProperty); }
            set { SetValue(IsDragOverProperty, value); }
        }

        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragEnter += OnDragEnter;
            AssociatedObject.DragLeave += OnDragLeave;
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.AllowDrop = false;
            AssociatedObject.DragEnter -= OnDragEnter;
            AssociatedObject.DragLeave -= OnDragLeave;
            AssociatedObject.Drop -= OnDrop;

            base.OnDetaching();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            IsDragOver = true;
            e.Handled = true;
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            IsDragOver = false;
            e.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            IsDragOver = false;

            var item = AssociatedObject.DataContext;
            if (item != null && DropCommand != null && DropCommand.CanExecute(item))
            {
                DropCommand.Execute(item);
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }
    }
}
