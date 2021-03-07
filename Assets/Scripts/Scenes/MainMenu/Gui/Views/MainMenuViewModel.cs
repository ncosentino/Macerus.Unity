#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;

using UnityEngine;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif


namespace Assets.Scripts.Scenes.MainMenu.Gui.Views
{
    public sealed class MainMenuViewModel : IMainMenuViewModel
    {
        public ImageSource BackgroundImage { get; } =
            new TextureSource((Texture2D)UnityEngine.Resources.Load("Graphics/Gui/MainMenu/background"));
    }
}