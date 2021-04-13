#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
using NoesisApp;
using System;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
#endif

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis.Resources
{
    public partial class AnimatedNumber : UserControl
    {
        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            nameof(AnimationDuration),
            typeof(TimeSpan),
            typeof(AnimatedNumber),
            new PropertyMetadata(new TimeSpan(0, 0, 0, 0, 500)));

        // Using a DependencyProperty as the backing store for Number.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
            nameof(Number),
            typeof(int),
            typeof(AnimatedNumber),
            new PropertyMetadata((int)0, OnNumberChanged));

        // Using a DependencyProperty as the backing store for AnimatedNumber.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty AnimatedNumberProperty = DependencyProperty.Register(
            nameof(AnimatedNumber),
            typeof(int),
            typeof(AnimatedNumber),
            new PropertyMetadata((int)0));

        public AnimatedNumber()
        {
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            NoesisComponentInitializer.InitializeComponentXaml(this);
        }
#endif

        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        private static void OnNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatedNumber)d).OnNumberChanged((int)e.OldValue, (int)e.NewValue);
        }

        private void OnNumberChanged(int oldValue, int newValue)
        {
            Int32AnimationUsingKeyFrames animation = new Int32AnimationUsingKeyFrames();
            Storyboard.SetTargetName(animation, "Root");
            Storyboard.SetTargetProperty(animation, new PropertyPath("AnimatedNumber"));

            animation.KeyFrames.Add(new LinearInt32KeyFrame { Value = oldValue, KeyTime = new TimeSpan() });
            animation.KeyFrames.Add(new LinearInt32KeyFrame { Value = newValue, KeyTime = AnimationDuration });

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin(this);
        }
    }
}
