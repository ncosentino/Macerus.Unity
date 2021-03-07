#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif


namespace Assets.Scripts.Scenes.MainMenu.Gui.Views
{
    public interface IMainMenuViewModel
    {
        ImageSource BackgroundImage { get; }
    }
}