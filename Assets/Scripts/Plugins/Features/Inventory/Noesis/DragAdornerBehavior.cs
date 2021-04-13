#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using NoesisApp;
#else
using System.Windows;
using System.Windows.Interactivity;
#endif

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class DragAdornerBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty DragStartOffsetProperty = DependencyProperty.Register(
            nameof(DragStartOffset),
            typeof(Point),
            typeof(DragAdornerBehavior),
            new PropertyMetadata(new Point(0, 0)));

        private static readonly DependencyProperty DraggedItemXProperty = DependencyProperty.Register(
            nameof(DraggedItemX),
            typeof(double),
            typeof(DragAdornerBehavior),
            new PropertyMetadata(0.0));

        private static readonly DependencyProperty DraggedItemYProperty = DependencyProperty.Register(
            nameof(DraggedItemY),
            typeof(double),
            typeof(DragAdornerBehavior),
            new PropertyMetadata(0.0));

        public Point DragStartOffset
        {
            get { return (Point)GetValue(DragStartOffsetProperty); }
            set { SetValue(DragStartOffsetProperty, value); }
        }

        public double DraggedItemX
        {
            get { return (double)GetValue(DraggedItemXProperty); }
            private set { SetValue(DraggedItemXProperty, value); }
        }

        public double DraggedItemY
        {
            get { return (double)GetValue(DraggedItemYProperty); }
            private set { SetValue(DraggedItemYProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragOver += OnDragOver;
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.AllowDrop = false;
            AssociatedObject.DragOver -= OnDragOver;
            AssociatedObject.Drop -= OnDrop;

            base.OnDetaching();
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            var position = e.GetPosition(AssociatedObject);
            DraggedItemX = position.X - DragStartOffset.X;
            DraggedItemY = position.Y - DragStartOffset.Y;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
        }
    }
}
