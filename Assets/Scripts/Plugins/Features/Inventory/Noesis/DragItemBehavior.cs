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
    public sealed class DragItemBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty DragStartOffsetProperty = DependencyProperty.Register(
            nameof(DragStartOffset),
            typeof(Point),
            typeof(DragItemBehavior),
            new PropertyMetadata(new Point(0, 0)));

        public static readonly DependencyProperty StartDragCommandProperty = DependencyProperty.Register(
            nameof(StartDragCommand),
            typeof(ICommand),
            typeof(DragItemBehavior),
            new PropertyMetadata(null));

        public static readonly DependencyProperty EndDragCommandProperty = DependencyProperty.Register(
            nameof(EndDragCommand),
            typeof(ICommand),
            typeof(DragItemBehavior),
            new PropertyMetadata(null));

        private bool _mouseClicked;

        public Point DragStartOffset
        {
            get { return (Point)GetValue(DragStartOffsetProperty); }
            set { SetValue(DragStartOffsetProperty, value); }
        }

        public ICommand StartDragCommand
        {
            get { return (ICommand)GetValue(StartDragCommandProperty); }
            set { SetValue(StartDragCommandProperty, value); }
        }

        public ICommand EndDragCommand
        {
            get { return (ICommand)GetValue(EndDragCommandProperty); }
            set { SetValue(EndDragCommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.GiveFeedback += OnGiveFeedback;
            AssociatedObject.PreviewMouseLeftButtonDown += OnMouseDown;
            AssociatedObject.PreviewMouseLeftButtonUp += OnMouseUp;
            AssociatedObject.PreviewMouseMove += OnMouseMove;
        }

        private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GiveFeedback -= OnGiveFeedback;
            AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseDown;
            AssociatedObject.PreviewMouseLeftButtonUp -= OnMouseUp;
            AssociatedObject.PreviewMouseMove -= OnMouseMove;

            base.OnDetaching();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseClicked = true;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _mouseClicked = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseClicked)
            {
                return;
            }

            _mouseClicked = false;

            var item = AssociatedObject.DataContext;
            if (item != null)
            {
                if (StartDragCommand != null && StartDragCommand.CanExecute(item))
                {
                    DragStartOffset = e.GetPosition(AssociatedObject);

                    StartDragCommand.Execute(item);

#if NOESIS
                    DragDrop.DoDragDrop(this.AssociatedObject, item, DragDropEffects.Move,
                        (source, data, target, dropPoint, effects) =>
                        {
                            bool dragSuccess = effects != DragDropEffects.None;
                            if (EndDragCommand != null && EndDragCommand.CanExecute(dragSuccess))
                            {
                                EndDragCommand.Execute(dragSuccess);
                            }
                        });
#else
                    var effects = DragDrop.DoDragDrop(AssociatedObject, item, DragDropEffects.Move);

                    var dragSuccess = effects != DragDropEffects.None;
                    if (EndDragCommand != null && EndDragCommand.CanExecute(dragSuccess))
                    {
                        EndDragCommand.Execute(dragSuccess);
                    }
#endif
                }
            }
        }
    }
}
